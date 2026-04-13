using Microsoft.AspNetCore.Mvc;
using PersonRecommendationApp.Models.Entities;
using PersonRecommendationApp.Services;

namespace PersonRecommendationApp.Controllers;

public class RecommendationController : Controller
{
    private readonly IPersonRepository _personRepo;

    public RecommendationController(IPersonRepository personRepo)
    {
        _personRepo = personRepo;
    }

    public async Task<IActionResult> Create(int id)
    {
        var person = await _personRepo.ReadAsync(id);
        if(person == null)
        {
            return RedirectToAction("Index", "Person");
        }
        ViewData["Person"] = person;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int personId, Recommendation recommendation)
    {
        if(ModelState.IsValid)
        {
            await _personRepo.CreateRecommendationAsync(personId, recommendation);
            return RedirectToAction("Index", "Person");
        }
        var person = await _personRepo.ReadAsync(personId);
        ViewData["Person"] = person;
        return View(person);
    }
}