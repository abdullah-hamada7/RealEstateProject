using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.Interfaces;

public interface IPropertyRepository
{
    Task<bool> IsImageFile(IFormFile file);
    Task<bool> AddProperty(PropertyCreateVM model);
    Task<List<PropertyVM>> PropertyListForOwner(int id);
    Task<List<PropertyVM>> PropertyListForType(int id);
    Task<List<PropertyVM>> PropertyListForChoice(int id);
    Task<PropertyDetailsVM> PropertyDetails(int id);
    Task<bool> PropertyDelete(int id);
}
