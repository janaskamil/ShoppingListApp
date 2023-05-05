using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Database
{
    public class ShoppingListDBContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientForMeal> IngredientForMeal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"FileName={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ShoppingListDB.sqlite")}");
        }
    }   
}
