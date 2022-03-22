using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeListMVC.Models;
using RecipeListMVC.Data;

namespace RecipeListMVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IJSONParser _context;

        public RecipeController(IJSONParser context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Recipes);
        }

        public IActionResult EditIngredient(string recipeName, string ingredName)
        {
            var recipe = _context.Recipes.Find(r => r.Name == recipeName);
            var ingred = recipe.Ingredients.Find(i => i.Name == ingredName);
            int index = recipe.Ingredients.IndexOf(ingred);

            TempData["ingredIndex"] = index;
            if (recipe == null)
                return NotFound();

            return View("CreateOrEdit", recipe);
        }

        public IActionResult CreateOrEdit()
        {
            return View("CreateOrEdit", new Recipe());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveJSON();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(string name)
        {
            var selectedRecipe = _context.Recipes.Find(r => r.Name == name);

            if (selectedRecipe == null)
                return NotFound();

            return View("CreateOrEdit", selectedRecipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Recipe recipe)
        {
            string currName = String.Empty;
            if (TempData.ContainsKey("currRecipe"))
                currName = TempData["currRecipe"].ToString();

            var existingRecipe = _context.Recipes.Find(r => r.Name == currName);

            if (existingRecipe == null)
                return BadRequest();

            existingRecipe.Name = recipe.Name;
            existingRecipe.Instruction = recipe.Instruction;

            await _context.SaveJSON();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            var recipe = _context.Recipes.Find(r => r.Name == name);

            if (recipe == null)
                return NotFound();

            _context.Recipes.Remove(recipe);
            await _context.SaveJSON();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
