using System.ComponentModel.DataAnnotations;

namespace BulkyBookModels;

public class Company
{
    [Required] 
    public int Id { get; set; }

    public string Name { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public string? PostalCode { get; set; }
    
    public string? PhoneNumber { get; set; }
}