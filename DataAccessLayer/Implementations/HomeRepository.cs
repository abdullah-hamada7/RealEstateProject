using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementations;

public class HomeRepository:IHomeRepository
{
    public readonly IGenericRepository genericRepository;
    public readonly RealEstateContext db;
    public HomeRepository(IGenericRepository genericRepository, RealEstateContext realEstateContext)
    {
        this.genericRepository = genericRepository;
        this.db = realEstateContext;
    }


    // Method to return all property list for home page
    public async Task<List<PropertyVM>> PropertyList()
    {

        var results = await db.Properties
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


    // Method to return all property types count for home page
    public async Task<PropertyTypesCountVM> PropertyTypeCount()
    {
        PropertyTypesCountVM model = new PropertyTypesCountVM();

        model.ApartmentCount = await db.Properties.Where(p => p.TypeId == 1).CountAsync();
        model.VillCount = await db.Properties.Where(p => p.TypeId == 2).CountAsync();
        model.HomeCount = await db.Properties.Where(p => p.TypeId == 3).CountAsync();
        model.TownHouseCount = await db.Properties.Where(p => p.TypeId == 4).CountAsync();
        model.BuildingCount = await db.Properties.Where(p => p.TypeId == 5).CountAsync();
        model.OfficeCount = await db.Properties.Where(p => p.TypeId == 6).CountAsync();

        return model;
    }


    // Method to return all property list for search box on home page
    public async Task<List<PropertyVM>> PropertyListOfSearchBar(HomeSearchVM model)
    {
        var query = db.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(model.location))
        {
            query = query.Where(p => p.Address.Contains(model.location));
        }

        if (model.typeId != 0)
        {
            query = query.Where(p => p.TypeId == model.typeId);
        }

        if (model.ChoiceId != null)
        {
            query = query.Where(p => p.ChoiceId == model.ChoiceId);
        }

        var results = await query
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


}
