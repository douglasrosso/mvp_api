using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mvp_api.Dto.Consumidores;
using mvp_api.Models;
using mpv_api.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using MySqlConnector;
using System.Text;

namespace mvp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumidorController : ControllerBase
{
    private AppDbContext _context;
    private IMapper _mapper;

    public ConsumidorController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult PostConsumidor([FromBody] CreateConsumidorDto consumidorDto)
    {
        Consumidor consumidor = _mapper.Map<Consumidor>(consumidorDto);
        if (consumidor.Doc_Consumidor.Length == 11 || consumidor.Doc_Consumidor.Length == 14)
        {
            _context.Consumidores.Add(consumidor);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("Consumidores.IX_Consumidores_Email"))
                {
                    return Conflict("Email Duplicado");
                }
                else if (ex.InnerException.Message.Contains("Consumidores.IX_Consumidores_Doc_Consumidor"))
                {
                    return Conflict("Documento Duplicado");
                }
                else
                {   
                    throw;
                }
            }catch (Exception ex)
            {
                return BadRequest("Não foi possível salvar os dados");
            }
        }
        else
            return BadRequest("Documento inválido");
        return CreatedAtAction(nameof(GetConsumidorCod), new { cod = consumidor.Cod_Consumidor }, consumidor);

    }

    [HttpGet]
    public IEnumerable<ReadConsumidorDto> GetConsumidor([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadConsumidorDto>>(_context.Consumidores.Skip(skip).Take(take));
    }

    [HttpGet("{cod}")]
    public IActionResult GetConsumidorCod(int cod)
    {
        var consumidor = _context.Consumidores.FirstOrDefault(consumidor => consumidor.Cod_Consumidor == cod);
        if (consumidor == null) return NotFound();
        var consumidorDto = _mapper.Map<ReadConsumidorPorCodDto>(consumidor);
        return Ok(consumidorDto);
    }

    [HttpGet("Documento/{doc}")]
    public IActionResult GetConsumidorDoc(string doc)
    {
        var verificaDoc = _context.Consumidores.FirstOrDefault(consumidor => consumidor.Doc_Consumidor == doc);
        if (verificaDoc == null) return NotFound();
        var get = _mapper.Map<ReadConsumidorPorCodDto>(verificaDoc);
        return Ok(get);
    }
    
    [HttpPut("{cod}")]
    public IActionResult PutConsumidor(int cod, [FromBody] UpdateConsumidorDto consumidorDto)
    {
        var consumidor = _context.Consumidores.FirstOrDefault(
            consumidor => consumidor.Cod_Consumidor == cod);
        if (consumidor == null) return NotFound();
        _mapper.Map(consumidorDto, consumidor);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpPut("Edita/{cod}")]
    public IActionResult PutDistConsu(int cod, [FromBody] UpdateDistribuidoraConsumidorDto consumidorDto)
    {
        var consumidor = _context.Consumidores.FirstOrDefault(
            consumidor => consumidor.Cod_Consumidor == cod);
        if (consumidor == null) return NotFound();
        _mapper.Map(consumidorDto, consumidor);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{cod}")]
    public IActionResult PatchConsumidor(int cod,
        JsonPatchDocument<UpdateConsumidorDto> patch)
    {
        var consumidor = _context.Consumidores.FirstOrDefault(
            consumidor => consumidor.Cod_Consumidor == cod);
        if (consumidor == null) return NotFound();

        var ConsumidorParaAtualizar = _mapper.Map<UpdateConsumidorDto>(consumidor);

        patch.ApplyTo(ConsumidorParaAtualizar, ModelState);

        if (!TryValidateModel(ConsumidorParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(ConsumidorParaAtualizar, consumidor);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{cod}")]
    public IActionResult DeleteConsumidor(int cod)
    {
        var validaCod = _context.Consumidores.FirstOrDefault(
            consumidor => consumidor.Cod_Consumidor == cod);
        if (validaCod == null) return NotFound();
        _context.Remove(validaCod);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut("Cadastro/{cod}")]
    public IActionResult Cadastro(string cod, [FromBody] CadastroClienteDto cadastroDto)
    {
        var validaCod = _context.Consumidores.FirstOrDefault(
            consumidor => consumidor.Doc_Consumidor == cod);
        if (validaCod == null) return NotFound();
        _mapper.Map(cadastroDto, validaCod);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException.Message.Contains("Consumidores.IX_Consumidores_Email"))
            {
                return Conflict("Email Duplicado");
            }
            else if (ex.InnerException.Message.Contains("Consumidores.IX_Consumidores_Doc_Consumidor"))
            {
                return Conflict("Documento Duplicado");
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Não foi possível salvar os dados");
        }
        return NoContent();
    }

    [HttpPost("Login/{cod}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var consumidorBanco = _context.Consumidores.FirstOrDefault(login => login.Doc_Consumidor == loginDto.Doc_Consumidor);
            if (consumidorBanco == null)
                return NotFound("O documento está inválido");
            else if (consumidorBanco.Senha != loginDto.Senha)
                return Unauthorized("A senha está incorreta");
            else
                return Ok(consumidorBanco);
        }
        catch (MySqlException ex)
        {
            return BadRequest("não foi possivel encontrar o banco de dados");
        }
    }
    [HttpGet("ProcuraDocumento/{doc}")]
    public IActionResult GetConsumidorProcuraDoc(string doc)
    {

        var procura = (from c in _context.Consumidores
                       where c.Doc_Consumidor.Contains(doc)
                       select new ReadConsumidorDto
                       {
                           Cod_Consumidor = c.Cod_Consumidor,
                           Status = c.Status,
                           Nome_Consumidor = c.Nome_Consumidor,
                           Email = c.Email,
                           Doc_consumidor = c.Doc_Consumidor,
                           Senha = c.Senha,
                           Data_cadastro = c.Data_Cadastro,
                           Telefone1 = c.Telefone1,
                           Telefone2 = c.Telefone2
                       }).ToList();
        if (procura == null) return NotFound();
        var get = _mapper.Map<List<ReadConsumidorDto>>(procura);
        return Ok(procura);
    }

    [HttpGet("ForgetPassword/{cod}")]
    public IActionResult RecuperaSenha(string cod)
    {
        var doc = _context.Consumidores.FirstOrDefault(
            doc => doc.Doc_Consumidor == cod);

        if (doc != null)
        {
            string senhaAlterada = GerarSenhaAleatoria();

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("usealltestes@outlook.com");
            mail.To.Add(doc.Email);
            mail.Subject = "Password reset request for your LiveAgent account";
            mail.Body = $"Olá {doc.Nome_Consumidor},\r\n\r\nHouve um pedido para alterar sua senha!\r\n\r\nSe você não fez esta solicitação, ignore este e-mail.\r\n\r\nCaso contrário, clique neste link para alterar sua senha: [{senhaAlterada}]";

            using (var smtp = new SmtpClient("smtp.office365.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("usealltestes@outlook.com", "1q2w3e4r5t6y");
                try
                {
                    smtp.Send(mail);
                    doc.Senha = senhaAlterada;
                    _context.SaveChanges();
                    return Ok("Enviado com sucesso");
                }
                catch (Exception ex)
                {
                    return BadRequest("Houve um problema, no envio do email");
                }
            }
        }
        else
        {
            return BadRequest("Documento não cadastrado");
        }
    }

    public static string GerarSenhaAleatoria()
    {
        string letrasNumeros = "abcdefghijklmnopqrstuvwxyz0123456789";

        Random rnd = new Random();

        StringBuilder str = new StringBuilder();
        for (int m = 1; m <= 8; m++)
        {
            int pos = rnd.Next(0, letrasNumeros.Length);
            str.Append(letrasNumeros[pos].ToString());
        }
        return str.ToString();
    }
}
