using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendApp.Shared.Models
{
    public class DivisionModel : IModel
    {
        private const string c_toString = "Division";

        [Key]
        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid input")]
        public int Id { get; set; } = 0;
        public override string ToString() => c_toString;
        public string[] GetTitles() => new[] { "Id" };
    }
}
