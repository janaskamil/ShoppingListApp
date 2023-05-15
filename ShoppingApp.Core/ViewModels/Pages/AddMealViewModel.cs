using Microsoft.EntityFrameworkCore;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class AddMealViewModel : BaseViewModel
    {
        public ObservableCollection<MealViewModel> MealsList { get; set; } = new ObservableCollection<MealViewModel>();
        public ObservableCollection<IngreditenViewModel> IngedientsListVM { get; set; } = new ObservableCollection<IngreditenViewModel>();
        public ObservableCollection<IngredientForMealViewModel> IngredientsForMealVM { get; set; } = new ObservableCollection<IngredientForMealViewModel>();
        public MealViewModel SelectedMeal { get; set; }
        public string[] MealTypesList { get; set; } = new string[] {"Breakfast", "Lunch", "Dinner", "Tea", "Supper"};

        public string AddMealName { get; set; }
        public string AddMealType { get; set; }
        public string AddMealRecipe { get; set; }
        public string AddIngredientForMeal1 { get; set; }
        public ICommand SaveMealCommand { get; set; }
        public ICommand DeleteMealCommand { get; set; }
        public AddMealViewModel()
        {
            DatabaseCreationTool.MyDatabase.Meals.ToList();
            SaveMealCommand = new RelayCommand(SaveMeal);
            DeleteMealCommand = new RelayCommand(DeleteMeal);
            foreach (var meal in DatabaseCreationTool.MyDatabase.Meals.ToList())
            {           
                MealsList.Add(new MealViewModel
                {
                    Id = meal.Id,
                    MealName = meal.MealName,
                    MealRecipe = meal.MealRecipe,
                    MealType = meal.MealType
                });
            }
            foreach (var ingredient in DatabaseCreationTool.MyDatabase.Ingredients.ToList())
            {
                IngedientsListVM.Add(new IngreditenViewModel
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name
                });

            }

            foreach(var meal in MealsList)
            {
                int tempId = 1;
                var query = from ingredientForMeal in DatabaseCreationTool.MyDatabase.IngredientForMeal
                            join ingredient in DatabaseCreationTool.MyDatabase.Ingredients
                            on ingredientForMeal.IngredientId equals ingredient.Id
                            where ingredientForMeal.MealId == meal.Id
                            select new
                            {
                                ingredientForMeal.Id,
                                ingredientForMeal.MealId,
                                ingredientForMeal.IngredientId,
                                IngredientName = ingredient.Name,
                                ingredientForMeal.Quantity,
                                ingredientForMeal.Unit
                            };

                foreach (var item in query)
                {
                    IngredientsForMealVM.Add(new IngredientForMealViewModel
                    {
                        tempId = tempId,
                        Id = item.Id,
                        MealId = item.MealId,
                        IngredientId = item.IngredientId,
                        IngredientName = item.IngredientName,
                        Quantity = item.Quantity,
                        Unit = item.Unit
                    });
                    tempId++;
                }
            }          
        }


        public string MealRecipe
        {
            get
            {
                if(SelectedMeal != null)
                {
                    return SelectedMeal.MealRecipe;
                }
                else
                {
                    return AddMealRecipe;
                }
                          
            }
            set
            {
                if (SelectedMeal != null)
                {
                    SelectedMeal.MealRecipe = value;
                }
                else
                {
                    AddMealRecipe = value;
                }
  
            }
        }

        public string MealType
        {
            get
            {
                if (SelectedMeal != null)
                {
                    return SelectedMeal.MealType;
                }
                else
                {
                    return AddMealType;
                }
            }
            set 
            {
                if (SelectedMeal != null)
                {
                    SelectedMeal.MealType = value;
                }
                else
                {
                    AddMealType = value;
                }
            }
        }
        //method to save new and existing meals
        public void SaveMeal()
        {
            if ((!String.IsNullOrEmpty(AddMealName)))
            {
                if (AddMealName.Equals(SelectedMeal?.MealName.ToString()))
                {
                    var mealToUpdate = DatabaseCreationTool.MyDatabase.Meals.Find(SelectedMeal.Id);
                    mealToUpdate.MealRecipe = SelectedMeal.MealRecipe;
                    mealToUpdate.MealType = SelectedMeal.MealType;
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    //clear everything to force user to select new meal to create/edit after saving
                    AddMealName = null;
                    AddMealRecipe = null;
                    AddMealType = null;
                }
                //save new recipe
                else
                {
                    var countedMealsId = MealsList.ToList();
                    int IdTemp;
                    if (countedMealsId.Count > 0)
                    {
                        IdTemp = DatabaseCreationTool.MyDatabase.Meals.OrderByDescending(m => m.Id).First().Id + 1;
                    }
                    else
                    {
                        IdTemp = 1;
                    }
                    
                    var NewMeal = new MealViewModel
                    {
                        Id = IdTemp,
                        MealName = AddMealName,
                        MealRecipe = AddMealRecipe,
                        MealType = AddMealType
                    };

                    MealsList.Add(NewMeal);
                    DatabaseCreationTool.MyDatabase.Meals.Add(new Database.Meal
                    {
                        Id = NewMeal.Id,
                        MealName = NewMeal.MealName,
                        MealRecipe = NewMeal.MealRecipe,
                        MealType = NewMeal.MealType
                    });

                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    //clear everything to force user to select new meal to create/edit after saving
                    AddMealName = null;
                    AddMealRecipe = null;
                    AddMealType = null;

                }
            }                    
        }

        //deleting existing meals
        public void DeleteMeal()
        {
            if (SelectedMeal != null)
            {
                var mealToDelete = DatabaseCreationTool.MyDatabase.Meals.Find(SelectedMeal.Id);
                DatabaseCreationTool.MyDatabase.Meals.Remove(mealToDelete);
                DatabaseCreationTool.MyDatabase.SaveChanges();
                MealsList.Remove(SelectedMeal);
                //clear everything to force user to select new meal after deleting
                AddMealName = null;
                AddMealRecipe = null;
                AddMealType = null;
            }
            else
            {
                //temporary - messagebox eventually
                AddMealName = null;
                AddMealRecipe = null;
                AddMealType = null;
            }
                
        }

        public string IngredientForSelectedMeal1
        {
            get
            {
                if (SelectedMeal != null)
                {
                    var ingredient = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == SelectedMeal.Id);
                    if (ingredient != null)
                    {
                        return ingredient.IngredientName.ToString();
                    }
                    else
                    {
                        return AddIngredientForMeal1;
                    }              
                }
                else
                {
                    return AddIngredientForMeal1;
                }
            }
            set
            {              
                if (SelectedMeal != null)
                {
                    var ingredient = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == SelectedMeal.Id);
                    if(ingredient != null)
                    {
                        int? ingredientId = ingredient.Id;
                        int? ingedient1IngId = ingredient.IngredientId;
                        string? ingedient1Name = ingredient.IngredientName;
                    }
                    else
                    {

                    }
                    AddIngredientForMeal1 = value;

                }
                else
                {
                    AddIngredientForMeal1 = value;
                }
            }
        }
        public string IngredientForMeal2 { get; set; }
       

    }          
}
