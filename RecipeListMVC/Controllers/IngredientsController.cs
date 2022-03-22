using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeListMVC.Models;
using RecipeListMVC.Data;

namespace RecipeListMVC.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IJSONParser _context;

        public IngredientsController(IJSONParser context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(string recipeName, string name, decimal quantity, string units)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Name == recipeName);

            if (recipe == null)
                return NotFound();

            var ingredient = recipe.Ingredients.FirstOrDefault(i => i.Name == name && i.Units == units);

            if (ingredient != null)
                ingredient.Quantity += quantity;
            else
                recipe.Ingredients.Add(new Ingredients { Name = name, Quantity = quantity, Units = units });

            await _context.SaveJSON();

            return RedirectToAction("Edit", "Recipe", new { name = recipeName });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string recipeName, string name, decimal quantity, string units)
        {
            string currIngred = String.Empty;
            if (TempData.ContainsKey("currIngred"))
                currIngred = TempData["currIngred"].ToString();

            var recipe = _context.Recipes.Find(r => r.Name == recipeName);
            var ingredient = recipe.Ingredients.Find(i => i.Name == currIngred);
            int index = recipe.Ingredients.IndexOf(ingredient);

            if (recipe == null)
                return NotFound();

            if (ingredient == null)
                return NotFound();

            ingredient.Name = name;
            ingredient.Quantity = quantity;
            ingredient.Units = units;

            await _context.SaveJSON();

            return RedirectToAction("Edit", "Recipe", new { name = recipeName });
        }

        public async Task<IActionResult> Delete(string recipeName, string ingredName, string units)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Name == recipeName);

            if (recipe == null)
                return NotFound();

            var ingredient = recipe.Ingredients.FirstOrDefault(i => i.Name == ingredName && i.Units == units);

            if (ingredient == null)
                return NotFound();

            recipe.Ingredients.Remove(ingredient);
            await _context.SaveJSON();
            return RedirectToAction("Edit", "Recipe", new { name = recipeName });
        }


    }
}
