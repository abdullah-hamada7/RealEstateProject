using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Interfaces;

public interface IChoiceRepository
{
    Task<List<ChoiceVM>> ChoiceGetAll();
}
