using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public enum Category
{
    [Display(Name = "Rodzina", Order = 1)]
    Family,
    [Display(Name = "Znajomi", Order = 3)]
    Friends,
    [Display(Name = "Kontakty zawodowe", Order = 2)]
    Business,
}