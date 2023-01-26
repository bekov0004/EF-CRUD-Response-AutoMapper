using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    [Required,MaxLength(20)]
    public string OrderNumbrer { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Type { get; set; }
}