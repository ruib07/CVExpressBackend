using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    public interface IExperienceService
    {
        Task<List<ExperienceEfo>> GetAllExperiences();
        Task<ExperienceEfo> GetExperienceById(int id);
        Task<ExperienceEfo> SendExperience(ExperienceEfo experience);
        Task<ExperienceEfo> UpdateExperience(int id, ExperienceEfo updateExperience);
        Task DeleteExperience(int id);
    }
}
