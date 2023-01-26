using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    [Required,MaxLength(40)]
    public string Name { get; set; }
    [Required,MaxLength(20)]
    public string Phone { get; set; }
    public List<Product> Products { get; set; }
}