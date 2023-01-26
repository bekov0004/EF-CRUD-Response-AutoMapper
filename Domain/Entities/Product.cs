using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string ProductName { get; set; }
    [Required]
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public int TotalAmount { get; set; }
    public List<OrderItem> Type { get; set; } 
}