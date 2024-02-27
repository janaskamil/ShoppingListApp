using ShoppingApp.Core.Helpers;
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
        public ObservableCollection<MealViewModel> MealsToChooseFrom { get; set; } = new ObservableCollection<MealViewModel>();
        public int[] MealQuantity { get; set; } = new int[] {1,2,3,4,5,6,7,8,9,10};
        //setting default quanitity to 2 (1 day for 2 people)
        public int chosenQuantity { get; set; } = 2;

        public GenerateListViewModel()
        {
            //reloading vms
            ReloadVMTables();
            AddMealToListCommand = new RelayCommand(AddMealToMealsForShoppingList);
            GenereteShoppingListCommand = new RelayCommand(GenerateShoppingList);
            DeleteShoppingListCommand = new RelayCommand(DeleteShoppingList);
            DeleteShoppingListWithIngredientsCommand = new RelayCommand(DeleteShoppingListWithIngredients);

            //create list of meals to choose from what to add into shoppinglist
            foreach(var item in MealsList)
            {
                MealsToChooseFrom.Add(item);
                item.isSelected = false;
            }
        }
        private void AddMealToMealsForShoppingList()
        {        
            var isSelectedMeal = MealsToChooseFrom.FirstOrDefault(x => x.isSelected);
            //if there is no selected meal nothing will save on the list
            if(isSelectedMeal != null)
            {    
                //creating 1 entity with quantity of that meal to multiply ingredients later
                for(int i = 1; i <= chosenQuantity; i++)
                {
                    //adding count to existing meal 
                    var temp = MealsForShoppingList.FirstOrDefault(x => x.Id == isSelectedMeal.Id);
                    if (temp != null)
                    {                      
                        temp.MealCount++;
                    }
                    else 
                    {
                        MealsForShoppingList.Add(isSelectedMeal);
                    }                
                }
                //unselecting meal
                isSelectedMeal.isSelected = false;
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
                            var mealIngredientExistingOnList = IngredientsToBuy.FirstOrDefault(x => x.IngredientId == position.IngredientId && string.Equals(x.Unit, position.Unit, StringComparison.OrdinalIgnoreCase));
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
                                IngredientsToBuy.Add(newIngredientOnList);
                            }
                            else
                            {
                                mealIngredientExistingOnList.Quantity = position.Quantity + mealIngredientExistingOnList.Quantity;
                            }
                        }                                                                              
                    }                                     
                }
            }
        }

        private void DeleteShoppingListWithIngredients()
        {
            IngredientsToBuy.Clear();
            MealsForShoppingList.Clear();
            ReloadVMTables();
        }

        private void DeleteShoppingList()
        {
            var mealFromList = MealsForShoppingList.Where(x => x.isChecked).ToList();
            foreach(var meals in mealFromList)
            {
                MealsForShoppingList.Remove(meals);
            }

        }
    }
}
