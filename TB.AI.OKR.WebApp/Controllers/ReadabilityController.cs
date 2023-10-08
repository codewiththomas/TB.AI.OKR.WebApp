using Microsoft.AspNetCore.Mvc;
using TB.Tools.Readability;

namespace TB.AI.OKR.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadabilityController : ControllerBase
{
    private IReadabilityService ReadabilityService { get; }


    public ReadabilityController(IReadabilityService readabilityService)
    {
        ReadabilityService = readabilityService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="algorithm">if left blank, ARI will be used</param>
    /// <returns></returns>
    [HttpGet("rate")]
    public IActionResult GetReadadabilityRating(string text, string? algorithm = null)
    {
        if (algorithm is null)
        {
            algorithm = ReadabilityAlgorithms.AutomatedReadabilityIndex.ToString();
        }

        try
        {
            return Ok(ReadabilityService.CalculateReadability(text, algorithm));
        }
        catch (ArgumentException ex)
        { 
            return BadRequest(ex.Message);
        }
       
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("algorithms")]
    public IActionResult GetAlgorithms()
    {
        return Ok(ReadabilityService.GetAvailableAlgorithms());
    }
}
