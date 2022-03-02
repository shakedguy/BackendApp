using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendApp.Shared.Models
{
    public class DispatchModel : IModel
    {
        private const string c_toString = "Dispatch";

        public static readonly string GetAllQuery = "SELECT * FROM dispatch";
        public static readonly string GetQuery = GetAllQuery + " WHERE id = @Id";
        public static readonly string UpdateQuery =
            "UPDATE dispatch SET MessageId = @MessageId, AgentId = @AgentId";

        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input")]
        public int AgentId { get; set; }
        public DateTime TimeStamp { get; set; }
        public override string ToString() => c_toString;
        public string[] GetTitles() => new[] { "Id", "Message Nr", "Agent Nr", "TimeStamp" };
        public new Type GetType() => typeof(DispatchModel);
    }
}
