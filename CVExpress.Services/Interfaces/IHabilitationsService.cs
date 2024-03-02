using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    #region Habilitations Interface
    public interface IHabilitationsService
    {
        #region Habilitations Interface Tasks

        Task<List<HabilitationsEfo>> GetAllHabilitations();
        Task<HabilitationsEfo> GetHabilitationById(int id);
        Task<HabilitationsEfo> SendHabilitation(HabilitationsEfo habilitation);
        Task<HabilitationsEfo> UpdateHabilitation(int id,  HabilitationsEfo updateHabilitation);
        Task DeleteHabilitation(int id);

        #endregion
    }

    #endregion
}
