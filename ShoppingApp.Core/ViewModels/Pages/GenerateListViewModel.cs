using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class GenerateListViewModel : BaseViewModel
    {
        public ObservableCollection<MealViewModel> MealsForShoppingList { get; set; } = new ObservableCollection<MealViewModel>();
        public int[] MealQuantity { get; set; } = new int[] {1,2,3,4,5,6,7,8,9,10};
        public int chosenQuantity { get; set; } = 1;
        public GenerateListViewModel()
        {
            ReloadVMTables();
            SaveListCommand = new RelayCommand(AddMealToMealList);
        }
        private void AddMealToMealList()
        {          
            if(SelectedMeal != null)
            {    
                for(int i = 1; i <= chosenQuantity; i++)
                {
                    var temp = MealsForShoppingList.FirstOrDefault(x => x.Id == SelectedMeal.Id);
                    if (temp != null)
                    {                      
                        temp.MealCount++;
                    }
                    else 
                    {
                        MealsForShoppingList.Add(SelectedMeal);
                    }                   
                }         
            }
            ReloadVMTables();
        }

    }
}
