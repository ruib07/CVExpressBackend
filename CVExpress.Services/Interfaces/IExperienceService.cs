using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    #region Experience Interface 

    public interface IExperienceService
    {
        #region Experience Interface Tasks

        Task<List<ExperienceEfo>> GetAllExperiences();
        Task<ExperienceEfo> GetExperienceById(int id);
        Task<ExperienceEfo> SendExperience(ExperienceEfo experience);
        Task<ExperienceEfo> UpdateExperience(int id, ExperienceEfo updateExperience);
        Task DeleteExperience(int id);

        #endregion
    }

    #endregion
}
