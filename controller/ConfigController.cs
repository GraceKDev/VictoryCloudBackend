using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VictoryCloudApi.Data;
using VictoryCloudApi.Models;

// [Authorize]
[ApiController]
[Route("Api/[controller]")] 

public class ConfigController : ControllerBase
{
    private readonly MyDbContext _context;
    public ConfigController(MyDbContext myDbContext)
    {
        _context = myDbContext;
    }

    [AllowAnonymous]
    [HttpGet("GetConfig")]
    public IActionResult GetConfig()
    {
        var config = _context.Config.ToDictionary(
            c => c.ConfigItemName,
            c => JsonSerializer.Deserialize<JsonElement>(c.ConfigItemValue)
        );
        return Ok(config);
    }

 
    [HttpPut("UpdateConfig")] 
    public async Task<IActionResult> UpdateConfig([FromBody] Dictionary<string,string> configRequest)
    {
        if(configRequest == null) {
            return BadRequest("No config processed");
        }
        foreach (var (key, value) in configRequest)
        {
            var existing = _context.Config.FirstOrDefault(c => c.ConfigItemName == key);
            if (existing is not null)
            {
                existing.ConfigItemValue = value;
            }
            else
            {
                _context.Config.Add(new Config
                {
                    ConfigItemName = key,
                    ConfigItemValue = value
                });
            }
        }
        await _context.SaveChangesAsync();
        return Ok(new { message = "Config Updated Successfully" });
    }
}

public record ConfigRequestData(Dictionary<string, JsonElement> KeyValuePairs);