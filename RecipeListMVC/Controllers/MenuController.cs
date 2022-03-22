using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using RecipeListMVC.Models;
using RecipeListMVC.Data;

namespace RecipeListMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IJSONParser _context;

        public MenuController(IJSONParser context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            Menu menu = new();
            menu.AllRecipes = _context.Recipes;
            if (HttpContext.Session.Keys.Contains("ChosenRecipes"))
            {
                var chosenRecipes = HttpContext.Session.GetString("ChosenRecipes");
                menu.ChosenRecipes = JsonSerializer.Deserialize<List<string>>(chosenRecipes);

            foreach (var recipeName in menu.ChosenRecipes)
            {
                var recipe = _context.Recipes.Find(r => r.Name == recipeName);
                foreach (var ingredient in recipe.Ingredients)
                {
                    menu.AddIngredient(ingredient);
                }
            }
         }
            menu.SortIngredients();
            return View(menu);
        }

        public IActionResult ChooseRecipe(string name)
        {
            List<string> chosenRecipes = HttpContext.Session.Keys.Contains("ChosenRecipes") ? JsonSerializer.Deserialize<List<string>>(HttpContext.Session.GetString("ChosenRecipes")) : new();
            chosenRecipes.Add(name);

            HttpContext.Session.SetString("ChosenRecipes", JsonSerializer.Serialize(chosenRecipes));
            return RedirectToAction("Index");
        }

        public IActionResult RemoveRecipe(string name)
        {
            if (!HttpContext.Session.Keys.Contains("ChosenRecipes"))
                return RedirectToAction("Index");

            List<string> chosenRecipes = JsonSerializer.Deserialize<List<string>>(HttpContext.Session.GetString("ChosenRecipes"));
            chosenRecipes.Remove(name);

            HttpContext.Session.SetString("ChosenRecipes", JsonSerializer.Serialize(chosenRecipes));
            return RedirectToAction("Index");
        }
    }
}

