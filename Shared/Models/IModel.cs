using System.ComponentModel.DataAnnotations;

namespace BackendApp.Shared.Models;

public interface IModel
{
    [Key, Required, Range(0, int.MaxValue)]
    int Id { get; set; }
    public string[] GetTitles();
}