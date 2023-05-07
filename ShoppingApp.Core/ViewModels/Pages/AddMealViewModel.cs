using Microsoft.EntityFrameworkCore;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class AddMealViewModel : BaseViewModel
    {
        public ObservableCollection<MealViewModel> MealsList { get; set; } = new ObservableCollection<MealViewModel>();
        public string[] MealTypesList { get; set; } = new string[] {"Breakfast", "Lunch", "Dinner", "Tea", "Supper"};

        public string AddMealName { get; set; }
        public string AddMealType { get; set; }
        public string AddMealRecipe { get; set; }
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
        }
        public MealViewModel SelectedMeal { get; set; }

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
    }          
}
