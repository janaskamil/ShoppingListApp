using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Database
{
    public class Meal
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)] public int Id { get; set; }
 
        [Required] public string MealName { get; set; }
        
        public string MealRecipe { get; set; }

        public string MealType { get; set; }
    }
}
