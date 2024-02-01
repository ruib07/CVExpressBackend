using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace CVExpress.Entities.Efos
{
    public class UsersEfo
    {
        private string _password = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public DateOnly BirtDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Country {  get; set; } = string.Empty;
        public string Nationality {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 9)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{9,}$",
            ErrorMessage = "A {0} deve conter pelo menos 9 caracteres, uma maiúscula, uma minúscula, um número e um caracter especial.")]
        public string Password
        {
            get => _password;
            set
            {
                if (value != null)
                {
                    if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{9,}$"))
                    {
                        _password = value;
                    }
                    else
                    {
                        throw new ArgumentException("A password não atende aos requisitos de complexidade.");
                    }
                }
            }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUser_Id { get; set; }

        public RegisterUsersEfo RegisterUsers {  get; set; }
    }
}
