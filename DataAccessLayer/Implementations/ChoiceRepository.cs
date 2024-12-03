using DataAccessLayer.DomainModels;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Implementations;

public class ChoiceRepository : IChoiceRepository
{
    private readonly IGenericRepository repository;
    public ChoiceRepository(IGenericRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<ChoiceVM>> ChoiceGetAll()
    {
        List<ChoiceVM> Choices = new List<ChoiceVM>();

        var results = await repository.GetAll<Choice>();

        foreach (var Choice in results)
        {
            var ChoiceVM = new ChoiceVM
            {
                Id = Choice.Id,
                Choice1 = Choice.Choice1
            };

            Choices.Add(ChoiceVM);
        }

        return Choices;
    }
}
