using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Database;

namespace ShoppingApp.Core
{
    public class IngredientForMealViewModel : BaseViewModel
    {
        public int tempId { get; set; } 
        public int Id { get; set; }
        public int MealId { get; set; }  
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
