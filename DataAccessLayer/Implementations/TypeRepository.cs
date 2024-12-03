using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using DataAccessLayer.DomainModels;
using Type = DataAccessLayer.DomainModels.Type;

namespace DataAccessLayer.Implementations;

public class TypeRepository : ITypeRepository
{
    private readonly IGenericRepository repository;
    public TypeRepository(IGenericRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<TypeVM>> TypeGetAll()
    {
        List<TypeVM> types = new List<TypeVM>();

        var results = await repository.GetAll<Type>();

        foreach (var type in results)
        {
            var typeVM = new TypeVM
            {
                Id = type.Id,
                Type1 = type.Type1
            };

            types.Add(typeVM);
        }

        return types;
    }
}
