using Microsoft.AspNetCore.Mvc;
using TransactionTestApp.Abstractions.Transactions;
using TransactionTestApp.Data;
using TransactionTestApp.Models.ClientDtos;
using TransactionTestApp.Models.ViewModels;

namespace TransactionTestApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, 
            ITransactionService transactionService,
            TransactionDbContext dbContext)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<TransactionView> GetTransactionById(Guid id, CancellationTokenSource token)
        {
            var result = await _transactionService.GetById(id);

            return result;
        }

        [HttpPost]
        public async Task<TransactionCreatedView> CreateTransaction(Transaction transactionDto)
        {
            var result = await _transactionService.Create(transactionDto);

            return result;
        }

        [HttpGet("test")]
        public IEnumerable<TransactionView> GetTestTransactions()
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
