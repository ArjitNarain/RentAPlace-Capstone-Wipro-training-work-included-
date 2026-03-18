using Microsoft.AspNetCore.Mvc;
using MiddlewareMVCApp.Models;
using System.Collections.Generic;

public class ItemsController : Controller
{
    private static List<Item> items = new List<Item>
    {
        new Item { Name = "Apple" },
        new Item { Name = "Banana" }
    };

    public IActionResult Index()
    {
        return View(items);
    }

    [HttpPost]
    public IActionResult Add(Item item)
    {
        items.Add(item);
        return RedirectToAction("Index");
    }
}