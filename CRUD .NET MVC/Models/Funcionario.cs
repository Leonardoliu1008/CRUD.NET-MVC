using System.ComponentModel.DataAnnotations;

namespace FuncionarioCRUD.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório")]
        public string Cargo { get; set; }

        [Display(Name = "Salário (R$)")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe um valor válido")]
        public decimal Salario { get; set; }

        [Display(Name = "Data de Admissão")]
        [DataType(DataType.Date)]
        public DateTime DataAdmissao { get; set; }
    }
}
