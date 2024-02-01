using System.ComponentModel.DataAnnotations.Schema;

namespace CVExpress.Entities.Efos
{
    public class HabilitationsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Designation {  get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Institution { get; set; } = string.Empty;
        public string FormationArea {  get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        public UsersEfo Users { get; set; }
    }
}
