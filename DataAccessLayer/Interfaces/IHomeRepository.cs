using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Interfaces;

public interface IHomeRepository
{
    Task<List<PropertyVM>> PropertyList();
    Task<PropertyTypesCountVM> PropertyTypeCount();
    Task<List<PropertyVM>> PropertyListOfSearchBar(HomeSearchVM model);
}
