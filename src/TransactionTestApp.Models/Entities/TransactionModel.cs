namespace TransactionTestApp.Models.Entities;

public sealed class TransactionModel
{
    public required Guid Id { get; set; }

    public required DateTime TransactionDate { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime InsertedDate { get; set; }
}
