using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class DeleteUnusedIngredientsViewModel : BaseViewModel
    {
        public ObservableCollection<IngreditenViewModel> Ingredientsxd { get; set; } = new ObservableCollection<IngreditenViewModel>();

        public DeleteUnusedIngredientsViewModel()
        {
            
        }
    }
}
