using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        //setting default quanitity to 2 (1 day for 2 people)
        public int chosenQuantity { get; set; } = 2;

        public GenerateListViewModel()
        {
            //reloading vms
            ReloadVMTables();
            AddMealToListCommand = new RelayCommand(AddMealToMealsForShoppingList);
            GenereteShoppingListCommand = new RelayCommand(GenerateShoppingList);
        }
        private void AddMealToMealsForShoppingList()
        {          
            //if there is no selected meal nothing will save on the list
            if(SelectedMeal != null)
            {    
                //creating 1 entity with quantity of that meal to multiply ingredients later
                for(int i = 1; i <= chosenQuantity; i++)
                {
                    //adding count to existing meal 
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
            chosenQuantity = 2;
        }
        private void GenerateShoppingList()
        {
            //declaring moment of list generation
            DateTime currentTimestamp = DateTime.Now;
            //adding ingredient to shopping list
            foreach (var meal in MealsForShoppingList)
            {               
                var existingPosition = IngredientsForMealVM.Where(x => x.MealId == meal.Id);
                if(existingPosition != null)
                {
                    foreach (var position in existingPosition)
                    {              
                        for(int i = 1; i <= meal.MealCount; i++)
                        {
                            var mealIngredientExistingOnList = IngredientsToListVM.FirstOrDefault(x => x.IngredientId == position.IngredientId && string.Equals(x.Unit, position.Unit, StringComparison.OrdinalIgnoreCase));
                            if (mealIngredientExistingOnList == null)
                            {
                                var newIngredientOnList = new IngredientsToListViewModel
                                {
                                    IngredientId = position.IngredientId,
                                    IngredientName = position.IngredientName,
                                    Quantity = position.Quantity,
                                    Unit = position.Unit,
                                    Regdate = currentTimestamp
                                };
                                IngredientsToListVM.Add(newIngredientOnList);
                            }
                            else
                            {
                                mealIngredientExistingOnList.Quantity = position.Quantity + mealIngredientExistingOnList.Quantity;
                            }
                        }                                                                              
                    }                                     
                }
            }
            //Debug stop
            Console.WriteLine("");
        }
    }
}
