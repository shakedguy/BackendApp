using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendApp.Shared.Models
{
    public class MessageModel : IModel
    {
        private const string c_toString = "Massage";

        [Key]
        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int DivisionId { get; set; }
        public bool Done { get; set; }
        public override string ToString() => c_toString;
        public string[] GetTitles() => new[] { "Id", "Content", "Division", "Done" };
    }
}
