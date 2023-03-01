using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mpv_api.Data;
using mvp_api.Dto.ItensFatura;
using mvp_api.Models;

namespace mvp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemFaturaController : ControllerBase
{
    private AppDbContext _context;
    private IMapper _mapper;

    public ItemFaturaController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult PostItemFatura([FromBody] CreateItemFaturaDto itemFaturaDto)
    {
        ItemFatura itemFatura = _mapper.Map<ItemFatura>(itemFaturaDto);
        _context.ItensFatura.Add(itemFatura);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetItemFaturaCod), new { cod = itemFatura.Cod_ItemFatura }, itemFatura);
    }

    [HttpGet]
    public IEnumerable<ReadItemFaturaDto> GetItemFatura([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadItemFaturaDto>>(_context.ItensFatura.Skip(skip).Take(take));
    }

    [HttpGet("{cod}")]
    public IActionResult GetItemFaturaCod(int cod)
    {
        var itemFatura = _context.ItensFatura.FirstOrDefault(itemFatura => itemFatura.Cod_ItemFatura == cod);
        if (itemFatura == null) return NotFound();
        var itemDto = _mapper.Map<ReadItemFaturaDto>(itemFatura);
        return Ok(itemDto);
    }

    [HttpPut("{cod}")]
    public IActionResult PutItemFatura(int cod, [FromBody] UpdateItemFaturaDto itemFaturaDto)
    {
        var itemFatura = _context.ItensFatura.FirstOrDefault(
            itemFatura => itemFatura.Cod_ItemFatura == cod);
        if (itemFatura == null) return NotFound();
        _mapper.Map(itemFaturaDto, itemFatura);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{cod}")]
    public IActionResult PatchItemFatura(int cod,
        JsonPatchDocument<UpdateItemFaturaDto> patch)
    {
        var itemFatura = _context.ItensFatura.FirstOrDefault(
            itemFatura => itemFatura.Cod_ItemFatura == cod);
        if (itemFatura == null) return NotFound();

        var ItemFaturaParaAtualizar = _mapper.Map<UpdateItemFaturaDto>(itemFatura);

        patch.ApplyTo(ItemFaturaParaAtualizar, ModelState);

        if (!TryValidateModel(ItemFaturaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(ItemFaturaParaAtualizar, itemFatura);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{cod}")]
    public IActionResult DeleteItemFatura(int cod)
    {
        var itemFatura = _context.ItensFatura.FirstOrDefault(
            itemFatura => itemFatura.Cod_ItemFatura == cod);
        if (itemFatura == null) return NotFound();
        _context.Remove(itemFatura);
        _context.SaveChanges();
        return Content("Item deletado com sucesso");
    }
}
