using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeListMVC.Models;

namespace RecipeListMVC.Models
{
    public class Menu
    {
        public List<Recipe> AllRecipes { get; set; }
        public List<string> ChosenRecipes { get; set; }
        public List<Ingredients> Ingredients { get; private set; }

        public Menu()
        {
            AllRecipes = new();
            ChosenRecipes = new();
            Ingredients = new();
        }
        public void AddIngredient(Ingredients ingredient)
        {
            foreach (var ingred in Ingredients)
            {
                if (ingred.Name == ingredient.Name && ingred.Units == ingredient.Units)
                {
                    ingred.Quantity += ingredient.Quantity;
                    return;
                }
            }
            Ingredients.Add(ingredient);
        }

        public void SortIngredients()
        {
            Ingredients = Ingredients.OrderBy(x => x.Name).ToList();
        }
    }
}
