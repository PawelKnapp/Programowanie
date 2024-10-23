using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ContactController : Controller
{

    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Id = 1,
                FirstName = "Paweł",
                LastName = "Knap",
                Email = "pk@wsei.edu.p",
                PhoneNumber = "123 123 123",
                BirthDate = new DateOnly(2003, 06, 17)
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2,
                FirstName = "Mateusz",
                LastName = "Duda",
                Email = "md@wsei.edu.p",
                PhoneNumber = "123 321 123",
                BirthDate = new DateOnly(2004, 06, 21)
            }
        },
        {
            3,
            new ContactModel()
            {
                Id = 3,
                FirstName = "Dominik",
                LastName = "Korbiel",
                Email = "dk@wsei.edu.p",
                PhoneNumber = "123 123 321",
                BirthDate = new DateOnly(2003, 09, 22)
            }
        }
    };

    private static int currentId = 3;
    
    // Lista kontaktów, przycisk dodawania kontaktu
    public IActionResult Index()
    {
        return View(_contacts);
    }
    
    // Formularz dodawania kontaktu
    public IActionResult Add()
    {
        return View();
    }

    // Odebranie danych z formularza, walidacja i dodanie kontaktu do kolekcji
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            // wyświetlenie ponowne formularza z błędami
            return View(model);
        }

        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Details()
    {
        throw new NotImplementedException();
    }
}