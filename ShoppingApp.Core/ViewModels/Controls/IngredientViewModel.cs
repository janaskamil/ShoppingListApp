using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class IngredientViewModel : BaseViewModel
    {       
        public bool isSelected { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
