using Microsoft.AspNetCore.Mvc;
using TransactionTestApp.Abstractions.Transactions;
using TransactionTestApp.Common.Exceptions;
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
            ITransactionService transactionService)
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
            if (transactionDto.Amount <= 0)
            {
                var details = new Common.Responses.ProblemDetails()
                { 
                    RequestStatus = StatusCodes.Status400BadRequest,
                    Title = "Ошибка при валидации входных данных",
                    Detail = "Cумма в транзакциях должна быть всегда положительной",
                    Instance = "TransactionAmount"
                };

                throw new BusinessErrorException(details);
            }

            if (transactionDto.TransactionDate > DateTime.UtcNow)
            {
                var details = new Common.Responses.ProblemDetails()
                {
                    RequestStatus = StatusCodes.Status400BadRequest,
                    Title = "Ошибка при валидации входных данных",
                    Detail = "Дата не может быть в будущем",
                    Instance = "TransactionDate"
                };

                throw new BusinessErrorException(details);
            }

            var count = await _transactionService.GetCount();
            if (count >= 100)
            {
                var details = new Common.Responses.ProblemDetails()
                {
                    RequestStatus = StatusCodes.Status500InternalServerError,
                    Title = "Слишком много сущностей на сервере",
                    Detail = "Ограничение на количество одновременно хранимых транзакций:\r\n100 штук",
                    Instance = "Transaction"
                };

                throw new BusinessErrorException(details);
            }

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
