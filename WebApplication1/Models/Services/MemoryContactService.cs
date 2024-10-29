namespace WebApplication1.Models.Services;

public class MemoryContactService: IContactService
{
    private Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Category = Category.Business,
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
                Category = Category.Family,
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
                Category = Category.Friends,
                Id = 3,
                FirstName = "Dominik",
                LastName = "Korbiel",
                Email = "dk@wsei.edu.p",
                PhoneNumber = "123 123 321",
                BirthDate = new DateOnly(2003, 09, 22)
            }
        }
    };

    private int currentId = 3;
    
    public void Add(ContactModel model)
    {
        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
    }

    public void Update(ContactModel model)
    {
        if (_contacts.ContainsKey(model.Id))
        {
            _contacts[model.Id] = model;
        }
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }
}