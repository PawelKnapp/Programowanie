using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(length: 20, ErrorMessage = "Imię nie może być większe niż 20 znaków")]
    [MinLength(length: 2, ErrorMessage = "Imię musi mieć conajmniej 2 znaki!")]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(length: 50, ErrorMessage = "Imię nie może być większe niż 50 znaków")]
    [MinLength(length: 2, ErrorMessage = "Imię musi mieć conajmniej 2 znaki!")]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    [RegularExpression("\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wpisz numer wg. wzoru: xxx xxx xxx")]
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
}