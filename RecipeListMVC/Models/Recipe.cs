using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeListMVC.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Instruction { get; set; }
        public List<Ingredients> Ingredients { get; set; } = new();
    }
}
