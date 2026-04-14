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
            return RedirectToAction("Details", "Person", new {Id = personId});
        }
        var person = await _personRepo.ReadAsync(personId);
        ViewData["Person"] = person;
        return View(recommendation);
    }

    public async Task<IActionResult> Edit(int id, int recId)
    {
        Person? person = await _personRepo.ReadAsync(id);
        if(person == null)
        {
            return RedirectToAction("Index", "Person");
        }
        Recommendation? recommendation = 
            person.Recommendations.FirstOrDefault(r => r.Id == recId);
        if(recommendation == null)
        {
            return RedirectToAction("Details", "Person", new {Id = id});
        }
        ViewData["Person"] = person;
        return View(recommendation);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int personId, Recommendation recommendation)
    {
        if(ModelState.IsValid)
        {
            await _personRepo.UpdateRecommendationAsync(recommendation);
            return RedirectToAction("Details", "Person", new {Id = personId});
        }
        var person = await _personRepo.ReadAsync(personId);
        ViewData["Person"] = person;
        return View(recommendation);
    }

    public async Task<IActionResult> Delete(int id, int recId)
    {
        Person? person = await _personRepo.ReadAsync(id);
        if(person == null)
        {
            return RedirectToAction("Index", "Person");
        }
        Recommendation? recommendation = 
            person.Recommendations.FirstOrDefault(r => r.Id == recId);
        if(recommendation == null)
        {
            return RedirectToAction("Details", "Person", new {Id = id});
        }
        ViewData["Person"] = person;
        return View(recommendation);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int personId, int recommendationId)
    {
        await _personRepo.DeleteRecommendationAsync(recommendationId);
        return RedirectToAction("Details", "Person", new {Id = personId});
    }
}