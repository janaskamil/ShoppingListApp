using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Database;

namespace ShoppingApp.Core
{
    public class DatabaseCreationTool
    {
        public static ShoppingListDBContext MyDatabase { get; set; }

    }
}
