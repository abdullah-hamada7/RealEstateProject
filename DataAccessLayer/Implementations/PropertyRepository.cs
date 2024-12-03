using DataAccessLayer.Data;
using DataAccessLayer.DomainModels;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementations;

public class PropertyRepository : IPropertyRepository
{
    private readonly IGenericRepository genericRepository;
    private readonly RealEstateContext db;

    public PropertyRepository(IGenericRepository genericRepository, RealEstateContext realEstateContext)
    {
        this.genericRepository = genericRepository;
        db = realEstateContext;
    }

    //method for checking image file
    public async Task<bool> IsImageFile(IFormFile file)
    {
        if (file == null)
        {
            return false;
        }

        // Check if the file has a valid image extension
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".jfif", ".webp" };
        string extension = Path.GetExtension(file.FileName).ToLower();

        return allowedExtensions.Contains(extension);
    }

    public async Task<bool> AddProperty(PropertyCreateVM model)
    {
        var property = new Property()
        {
            Title = model.Title,
            Price = model.Price,
            Image = model.Image,
            Size = model.Size,
            Baths = model.Baths,
            Rooms = model.Rooms,
            Address = model.Address,
            ChoiceId = model.ChoiceId,
            TypeId = model.TypeId,
            OwnerId = model.OwnerId,
        };

        await genericRepository.Create<Property>(property);
        return true;
    }


    //Property list for owner
    public async Task<List<PropertyVM>> PropertyListForOwner(int id)
    {

        var results = await db.Properties.Where(p => p.OwnerId == id)
         .Select(p => new PropertyVM
         {
             Id = p.Id,
             Title = p.Title,
             Price = p.Price,
             Address = p.Address,
             Baths = p.Baths,
             Rooms = p.Rooms,
             Size = p.Size,
             Image = p.Image,
             Choice = p.Choice != null ? p.Choice.Choice1 : "N/A",
             type = p.Type != null ? p.Type.Type1 : "N/A",
             owner = p.Owner != null ? p.Owner.FirstName : "N/A"
         })
         .ToListAsync();

        return results;
    }

    //Property list for differnt type on the basis of type ID for home page
    public async Task<List<PropertyVM>> PropertyListForType(int id)
    {

        var results = await db.Properties.Where(p => p.TypeId == id)
         .Select(p => new PropertyVM
         {
             Id = p.Id,
             Title = p.Title,
             Price = p.Price,
             Address = p.Address,
             Baths = p.Baths,
             Rooms = p.Rooms,
             Size = p.Size,
             Image = p.Image,
             Choice = p.Choice != null ? p.Choice.Choice1 : "N/A",
             type = p.Type != null ? p.Type.Type1 : "N/A",
             owner = p.Owner != null ? p.Owner.FirstName : "N/A"
         })
         .ToListAsync();

        return results;
    }

    public async Task<List<PropertyVM>> PropertyListForChoice(int id)
    {

        var results = await db.Properties.Where(p => p.ChoiceId == id)
         .Select(p => new PropertyVM
         {
             Id = p.Id,
             Title = p.Title,
             Price = p.Price,
             Address = p.Address,
             Baths = p.Baths,
             Rooms = p.Rooms,
             Size = p.Size,
             Image = p.Image,
             Choice = p.Choice != null ? p.Choice.Choice1 : "N/A",
             type = p.Type != null ? p.Type.Type1 : "N/A",
             owner = p.Owner != null ? p.Owner.FirstName : "N/A"
         })
         .ToListAsync();

        return results;
    }

    //Property details
    public async Task<PropertyDetailsVM> PropertyDetails(int id)
    {

        var result = await db.Properties.Where(p => p.Id == id)
         .Select(p => new PropertyDetailsVM
         {
             Id = p.Id,
             Title = p.Title,
             Price = p.Price,
             Address = p.Address,
             Baths = p.Baths,
             Rooms = p.Rooms,
             Size = p.Size,
             Image = p.Image,
             Choice = p.Choice != null ? p.Choice.Choice1 : "N/A",
             Type = p.Type != null ? p.Type.Type1 : "N/A",
             ownerName = p.Owner != null ? p.Owner.FirstName + " " + p.Owner.LastName : "N/A",
             ownerContact = p.Owner != null ? p.Owner.Contact : "N/A",
             ownerEmail = p.Owner != null ? p.Owner.Email : "N/A"
         })
         .FirstOrDefaultAsync();

        return result;
    }

    //Property delete
    public async Task<bool> PropertyDelete(int id)
    {
        var result = await db.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (await genericRepository.Delete(result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

