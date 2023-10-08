using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class MealViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string MealName { get; set; }

        public string MealRecipe { get; set; }

        public string MealType { get; set; }

        public int MealCount { get; set; } = 1;

    }
}
