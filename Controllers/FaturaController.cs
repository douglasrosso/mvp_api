using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using mpv_api.Data;
using mvp_api.Dto.Faturas;
using mvp_api.Models;
using mvp_api.Dto.Consumidores;
using Castle.Core.Resource;

namespace mvp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FaturaController : ControllerBase
{
    private AppDbContext _context;
    private IMapper _mapper;

    public FaturaController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateFatura([FromBody] CreateFaturaDto faturaDto)
    {
        var pegaConsumidor = _context.Consumidores.FirstOrDefault(
            Con => Con.Cod_Consumidor == faturaDto.Cod_consumidor
        );
        if (pegaConsumidor != null)
        {
            if (pegaConsumidor.Status == true)
            {
                var pegaUC = _context.UCs.FirstOrDefault(fat => fat.Cod_UC == faturaDto.Cod_uc); // tratar erro
                if (pegaUC != null)
                {
                    if (pegaUC.Status == true)
                    {
                        if (faturaDto.Competencia.Month == DateTime.Now.AddMonths(-1).Month)
                        {
                            using (
                                IDbContextTransaction transaction =
                                    _context.Database.BeginTransaction()
                            )
                            {
                                try
                                {
                                    Fatura fatura = new Fatura
                                    {
                                        Cod_Consumidor = faturaDto.Cod_consumidor,
                                        Cod_UC = faturaDto.Cod_uc,
                                        Competencia = faturaDto.Competencia,
                                        DataEmissao = faturaDto.DataEmissao,
                                        ValorTotalFatura = faturaDto.ItemFatura
                                            .Select(item => item.ValorTotal)
                                            .Sum(),
                                        ItemFatura = faturaDto.ItemFatura
                                            .Select(
                                                item =>
                                                    new ItemFatura
                                                    {
                                                        Cod_Item = item.Cod_Item,
                                                        QuantItem = item.QuantItem,
                                                        ValorItem = item.ValorItem
                                                    }
                                            )
                                            .ToList()
                                    };
                                    Console.WriteLine(fatura.ValorTotalFatura);
                                    _context.Faturas.Add(fatura);
                                    _context.SaveChanges();
                                    transaction.Commit();
                                    return CreatedAtAction(
                                        nameof(GetFaturaCod),
                                        new { cod = fatura.Cod_Fatura },
                                        fatura
                                    );
                                }
                                catch (DbUpdateException ex)
                                {
                                    if (ex.InnerException.Message.Contains("FK_ItensFatura_Itens_Cod_Item"))
                                    {
                                        return Conflict("Item Inexistente no banco de dados atual");
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    return BadRequest("Algo de errado não está certo");
                                }
                            }
                        }
                        return BadRequest("A data deve ser no máximo de mês anterior");
                    }
                    else
                        return BadRequest("Não foi possível lançar fatura, a UC está inativa");
                }
                else
                    return BadRequest(
                        "Não é possivel lançar fatura, pois o código de Unidade Consumidora informado não existe na base local"
                    );
            }
            else
                return BadRequest("Não foi possível lançar fatura, o Consumidor esta inativo");
        }
        else
            return BadRequest(
                "Não é possivel lançar fatura, pois o código de consumidor informado não existe na base local"
            );
    }

    [HttpGet]
    public IEnumerable<ReadFaturaDto> GetFatura([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFaturaDto>>(_context.Faturas.Skip(skip).Take(take));
    }

    [HttpGet("{cod}")]
    public IActionResult GetFaturaCod(int cod)
    {
        var fatura = _context.Faturas.FirstOrDefault(fatura => fatura.Cod_Fatura == cod);
        if (fatura == null)
            return NotFound();

        var read = new ReadFaturaPorCodDto()
        {
            Cod_Fatura = fatura.Cod_Fatura,
            Cod_Consumidor = fatura.Cod_Consumidor,
            Cod_UC = fatura.Cod_UC,
            Competencia = fatura.Competencia,
            ValorTotalFatura = fatura.ValorTotalFatura,
            Pagamento = fatura.Pagamento,
            DataEmissao = fatura.DataEmissao,
            DataPagamento = fatura.DataPagamento,
            ItemFatura = fatura.ItemFatura
                .Select(
                    item =>
                        new ItemFatura
                        {
                            Cod_ItemFatura = item.Cod_ItemFatura,
                            Cod_Fatura = item.Cod_Fatura,
                            Cod_Item = item.Cod_Item,
                            QuantItem = item.QuantItem,
                            ValorItem = item.ValorItem
                        }
                )
                .ToList()
        };
        return Ok(read);
    }

    [HttpPut("{cod}")]
    public IActionResult PutFatura(int cod, [FromBody] UpdateFaturaDto faturaDto)
    {
        var fatura = _context.Faturas.FirstOrDefault(fatura => fatura.Cod_Fatura == cod);
        if (fatura == null)
            return NotFound();
        if (fatura.Pagamento == false)
        {
            fatura.Cod_Consumidor = faturaDto.Cod_consumidor;
            fatura.Cod_UC = faturaDto.Cod_uc;
            fatura.Pagamento = faturaDto.Pagamento;
            if (faturaDto.Pagamento == true)
            {

                fatura.DataPagamento = DateTime.Now.Date;
            }
            else
            {
                fatura.DataPagamento = faturaDto.DataPagamento;
            }
            fatura.Competencia = faturaDto.Competencia;
            fatura.ValorTotalFatura = faturaDto.ItemFatura
                                            .Select(item => item.ValorTotal)
                                            .Sum();
            fatura.ItemFatura = faturaDto.ItemFatura
                .Select(
                    item =>
                        new ItemFatura
                        {
                            Cod_ItemFatura = item.Cod_ItemFatura,
                            Cod_Fatura = item.Cod_Fatura,
                            Cod_Item = item.Cod_Item,
                            QuantItem = item.QuantItem,
                            ValorItem = item.ValorItem
                        }).ToList();

            _context.SaveChanges();
            return NoContent();
        }
        else
        {
            return BadRequest("Fatura não pode ser alterada pois já está paga");
        }
    }

    [HttpDelete("{cod}")]
    public IActionResult DeleteFatura(int cod)
    {
                try
        {
            var fatura = _context.Faturas.FirstOrDefault(fatura => fatura.Cod_Fatura == cod);
            if (fatura == null)
                return NotFound();
            _context.Remove(fatura);
            _context.SaveChanges();
            return Content("fatura deletada com sucesso");
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException.Message.Contains("CONSTRAINT `FK_ItensFatura_Faturas_Cod_Fatura` FOREIGN KEY (`Cod_Fatura`) REFERENCES `Faturas` (`Cod_Fatura`) ON DELETE RESTRICT)"))
            {
                return Conflict("Impossivel apagar fatura pois ainda ha itens vinculados a ela");
            }
            else
            {
                throw;
            }
        }
    }

    [HttpPatch("{cod}")]
    public IActionResult PatchFatura(int cod, JsonPatchDocument<UpdateFaturaDto> patch)
    {
        var fatura = _context.Faturas.FirstOrDefault(fatura => fatura.Cod_Fatura == cod);
        if (fatura == null)
            return NotFound();
        if (fatura.Pagamento == false)
        {
            var FaturaParaAtualiza = _mapper.Map<UpdateFaturaDto>(fatura);
            patch.ApplyTo(FaturaParaAtualiza, ModelState);

            if (!TryValidateModel(FaturaParaAtualiza))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(FaturaParaAtualiza, fatura);
            _context.SaveChanges();
            return NoContent();
        }
        else
        {
            return BadRequest("Fatura não pode ser alterada pois já está paga"); 
        }
    }

    [HttpGet("PDF/{cod}")]
    public IActionResult GerarPdf(int cod)
    {
        var validaUC = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
        if (validaUC == null) return NotFound();

        var fatura = (from f in _context.Faturas
                      join c in _context.Consumidores
                      on f.Cod_Consumidor equals c.Cod_Consumidor
                      join uc in _context.UCs on f.Cod_UC equals uc.Cod_UC
                      where uc.Cod_UC == cod
                      select new FaturaPDFDto
                      {
                          Nome_Consumidor = c.Nome_Consumidor,
                          Telefone1 = c.Telefone1,
                          Logradouro = uc.Logradouro,
                          Num_casa = uc.Num_Casa,
                          Bairro = uc.Bairro,
                          Cep = uc.CEP,
                          Complemento = uc.Complemento,
                          Doc_Consumidor = c.Doc_Consumidor,
                          Competencia = f.Competencia,
                          DataEmissao = f.DataEmissao,
                          ValorTotalFatura = f.ValorTotalFatura
                      }).FirstOrDefault();

        return Ok(fatura);

    }
}


