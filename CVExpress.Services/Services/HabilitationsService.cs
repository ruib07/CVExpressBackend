using CVExpress.Entities.Efos;
using CVExpress.EntityFramework;
using CVExpress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.Services.Services
{
    #region Habilitations Service

    public class HabilitationsService : IHabilitationsService
    {
        private readonly CVExpressDbContext _context;

        #region Habilitations Service Constructors

        public HabilitationsService(CVExpressDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Habilitations GET Services

        public async Task<List<HabilitationsEfo>> GetAllHabilitations()
        {
            return await _context.Habilitations.ToListAsync();
        }

        public async Task<HabilitationsEfo> GetHabilitationById(int id)
        {
            HabilitationsEfo? habilitation = await _context.Habilitations
                .AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

            if (habilitation == null)
            {
                throw new Exception("Habilitação não encontrada!");
            }

            return habilitation;
        }

        #endregion

        #region Habilitations POST Service

        public async Task<HabilitationsEfo> SendHabilitation(HabilitationsEfo habilitation)
        {
            try
            {
                await _context.Habilitations.AddAsync(habilitation);
                await _context.SaveChangesAsync();

                return habilitation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a enviar habilitação: {ex.Message}");
            }
        }

        #endregion

        #region Habilitations PUT Service

        public async Task<HabilitationsEfo> UpdateHabilitation(int id, HabilitationsEfo updateHabilitation)
        {
            try
            {
                HabilitationsEfo? newHabilitation = await _context.Habilitations
                    .FirstOrDefaultAsync(h => h.Id == id);

                if (newHabilitation == null)
                {
                    throw new Exception("Habilitação não encontrada!");
                }

                newHabilitation.Designation = updateHabilitation.Designation;
                newHabilitation.StartDate = updateHabilitation.StartDate;
                newHabilitation.EndDate = updateHabilitation.EndDate;
                newHabilitation.Institution = updateHabilitation.Institution;
                newHabilitation.FormationArea = updateHabilitation.FormationArea;

                await _context.SaveChangesAsync();

                return newHabilitation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar habilitação: {ex.Message}");
            }
        }

        #endregion

        #region Habilitations DELETE Service

        public async Task DeleteHabilitation(int id)
        {
            HabilitationsEfo? habilitation = await _context.Habilitations
                .FirstOrDefaultAsync(h => h.Id == id);

            if (habilitation == null)
            {
                throw new Exception("Habilitação não encontrada!");
            }

            _context.Habilitations.Remove(habilitation);
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    #endregion
}
