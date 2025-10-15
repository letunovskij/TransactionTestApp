using TransactionTestApp.Models.ClientDtos;
using TransactionTestApp.Models.ViewModels;

namespace TransactionTestApp.Abstractions.Transactions;

public interface ITransactionService
{
    /// <summary>
    /// Вернуть транзакцию по GUID
    /// </summary>
    /// <param name="transactionId">GUID транзакции</param>
    Task<TransactionView> GetById(Guid transactionId);

    /// <summary>
    /// Создать транзакцию
    /// </summary>
    /// <param name="transactionDto">Транзакция с клиента</param>
    Task<TransactionCreatedView> Create(Transaction transactionDto);

    Task<int> GetCount();
}
