using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Database
{
    public class Items
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)] public int Id { get; set; }
        [Required] public string ItemName { get; set; }
    }
}
