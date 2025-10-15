using Mapster;
using TransactionTestApp.Models.ClientDtos;
using TransactionTestApp.Models.Entities;
using TransactionTestApp.Models.ViewModels;

namespace TransactionTestApp.WebApi.Config.MappingProfiles;

public class TransactionMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransactionModel, TransactionView>();

        config.NewConfig<Transaction, TransactionModel>();
    }
}
