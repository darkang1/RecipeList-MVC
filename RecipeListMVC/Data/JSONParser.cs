using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RecipeListMVC.Models;

namespace RecipeListMVC.Data
{
    public class JSONParser : IJSONParser
    {
        private string _filepath;
        public List<Recipe> Recipes { get; set; } = new();

        public JSONParser(IConfiguration configuration)
        {
            // For filepath look in appsettings.json!
            _filepath = configuration.GetSection("JsonFilePath").Value;
            ReadJSON().GetAwaiter().GetResult();
        }
        private async Task ReadJSON()
        {
            using var fs = new FileStream(_filepath, FileMode.OpenOrCreate);
            try
            {
                Recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(fs);
            }
            catch (JsonException) { } 
        } 
        public async Task SaveJSON()
        {
            using var fs = new FileStream(_filepath, FileMode.Create);
            await JsonSerializer.SerializeAsync(fs, Recipes, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
