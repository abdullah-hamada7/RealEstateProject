using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public class ServiceModules
{
    public static void Regsiter(IServiceCollection services)
    {
        services.AddTransient<IGenericRepository, GenericRepository>();
        services.AddTransient<ILoginRepository, LoginRepository>();
        services.AddTransient<IOwnerRepository, OwnerRepository>();
        services.AddTransient<IChoiceRepository, ChoiceRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<IPropertyRepository, PropertyRepository>();
        services.AddTransient<IHomeRepository, HomeRepository>();
    }
}
