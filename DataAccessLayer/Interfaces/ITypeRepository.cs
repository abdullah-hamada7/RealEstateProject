using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Interfaces;

public interface ITypeRepository
{
    Task<List<TypeVM>> TypeGetAll();
}
