using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class OrderItem
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int Quantity { get; set; } 
}