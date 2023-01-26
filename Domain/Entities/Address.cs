using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

public class Address
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Address1 { get; set; }
    [Required,MaxLength(50)]
    public string Address2 { get; set; }
    [Required,MaxLength(50)]
    public string CityName { get; set; }
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }   
}
