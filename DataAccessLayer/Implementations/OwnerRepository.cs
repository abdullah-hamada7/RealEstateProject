using DataAccessLayer.Data;
using DataAccessLayer.DomainModels;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementations;

public class OwnerRepository:IOwnerRepository
{
    private readonly IGenericRepository _genericRepository;
    private readonly RealEstateContext db;

    public OwnerRepository(IGenericRepository genericRepository, RealEstateContext realEstateContext)
    {
        _genericRepository = genericRepository;
        db = realEstateContext;
    }

    public async Task<bool> AddOwner(OwnerCreateVM model)
    {
        var owner = new Owner()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Contact = model.Contact,
            Password = model.Password,
        };
        _genericRepository.Create<Owner>(owner);
        return true;

    }

    public async Task<OwnerUpdateVM> OwnerGetById(int id)
    {
        var owner = await db.Owners.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (owner != null)
        {
            return new OwnerUpdateVM()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                Contact = owner.Contact,
                Password = owner.Password,
            };
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> OwnerUpdate(OwnerUpdateVM model)
    {
        var owner = await db.Owners.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
        if (owner != null)
        {
            owner.Id = model.Id;
            owner.FirstName = model.FirstName;
            owner.LastName = model.LastName;
            owner.Email = model.Email;
            owner.Contact = model.Contact;
            owner.Password = model.Password;
        }
        _genericRepository.Update<Owner>(owner);
        return true;
    }

    public async Task<OwnerVM> OwnerVMGetById(int id)
    {
        var owner = await db.Owners.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (owner != null)
        {
            return new OwnerVM()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                Contact = owner.Contact,
                Password = owner.Password,
            };
        }
        else
        {
            return null;
        }
    }
}
