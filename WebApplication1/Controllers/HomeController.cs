using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /*
     * Zadanie 1
     *Zdefiniuj metodę z widokiem Calculator
     * Dodaj link nawigacji do tej metody
     *
     * Zadanie 2
     * Dodaj do kalkulatora:
     * operator pow,który podnosi x do potęgi y
     * funkcje sin,która oblicza sin(x),y jest zbędne
     */
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Calculator(Operator? op, double? x, double? y)
    {
        /*var op=Request.Query["op"];
        var x=double.Parse(Request.Query["x"]);
        var y=double.Parse(Request.Query["y"]);*/
        if (op is null)
        { 
            ViewBag.ErrorMessage = "Niepoprawny operator"; 
            return View("CalculatorError");
        }

        if (!(op == Operator.sin && x is not null))
        if (x is null || y is null ) 
        {
            ViewBag.ErrorMessage = "Nie poprawny format liczby w parametrze X lub Y";
            return View("CalculatorError");
        }
        
        
        switch (op)
        {
            case Operator.add:
                ViewBag.Result = x + y;
                break;

            case Operator.sub:
                ViewBag.Result = x - y;
                break;

            case Operator.mul:
                ViewBag.Result = x * y;
                break;

            case Operator.div:
                ViewBag.Result = x / y;
                break;
            case Operator.pow:
                var xpom = x;
                for (int i = 1; i < y; i++)
                {
                    xpom = xpom * x;
                }
                
                ViewBag.Result = xpom;
                break;
            case Operator.sin:
                ViewBag.Result = Math.Sin((double)x);
                break;
        }

        return View();
    }
    

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public enum Operator
    {
        add, sub, mul, div, pow, sin
    }
}