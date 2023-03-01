using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mvp_api.Dto.Item;
using mvp_api.Models;
using mpv_api.Data;

namespace mvp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private AppDbContext _context;
    private IMapper _mapper;

    public ItemController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult PostItem([FromBody] CreateItensDto itemDto)
    {
        Item item = _mapper.Map<Item>(itemDto);
        _context.Itens.Add(item);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetItemCod), new { cod = item.cod_item }, item);
    }


    [HttpGet]
    public IEnumerable<ReadItensDto> GetItem([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadItensDto>>(_context.Itens.Skip(skip).Take(take));
    }

    [HttpGet("{cod}")]
    public IActionResult GetItemCod(int cod)
    {
        var item = _context.Itens.FirstOrDefault(item => item.cod_item == cod);
        if (item == null) return NotFound();
        var itemDto = _mapper.Map<ReadItensDto>(item);
        return Ok(itemDto);
    }

    [HttpPut("{cod}")]
    public IActionResult PutItem(int cod, [FromBody] UpdateItensDto itemDto)
    {
        var item = _context.Itens.FirstOrDefault(
            item => item.cod_item == cod);
        if (item == null) return NotFound();
        _mapper.Map(itemDto, item);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{cod}")]
    public IActionResult PatchItem(int cod,
        JsonPatchDocument<UpdateItensDto> patch)
    {
        var item = _context.Itens.FirstOrDefault(
            item => item.cod_item == cod);
        if (item == null) return NotFound();

        var ItemParaAtualizar = _mapper.Map<UpdateItensDto>(item);

        patch.ApplyTo(ItemParaAtualizar, ModelState);

        if (!TryValidateModel(ItemParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(ItemParaAtualizar, item);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{cod}")]
    public IActionResult DeleteItem(int cod)
    {
        var item = _context.Itens.FirstOrDefault(
            item => item.cod_item == cod);
        if (item == null) return NotFound();
        _context.Remove(item);
        _context.SaveChanges();
        return Content("Item deletado com sucesso");
    }
}