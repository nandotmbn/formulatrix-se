using Microsoft.AspNetCore.Mvc;
using FormulatrixB.Interfaces;
using FormulatrixB.Models;
using FormulatrixBWebAPI.Types;

namespace FormulatrixBWebAPI.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController(IItem item) : ControllerBase
{
  [HttpGet("{name}")]
  public async Task<ActionResult<string>> Retrieve([FromRoute] string name)
  {
    var result = await item.Retrieve(name);
    return Ok(result);
  }

  [HttpGet("type/{name}")]
  public async Task<ActionResult<ItemType>> GetType([FromRoute] string name)
  {
    var result = await item.GetType(name);
    return Ok(result);
  }

  [HttpPost]
  public async Task<ActionResult<Item>> Register([FromBody] ItemRequest request)
  {
    var result = await item.Register(request.Name, request.Content, request.Type);
    return Ok(result);
  }

  [HttpDelete("{name}")]
  public async Task<ActionResult<string>> Deregister([FromRoute] string name)
  {
    var result = await item.Deregister(name);
    return Ok(result);
  }
}