using Microsoft.AspNetCore.Mvc;
using PersonRecommendationApp.Models.Entities;
using PersonRecommendationApp.Services;

namespace PersonRecommendationApp.Controllers;

public class PersonController : Controller
{
    private readonly IPersonRepository _personRepo;

    public PersonController(IPersonRepository personRepo)
    {
        _personRepo = personRepo;
    }

    public async Task<IActionResult> Index()
    {
        var people = await _personRepo.ReadAllAsync();
        return View(people);
    }

    public async Task<IActionResult> Details(int id)
    {
        Person? person = await _personRepo.ReadAsync(id);
        if(person == null)
        {
            return RedirectToAction("Index");
        }
        return View(person);
    }
}