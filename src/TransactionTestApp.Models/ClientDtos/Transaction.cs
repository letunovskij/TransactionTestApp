using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTestApp.Models.ClientDtos
{
    public sealed record Transaction
    {
        public required Guid Id { get; init; }
        public required DateTime TransactionDate { get; init; }
        public required decimal Amount { get; init; }
    }
}
