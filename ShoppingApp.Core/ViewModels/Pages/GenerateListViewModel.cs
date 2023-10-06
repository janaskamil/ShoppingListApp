using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class GenerateListViewModel :BaseViewModel
    {
        private List<MealViewModel> MealsForShoppingList { get; set; } = new List<MealViewModel>();


        public GenerateListViewModel()
        {
            ReloadVMTables();

            SaveListCommand = new RelayCommand(AddMealToMealList);
        }

        private void AddMealToMealList()
        {          
            if(SelectedMeal != null)
            {
                MealsForShoppingList.Add(SelectedMeal);
            }
            ReloadVMTables();
        }

    }
}
