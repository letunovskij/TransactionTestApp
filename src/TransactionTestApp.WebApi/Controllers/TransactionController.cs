using Microsoft.AspNetCore.Mvc;
using TransactionTestApp.Models.ViewModels;

namespace TransactionTestApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTransactions")]
        public IEnumerable<TransactionView> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new TransactionView
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                Amount = Random.Shared.Next(1, 55)
            })
            .ToArray();
        }
    }
}
