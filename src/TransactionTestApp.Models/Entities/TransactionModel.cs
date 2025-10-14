using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTestApp.Models.Entities;

public sealed class TransactionModel
{
    public required Guid Id { get; set; }
    public required DateTime TransactionDate { get; set; }
    public required decimal Amount { get; set; }
}
