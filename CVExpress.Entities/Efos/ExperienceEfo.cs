using System.ComponentModel.DataAnnotations.Schema;

namespace CVExpress.Entities.Efos
{
    public class ExperienceEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Function { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Entity { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        public UsersEfo Users { get; set; }
    }
}
