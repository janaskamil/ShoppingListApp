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
        public string NewMealName { get; set; }
        public string NewMealRecipe { get; set; }
        public string NewMealType { get; set; }
        public ICommand SaveMealCommand { get; set; }
        public AddMealViewModel()
        {
            DatabaseCreationTool.MyDatabase.Meals.ToList();
            foreach (var meal in DatabaseCreationTool.MyDatabase.Meals.ToList())
            {
                SaveMealCommand = new RelayCommand(SaveMeal);
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
                return SelectedMeal?.MealRecipe;
            }
            set
            {
                if (SelectedMeal != null)
                {
                    SelectedMeal.MealRecipe = value;
                }
            }
        }

        public string MealType
        {
            get
            {
                return SelectedMeal?.MealType;
            }
            set
            {
                if (SelectedMeal != null)
                {
                    SelectedMeal.MealType = value;
                }
            }
        }

        public void SaveMeal()
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
            if (!String.IsNullOrEmpty(NewMealName))
            {
                var NewMeal = new MealViewModel
                {
                    Id = IdTemp,
                    MealName = NewMealName,
                    MealRecipe = NewMealRecipe,
                    MealType = NewMealType
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
                NewMealName = null;
                NewMealRecipe = null;
                NewMealType = null;
            }
        }

    }
}
