using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meta.challengeWebApi.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Canal { get; set; }
        public string Valor { get; set; }
        public string Obs { get; set; }
    }
}
