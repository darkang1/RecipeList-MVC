using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeListMVC.Models
{
    public class FullRecipe
    {
        public Recipe Recipe { get; set; }
        public Ingredients Ingredient { get; set; }
    }
}
