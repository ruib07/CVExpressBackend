using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.Services.Services
{
    #region Experience Services

    public class ExperienceService : IExperienceService
    {
        private readonly CVExpressDbContext _context;

        #region Experience Service Constructors

        public ExperienceService(CVExpressDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Experience GET Services

        public async Task<List<ExperienceEfo>> GetAllExperiences()
        {
            return await _context.Experiences.ToListAsync();
        }

        public async Task<ExperienceEfo> GetExperienceById(int id)
        {
            ExperienceEfo? experience = await _context.Experiences.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experience == null)
            {
                throw new Exception("Experiência não encontrado!");
            }

            return experience;
        }

        #endregion

        #region Experience POST Service

        public async Task<ExperienceEfo> SendExperience(ExperienceEfo experience)
        {
            try
            {
                await _context.Experiences.AddAsync(experience);
                await _context.SaveChangesAsync();

                return experience;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a enviar experiência: {ex.Message}");
            }
        }

        #endregion

        #region Experience PUT Service

        public async Task<ExperienceEfo> UpdateExperience(int id, ExperienceEfo updateExperience)
        {
            try
            {
                ExperienceEfo? newExperience = await _context.Experiences
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (newExperience == null)
                {
                    throw new Exception("Experiência não encontrada!");
                }

                newExperience.Function = updateExperience.Function;
                newExperience.StartDate = updateExperience.StartDate;
                newExperience.EndDate = updateExperience.EndDate;
                newExperience.Entity = updateExperience.Entity;
                newExperience.Description = updateExperience.Description;

                await _context.SaveChangesAsync();

                return newExperience;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar experiência: {ex.Message}");
            }
        }

        #endregion

        #region Experience DELETE Service

        public async Task DeleteExperience(int id)
        {
            ExperienceEfo? experience = await _context.Experiences
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experience == null)
            {
                throw new Exception("Experiência não encontrada!");
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    #endregion
}
