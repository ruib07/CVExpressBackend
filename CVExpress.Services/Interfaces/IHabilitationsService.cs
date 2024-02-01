using CVExpress.Entities.Efos;

namespace CVExpress.Services.Interfaces
{
    public interface IHabilitationsService
    {
        Task<List<HabilitationsEfo>> GetAllHabilitations();
        Task<HabilitationsEfo> GetHabilitationById(int id);
        Task<HabilitationsEfo> SendHabilitation(HabilitationsEfo habilitation);
        Task<HabilitationsEfo> UpdateHabilitation(int id,  HabilitationsEfo updateHabilitation);
        Task DeleteHabilitation(int id);
    }
}
