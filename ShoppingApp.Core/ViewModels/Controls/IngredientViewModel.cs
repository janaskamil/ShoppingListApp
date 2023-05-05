using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    internal class IngreditenViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
