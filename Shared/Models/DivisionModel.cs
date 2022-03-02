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

        public static readonly string GetAllQuery = "SELECT * FROM division";
        public static readonly string GetQuery = GetAllQuery + " WHERE id = @Id";
        public static readonly string UpdateQuery =
            "UPDATE division SET id=@Id WHERE id=@Id";

        [Key]
        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid input")]
        public int Id { get; set; } = 0;
        public override string ToString() => c_toString;
        public string[] GetTitles() => new[] { "Id" };
        public new Type GetType() => typeof(DivisionModel);


    }
}
