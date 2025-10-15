using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionTestApp.Abstractions.Transactions;
using TransactionTestApp.Data;
using TransactionTestApp.Models.ClientDtos;
using TransactionTestApp.Models.Entities;
using TransactionTestApp.Models.ViewModels;

namespace TransactionTestApp.Services.Transactions;

public sealed class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly TransactionDbContext _dbContext;

    public TransactionService(ILogger<TransactionService> logger,
        TransactionDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext; 
    }

    public async Task<TransactionCreatedView> Create(Transaction transactionDto)
    {
        var transaction = await _dbContext.Transactions
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == transactionDto.Id);

        if (transaction == null)
        {

            transaction = transactionDto.Adapt<TransactionModel>();
            transaction.InsertedDate = DateTime.UtcNow;

            _dbContext.Transactions.Add(transaction);

            await _dbContext.SaveChangesAsync();
        }

        return new TransactionCreatedView() { InsertDateTime = transaction.InsertedDate };
    }

    public async Task<TransactionView> GetById(Guid transactionId)
    {
        var transaction = await _dbContext.Transactions
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == transactionId);

        return transaction.Adapt<TransactionView>();
    }

    public async Task<int> GetCount() 
        => await _dbContext.Transactions.CountAsync();
}
