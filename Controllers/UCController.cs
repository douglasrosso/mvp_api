using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mpv_api.Data;
using mvp_api.Dto.Faturas;
using mvp_api.Dto.UCs;
using mvp_api.Models;
using System.Data.Entity;

namespace mpv_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UCController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public UCController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostUC(
        [FromBody] CreateUCDto UCDto)
        {
            UC uc = _mapper.Map<UC>(UCDto);
            _context.UCs.Add(uc);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return Content("O consumidor não está cadastrado");
            }
            return CreatedAtAction(nameof(GetUCCod),
                new { cod = uc.Cod_UC },
                uc);

        }

        [HttpGet]
        public IEnumerable<ReadUCDto> GetUC([FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadUCDto>>(_context.UCs.Skip(skip).Take(take));
        }

        [HttpGet("{cod}")]
        public IActionResult GetUCCod(int cod)
        {
            var uc = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
            if (uc == null) return NotFound();
            var ucDto = _mapper.Map<ReadUCPorCodDto>(uc);
            return Ok(ucDto);
        }

        [HttpPut("{cod}")]
        public IActionResult PutUC(int cod, [FromBody]
    UpdateUCDto UCDto)
        {
            var uc = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
            if (uc == null) return NotFound();
            if (uc.Status == true)
            {

                uc.DataLigacao = DateTime.Now.Date;
            }
            else
            {
                uc.DataLigacao= UCDto.DataLigacao;
            }
            _mapper.Map(UCDto, uc);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("TrocaStatus/{cod}")]
        public IActionResult PutStatusUC(int cod, [FromBody]
    UpdateStatusUCDto UCDto)
        {
            var uc = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
            if (uc == null) return NotFound();
            if (uc.Status == true)
            {

                uc.DataLigacao = DateTime.Now.Date;
            }
            else
            {
                uc.DataLigacao = UCDto.DataLigacao;
            }
            _mapper.Map(UCDto, uc);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("TrocaTitularidade/{cod}")]
        public IActionResult PutTitularidadeUC(int cod, [FromBody]
    UpdateTitularidadeUCDto UCDto)
        {
            var uc = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
            if (uc == null) return NotFound("UC não cadastrada");
            if (uc.Status == true)
            {
                uc.DataLigacao = DateTime.Now.Date;
            }
            else
            {
                uc.DataLigacao= UCDto.DataLigacao;
            }
            _mapper.Map(UCDto, uc);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{cod}")]
        public IActionResult PatchUC(int cod,
        JsonPatchDocument<UpdateUCDto> patch)
        {
            var verificaBodyComUC = _context.UCs.FirstOrDefault(
                uc => uc.Cod_UC == cod);
            if (verificaBodyComUC == null) return NotFound();

            var UCParaAtualiza = _mapper.Map<UpdateUCDto>(verificaBodyComUC);
            patch.ApplyTo(UCParaAtualiza, ModelState);

            if (!TryValidateModel(UCParaAtualiza))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(UCParaAtualiza, verificaBodyComUC);
            _context.SaveChanges();
            return NoContent();
            }

        [HttpDelete("{cod}")]
        public IActionResult DeleteUC(int cod)
        {
            var uc = _context.UCs.FirstOrDefault(
                uc => uc.Cod_UC == cod);
            if (uc == null) return NotFound();
            _context.Remove(uc);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("Consumidor/{cod}")]
        public IEnumerable<ReadUCDto> GetUCPorCodConsumidor(int cod)
        {
            var uc = _context.UCs.Where(u => u.Cod_Consumidor == cod);

            List<ReadUCDto> listRead = new List<ReadUCDto>();
            foreach (var u in uc)
            {
                var read = new ReadUCDto()
                {
                    Cod_uc = u.Cod_UC,
                    Cod_consumidor = u.Cod_Consumidor,
                    Status = u.Status,
                    Num_medidor = u.Num_Medidor,
                    Num_casa = u.Num_Casa,
                    Cep = u.CEP,
                    Logradouro = u.Logradouro,
                    Complemento = u.Complemento,
                    Bairro = u.Bairro
                };
                listRead.Add(read);
            }
            return listRead;
        }

        [HttpGet("Faturas/{cod}")]
        public IActionResult GetFaturasCodUC(int cod)
        {
            var uc = _context.UCs.FirstOrDefault(uc => uc.Cod_UC == cod);
                if (uc == null) return NotFound();
            var faturas = _mapper.Map<ReadFaturasPorUCDto>(uc);
            return Ok(faturas);

        }

    }
}
