using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class IngredientsToListViewModel
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime Regdate { get; set; }
    }
}
