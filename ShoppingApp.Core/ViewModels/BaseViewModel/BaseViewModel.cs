using ShoppingApp.Core.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public ObservableCollection<MealViewModel> MealsList { get; set; } = new ObservableCollection<MealViewModel>();
        public ObservableCollection<IngredientViewModel> IngedientsListVM { get; set; } = new ObservableCollection<IngredientViewModel>();
        public ObservableCollection<IngredientForMealViewModel> IngredientsForMealVM { get; set; } = new ObservableCollection<IngredientForMealViewModel>();
        public ObservableCollection<MealViewModel> MealsForShoppingList => DataStore.Instance.MealsForShoppingList;
        public ObservableCollection<IngredientsToListViewModel> IngredientsToBuy => DataStore.Instance.IngredientsToBuy;
        public virtual MealViewModel SelectedMeal { get; set; }
        //public static string xlsFileName = string.Empty;
        protected static string xlsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
        //public string xlsFile = Path.Combine(xlsFilePath, xlsFileName);
        public ICommand SaveMealCommand { get; set; }
        public ICommand DeleteMealCommand { get; set; }
        public ICommand AddMealToListCommand { get; set; }
        public ICommand GenereteShoppingListCommand { get; set; }
        public ICommand DeleteShoppingListCommand { get; set; }
        public ICommand DeleteShoppingListWithIngredientsCommand { get; set; }
        public ICommand CreateXls { get; set; }
        public ICommand AddItems { get; set; }
        public ICommand SaveItems { get; set; }
        public ICommand DeleteItems { get; set; }

        protected void ReloadVMTables()
        {
            MealsList.Clear();
            IngedientsListVM.Clear();
            IngredientsForMealVM.Clear();
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
                IngedientsListVM.Add(new IngredientViewModel
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name
                });

            }
            foreach (var meal in MealsList)
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
        protected void DeleteShoppingListWithIngredients()
        {
            IngredientsToBuy.Clear();
            MealsForShoppingList.Clear();
            ReloadVMTables();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
