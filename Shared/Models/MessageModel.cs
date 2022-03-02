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
        private const string c_toString = "Message";

        public static readonly string GetAllQuery = "SELECT * FROM message";
        public static readonly string GetQuery = GetAllQuery + " WHERE id = @Id";
        public static readonly string PostQuery =
            "INSERT INTO message (id, content, divisionId, done) VALUES (@Id, @Content, @DivisionId, @Done)";
        public static readonly string UpdateQuery =
            "UPDATE message SET content = @Content, divisionId = @DivisionId, done=@Done WHERE id=@Id";
        //INSERT INTO 
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
        public new Type GetType() => typeof(MessageModel);
    }
}
