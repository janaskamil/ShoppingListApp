using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class ItemViewModel : BaseViewModel
    {
        public bool isSelected;
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
