using Backend_Assessment.Models;

namespace Radha.Repositories;

public interface IWeekendRepository
{
    public Task<Weekend> GetWeekend(int id);
}