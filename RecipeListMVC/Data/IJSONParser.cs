using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeListMVC.Models;

namespace RecipeListMVC.Data
{
    public interface IJSONParser
    {
        public List<Recipe> Recipes { get; set; }
        public Task SaveJSON();
    }
}
