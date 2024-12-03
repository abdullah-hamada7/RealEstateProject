using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Interfaces;

public interface IOwnerRepository
{
    Task<bool> AddOwner(OwnerCreateVM model);
    Task<OwnerUpdateVM> OwnerGetById(int id);
    Task<bool> OwnerUpdate(OwnerUpdateVM model);
    Task<OwnerVM> OwnerVMGetById(int id);
}
