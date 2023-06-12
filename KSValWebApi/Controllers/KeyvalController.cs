using Data;
using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace KSValWebApi.Controllers;
using System.Data.SqlTypes;


[ApiController]
[Route("api/[controller]")]
public class KeyvalController : ControllerBase
{
  private readonly KeyvalContext _context;

  public KeyvalController(KeyvalContext context)
  {
    _context = context;
  }

  // GET: api/keyvals
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Keyval>>> GetKeyvals()
  {
    return Ok(await _context.keyvals.ToListAsync());
  }

  // GET: api/keyvals/Namekey_key
  [HttpGet("{key:guid}")]
  public async Task<ActionResult<Keyval>> GetKeyval(Guid key)
  {
    var keyval = await _context.keyvals.FindAsync(key);

    if (keyval == null)
    {
      return NotFound();
    }

    // return Ok(keyval);
    return keyval;
  }

  // POST: api/keyvals
  [HttpPost]
  public async Task<ActionResult<Keyval>> PostKeyval(Keyval keyval)
  {
    keyval.KName = Guid.NewGuid();
    await _context.keyvals.AddAsync(keyval);
    await _context.SaveChangesAsync();

    // return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    return CreatedAtAction(nameof(GetKeyval), new { key = keyval.KName }, keyval);

  }

  // PUT: api/keyvals/KNamekey
  [HttpPut("{key:guid}")] // [Route("{key:guid}")] -> "[FromRout] Guid key"
  public async Task<IActionResult> PutKeyval(Guid key, Keyval keyval)
  {
    if (key != keyval.KName)
    {
      return BadRequest();
    }

    _context.Entry(keyval).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!KeyvalExists(key))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }

// Patch
[HttpPatch("{key:guid}")] 
  public async Task<IActionResult> PatchKeyval(Guid key, [FromBody] Keyval keyval)
  {
    if (key != keyval.KName)
    {
      return BadRequest();
    }

    var keyval2 = await _context.keyvals.FindAsync(key);
    keyval2.KValue = keyval.KValue;
    _context.Entry(keyval2).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();     
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!KeyvalExists(key))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }


  // DELETE: api/keyvals/key5_key
  [HttpDelete("{key:guid}")]
  public async Task<IActionResult> DeleteKeyval(Guid key)
  {
    var keyval = await _context.keyvals.FindAsync(key);
    if (keyval == null)
    {
      return NotFound();
    }

    _context.keyvals.Remove(keyval);
    await _context.SaveChangesAsync();

    // return NoContent();
    return Ok(keyval);
  }

  private bool KeyvalExists(Guid key)
  {
    return _context.keyvals.Any(e => e.KName == key);
  }

  // dummy method to test the connection
  [HttpGet("hello")]
  public string Test()
  {
    return "Hello World!";
  }
}