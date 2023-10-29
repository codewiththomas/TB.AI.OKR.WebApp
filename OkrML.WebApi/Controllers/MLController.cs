using Microsoft.AspNetCore.Mvc;
using OkrML.WebApi.Models;

namespace OkrML.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MLController : ControllerBase
    {
        private readonly ILogger<MLController> _logger;

        public MLController(ILogger<MLController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Get([FromBody] ClassificationRequest request)
        {
            var classificationService = new PredictionService();
            var result = classificationService.PredictLabel(request);
            return result;
        }
    }
}