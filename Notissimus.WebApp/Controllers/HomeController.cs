using Microsoft.AspNetCore.Mvc;
using Notissimus.WebApp.Models;
using System.Diagnostics;
using Notissimus.Abstractions.Core;

namespace Notissimus.WebApp.Controllers;

public class HomeController : Controller
{
    private const int SelectedOfferId = 12344;
    
    private readonly IOfferService _offerService;

    public HomeController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    public async Task<IActionResult> Index()
    {
        var offer = await _offerService.ParseAndSaveOffer(SelectedOfferId);
        return View(offer);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}