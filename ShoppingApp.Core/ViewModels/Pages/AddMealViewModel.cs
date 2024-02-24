using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingApp.Core.Helpers;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class AddMealViewModel : BaseViewModel
    {
                
        private List<IngredientForMealViewModel> IngredientsForMealToReference { get; set; } = new List<IngredientForMealViewModel>();
        private MealViewModel? selectedMeal { get; set; }

        public override MealViewModel SelectedMeal
        {
            get { return selectedMeal; }
            set
            {
                selectedMeal = value;
                if (selectedMeal != null)
                {
                    // Clear the list before populating it to avoid duplicates
                    IngredientsForMealToReference.Clear();

                    // All ingredients for meals to edit/save
                    foreach (var ingredientForMeal in IngredientsForMealVM.Where(i => i.MealId == selectedMeal.Id).ToList())
                    {
                        IngredientsForMealToReference.Add(ingredientForMeal);
                    }

                    // Assigning each ingredient
                    var ingredient1 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                    if (ingredient1 != null)
                    {
                        selectedIngredient1ForMeal = null;
                        selectedIngredient1ForMeal = IngedientsListVM.First(i => i.Id == ingredient1.IngredientId);
                    }
                    else
                    {
                        selectedIngredient1ForMeal = null;
                    }
                    var ingredient2 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 2 && i.MealId == selectedMeal.Id);
                    if (ingredient2 != null)
                    {
                        selectedIngredient2ForMeal = null;
                        selectedIngredient2ForMeal = IngedientsListVM.First(i => i.Id == ingredient2.IngredientId);
                    }
                    else
                    {
                        selectedIngredient2ForMeal = null;
                    }
                    var ingredient3 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 3 && i.MealId == selectedMeal.Id);
                    if (ingredient3 != null)
                    {
                        selectedIngredient3ForMeal = null;
                        selectedIngredient3ForMeal = IngedientsListVM.First(i => i.Id == ingredient3.IngredientId);
                    }
                    else
                    {
                        selectedIngredient3ForMeal = null;
                    }
                    var ingredient4 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 4 && i.MealId == selectedMeal.Id);
                    if (ingredient4 != null)
                    {
                        selectedIngredient4ForMeal = null;
                        selectedIngredient4ForMeal = IngedientsListVM.First(i => i.Id == ingredient4.IngredientId);
                    }
                    else
                    {
                        selectedIngredient4ForMeal = null;
                    }
                    var ingredient5 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 5 && i.MealId == selectedMeal.Id);
                    if (ingredient5 != null)
                    {
                        selectedIngredient5ForMeal = null;
                        selectedIngredient5ForMeal = IngedientsListVM.First(i => i.Id == ingredient5.IngredientId);
                    }
                    else
                    {
                        selectedIngredient5ForMeal = null;
                    }
                    var ingredient6 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 6 && i.MealId == selectedMeal.Id);
                    if (ingredient6 != null)
                    {
                        selectedIngredient6ForMeal = null;
                        selectedIngredient6ForMeal = IngedientsListVM.First(i => i.Id == ingredient6.IngredientId);
                    }
                    else
                    {
                        selectedIngredient6ForMeal = null;
                    }
                    var ingredient7 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 7 && i.MealId == selectedMeal.Id);
                    if (ingredient7 != null)
                    {
                        selectedIngredient7ForMeal = null;
                        selectedIngredient7ForMeal = IngedientsListVM.First(i => i.Id == ingredient7.IngredientId);
                    }
                    else
                    {
                        selectedIngredient7ForMeal = null;
                    }
                    var ingredient8 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 8 && i.MealId == selectedMeal.Id);
                    if (ingredient8 != null)
                    {
                        selectedIngredient8ForMeal = null;
                        selectedIngredient8ForMeal = IngedientsListVM.First(i => i.Id == ingredient8.IngredientId);
                    }
                    else
                    {
                        selectedIngredient8ForMeal = null;
                    }
                    var ingredient9 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 9 && i.MealId == selectedMeal.Id);
                    if (ingredient9 != null)
                    {
                        selectedIngredient9ForMeal = null;
                        selectedIngredient9ForMeal = IngedientsListVM.First(i => i.Id == ingredient9.IngredientId);
                    }
                    else
                    {
                        selectedIngredient9ForMeal = null;
                    }
                    var ingredient10 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 10 && i.MealId == selectedMeal.Id);
                    if (ingredient10 != null)
                    {
                        selectedIngredient10ForMeal = null;
                        selectedIngredient10ForMeal = IngedientsListVM.First(i => i.Id == ingredient10.IngredientId);
                    }
                    else
                    {
                        selectedIngredient10ForMeal = null;
                    }
                    var ingredient11 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 11 && i.MealId == selectedMeal.Id);
                    if (ingredient11 != null)
                    {
                        selectedIngredient11ForMeal = null;
                        selectedIngredient11ForMeal = IngedientsListVM.First(i => i.Id == ingredient11.IngredientId);
                    }
                    else
                    {
                        selectedIngredient11ForMeal = null;
                    }
                    var ingredient12 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 12 && i.MealId == selectedMeal.Id);
                    if (ingredient12 != null)
                    {
                        selectedIngredient12ForMeal = null;
                        selectedIngredient12ForMeal = IngedientsListVM.First(i => i.Id == ingredient12.IngredientId);
                    }
                    else
                    {
                        selectedIngredient12ForMeal = null;
                    }
                    var ingredient13 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 13 && i.MealId == selectedMeal.Id);
                    if (ingredient13 != null)
                    {
                        selectedIngredient13ForMeal = null;
                        selectedIngredient13ForMeal = IngedientsListVM.First(i => i.Id == ingredient13.IngredientId);
                    }
                    else
                    {
                        selectedIngredient13ForMeal = null;
                    }
                    var ingredient14 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 14 && i.MealId == selectedMeal.Id);
                    if (ingredient14 != null)
                    {
                        selectedIngredient14ForMeal = null;
                        selectedIngredient14ForMeal = IngedientsListVM.First(i => i.Id == ingredient14.IngredientId);
                    }
                    else
                    {
                        selectedIngredient14ForMeal = null;
                    }
                    var ingredient15 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 15 && i.MealId == selectedMeal.Id);
                    if (ingredient15 != null)
                    {
                        selectedIngredient15ForMeal = null;
                        selectedIngredient15ForMeal = IngedientsListVM.First(i => i.Id == ingredient15.IngredientId);
                    }
                    else
                    {
                        selectedIngredient15ForMeal = null;
                    }
                }
            }
        }

        private IngreditenViewModel? selectedIngredient1ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient1ForMeal
        {
            get { return selectedIngredient1ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient1ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient2ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient2ForMeal
        {
            get { return selectedIngredient2ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient2ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient3ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient3ForMeal
        {
            get { return selectedIngredient3ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient3ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient4ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient4ForMeal
        {
            get { return selectedIngredient4ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient4ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient5ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient5ForMeal
        {
            get { return selectedIngredient5ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient5ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient6ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient6ForMeal
        {
            get { return selectedIngredient6ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient6ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient7ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient7ForMeal
        {
            get { return selectedIngredient7ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient7ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient8ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient8ForMeal
        {
            get { return selectedIngredient8ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient8ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient9ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient9ForMeal
        {
            get { return selectedIngredient9ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient9ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient10ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient10ForMeal
        {
            get { return selectedIngredient10ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient10ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient11ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient11ForMeal
        {
            get { return selectedIngredient11ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient11ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient12ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient12ForMeal
        {
            get { return selectedIngredient12ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient12ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient13ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient13ForMeal
        {
            get { return selectedIngredient13ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient13ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient14ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient14ForMeal
        {
            get { return selectedIngredient14ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient14ForMeal = value;
                }

            }
        }
        private IngreditenViewModel? selectedIngredient15ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient15ForMeal
        {
            get { return selectedIngredient15ForMeal; }
            set
            {
                if (selectedMeal != null)
                {
                    selectedIngredient15ForMeal = value;
                }

            }
        }


        public string[] MealTypesList { get; set; } = new string[] {"Breakfast", "Lunch", "Dinner", "Tea", "Supper"};
        public string AddMealName { get; set; }
        private string AddMealType { get; set; }
        private string AddMealRecipe { get; set; }
        public AddMealViewModel()
        {
            DatabaseCreationTool.MyDatabase.Meals.ToList();
            SaveMealCommand = new RelayCommand(SaveMeal);
            DeleteMealCommand = new RelayCommand(DeleteMeal);
            Reload();
        }

        public void Reload()
        {
            SelectedMeal = null;
            AddMealName = null;
            AddMealRecipe = null;
            AddMealType = null;
            selectedIngredient1ForMeal = null;
            AddStringIngredient1ForMeal = null;
            AddQuantityIngredient1ForMeal = null;
            AddUnitIngredient1ForMeal = null;
            selectedIngredient2ForMeal = null;
            AddStringIngredient2ForMeal = null;
            AddQuantityIngredient2ForMeal = null;
            AddUnitIngredient2ForMeal = null;
            selectedIngredient3ForMeal = null;
            AddStringIngredient3ForMeal = null;
            AddQuantityIngredient3ForMeal = null;
            AddUnitIngredient3ForMeal = null;
            selectedIngredient4ForMeal = null;
            AddStringIngredient4ForMeal = null;
            AddQuantityIngredient4ForMeal = null;
            AddUnitIngredient4ForMeal = null;
            selectedIngredient5ForMeal = null;
            AddStringIngredient5ForMeal = null;
            AddQuantityIngredient5ForMeal = null;
            AddUnitIngredient5ForMeal = null;
            selectedIngredient6ForMeal = null;
            AddStringIngredient6ForMeal = null;
            AddQuantityIngredient6ForMeal = null;
            AddUnitIngredient6ForMeal = null;
            selectedIngredient7ForMeal = null;
            AddStringIngredient7ForMeal = null;
            AddQuantityIngredient7ForMeal = null;
            AddUnitIngredient7ForMeal = null;
            selectedIngredient8ForMeal = null;
            AddStringIngredient8ForMeal = null;
            AddQuantityIngredient8ForMeal = null;
            AddUnitIngredient8ForMeal = null;
            selectedIngredient9ForMeal = null;
            AddStringIngredient9ForMeal = null;
            AddQuantityIngredient9ForMeal = null;
            AddUnitIngredient9ForMeal = null;
            selectedIngredient10ForMeal = null;
            AddStringIngredient10ForMeal = null;
            AddQuantityIngredient10ForMeal = null;
            AddUnitIngredient10ForMeal = null;
            selectedIngredient11ForMeal = null;
            AddStringIngredient11ForMeal = null;
            AddQuantityIngredient11ForMeal = null;
            AddUnitIngredient11ForMeal = null;
            selectedIngredient12ForMeal = null;
            AddStringIngredient12ForMeal = null;
            AddQuantityIngredient12ForMeal = null;
            AddUnitIngredient12ForMeal = null;
            selectedIngredient13ForMeal = null;
            AddStringIngredient13ForMeal = null;
            AddQuantityIngredient13ForMeal = null;
            AddUnitIngredient13ForMeal = null;
            selectedIngredient14ForMeal = null;
            AddStringIngredient14ForMeal = null;
            AddQuantityIngredient14ForMeal = null;
            AddUnitIngredient14ForMeal = null;
            selectedIngredient15ForMeal = null;
            AddStringIngredient15ForMeal = null;
            AddQuantityIngredient15ForMeal = null;
            AddUnitIngredient15ForMeal = null;
            ReloadVMTables();
            selectedMeal = null;                    
        }

        public string MealRecipe
        {
            get
            {
                if(selectedMeal != null)
                {
                    return selectedMeal.MealRecipe;
                }
                else
                {
                    return AddMealRecipe;
                }
                          
            }
            set
            {
                if (selectedMeal != null)
                {
                    selectedMeal.MealRecipe = value;
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
                if (selectedMeal != null)
                {
                    return selectedMeal.MealType;
                }
                else
                {
                    return AddMealType;
                }
            }
            set 
            {
                if (selectedMeal != null)
                {
                    selectedMeal.MealType = value;
                }
                else
                {
                    AddMealType = value;
                }
            }
        }
        
        //method to save completly new meals and change existing ones
        public void SaveMeal()
        {
            //mealName must exist
            if ((!String.IsNullOrEmpty(AddMealName)))
            {
                //update existing recipe
                if (AddMealName.Equals(selectedMeal?.MealName.ToString()))
                {
                    //updating recipe and type
                    var mealToUpdate = DatabaseCreationTool.MyDatabase.Meals.Find(selectedMeal.Id);                
                    mealToUpdate.MealRecipe = selectedMeal.MealRecipe;
                    mealToUpdate.MealType = selectedMeal.MealType;

                    //update ingredients used to prepare meal
                    for (int ingr = 1; ingr <= 15; ingr++)
                    {
                        SaveIngredientForMealLogic(ingr);                       
                    }
                                                         
                    //save changes to the database
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    
                    //clear everything to force user to select new meal to create/edit after saving
                    Reload();

                }
                //save new recipe
                else
                {
                    //find and assing Id for the new Meal
                    var lastMealId = MealsList.OrderByDescending(m => m.Id).FirstOrDefault();
                    int countedMealsToInsert = lastMealId != null ? lastMealId.Id : 0;
                    countedMealsToInsert += 1;

                    var NewMeal = new MealViewModel
                    {
                        Id = countedMealsToInsert,
                        MealName = AddMealName,
                        MealRecipe = AddMealRecipe,
                        MealType = AddMealType
                    };

                    MealsList.Add(NewMeal);
                    DatabaseCreationTool.MyDatabase.Meals.Add(new Meal
                    {
                        Id = NewMeal.Id,
                        MealName = NewMeal.MealName,
                        MealRecipe = NewMeal.MealRecipe,
                        MealType = NewMeal.MealType
                    });

                    //save changes to the database
                    DatabaseCreationTool.MyDatabase.SaveChanges();

                    //clear everything to force user to select new meal to create/edit after saving                   
                    Reload();
                }
            }                    
        }

        //deleting existing meals
        public void DeleteMeal()
        {
            if (selectedMeal != null)
            {
                var mealToDelete = DatabaseCreationTool.MyDatabase.Meals.Find(selectedMeal.Id);
                var ingredientsToDelete = DatabaseCreationTool.MyDatabase.IngredientForMeal.Where(i => i.MealId == selectedMeal.Id).ToList();
                foreach (var ingredient in ingredientsToDelete)
                { 
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Remove(ingredient);
                }
                DatabaseCreationTool.MyDatabase.Meals.Remove(mealToDelete);
                DatabaseCreationTool.MyDatabase.SaveChanges();
                //clear everything to force user to select new meal after deleting
                Reload();
            }
            else
            {
                //temporary - messagebox eventually
                Reload();
            }                
        }
        
        public void SaveIngredientForMealLogic(int IngredientForMealNumber)
        {
            //declare varuables at the beggining 
            string? saveAddString = null;
            double? saveAddQuantity = 0;
            string saveAddUnit = "";
            string? existingString = null;
            double? existingQuantity = null;
            string? existingUnit = null;
            switch (IngredientForMealNumber)
            {
                case 1:
                    saveAddString = AddStringIngredient1ForMeal;
                    saveAddQuantity = AddQuantityIngredient1ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient1ForMeal ?? "";
                    existingString = StringIngredient1ForMeal;
                    existingQuantity = QuantityIngredient1ForMeal;
                    existingUnit = UnitIngredient1ForMeal;
                    break;
                case 2:
                    saveAddString = AddStringIngredient2ForMeal;
                    saveAddQuantity = AddQuantityIngredient2ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient2ForMeal ?? "";
                    existingString = StringIngredient2ForMeal;
                    existingQuantity = QuantityIngredient2ForMeal;
                    existingUnit = UnitIngredient2ForMeal;
                    break;
                case 3:
                    saveAddString = AddStringIngredient3ForMeal;
                    saveAddQuantity = AddQuantityIngredient3ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient3ForMeal ?? "";
                    existingString = StringIngredient3ForMeal;
                    existingQuantity = QuantityIngredient3ForMeal;
                    existingUnit = UnitIngredient3ForMeal;
                    break;
                case 4:
                    saveAddString = AddStringIngredient4ForMeal;
                    saveAddQuantity = AddQuantityIngredient4ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient4ForMeal ?? "";
                    existingString = StringIngredient4ForMeal;
                    existingQuantity = QuantityIngredient4ForMeal;
                    existingUnit = UnitIngredient4ForMeal;
                    break;
                case 5:
                    saveAddString = AddStringIngredient5ForMeal;
                    saveAddQuantity = AddQuantityIngredient5ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient5ForMeal ?? "";
                    existingString = StringIngredient5ForMeal;
                    existingQuantity = QuantityIngredient5ForMeal;
                    existingUnit = UnitIngredient5ForMeal;
                    break;
                case 6:
                    saveAddString = AddStringIngredient6ForMeal;
                    saveAddQuantity = AddQuantityIngredient6ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient6ForMeal ?? "";
                    existingString = StringIngredient6ForMeal;
                    existingQuantity = QuantityIngredient6ForMeal;
                    existingUnit = UnitIngredient6ForMeal;
                    break;
                case 7:
                    saveAddString = AddStringIngredient7ForMeal;
                    saveAddQuantity = AddQuantityIngredient7ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient7ForMeal ?? "";
                    existingString = StringIngredient7ForMeal;
                    existingQuantity = QuantityIngredient7ForMeal;
                    existingUnit = UnitIngredient7ForMeal;
                    break;
                case 8:
                    saveAddString = AddStringIngredient8ForMeal;
                    saveAddQuantity = AddQuantityIngredient8ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient8ForMeal ?? "";
                    existingString = StringIngredient8ForMeal;
                    existingQuantity = QuantityIngredient8ForMeal;
                    existingUnit = UnitIngredient8ForMeal;
                    break;
                case 9:
                    saveAddString = AddStringIngredient9ForMeal;
                    saveAddQuantity = AddQuantityIngredient9ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient9ForMeal ?? "";
                    existingString = StringIngredient9ForMeal;
                    existingQuantity = QuantityIngredient9ForMeal;
                    existingUnit = UnitIngredient9ForMeal;
                    break;
                case 10:
                    saveAddString = AddStringIngredient10ForMeal;
                    saveAddQuantity = AddQuantityIngredient10ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient10ForMeal ?? "";
                    existingString = StringIngredient10ForMeal;
                    existingQuantity = QuantityIngredient10ForMeal;
                    existingUnit = UnitIngredient10ForMeal;
                    break;
                case 11:
                    saveAddString = AddStringIngredient11ForMeal;
                    saveAddQuantity = AddQuantityIngredient11ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient11ForMeal ?? "";
                    existingString = StringIngredient11ForMeal;
                    existingQuantity = QuantityIngredient11ForMeal;
                    existingUnit = UnitIngredient11ForMeal;
                    break;
                case 12:
                    saveAddString = AddStringIngredient12ForMeal;
                    saveAddQuantity = AddQuantityIngredient12ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient12ForMeal ?? "";
                    existingString = StringIngredient12ForMeal;
                    existingQuantity = QuantityIngredient12ForMeal;
                    existingUnit = UnitIngredient12ForMeal;
                    break;
                case 13:
                    saveAddString = AddStringIngredient13ForMeal;
                    saveAddQuantity = AddQuantityIngredient13ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient13ForMeal ?? "";
                    existingString = StringIngredient13ForMeal;
                    existingQuantity = QuantityIngredient13ForMeal;
                    existingUnit = UnitIngredient13ForMeal;
                    break;
                case 14:
                    saveAddString = AddStringIngredient14ForMeal;
                    saveAddQuantity = AddQuantityIngredient14ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient14ForMeal ?? "";
                    existingString = StringIngredient14ForMeal;
                    existingQuantity = QuantityIngredient14ForMeal;
                    existingUnit = UnitIngredient14ForMeal;
                    break;
                case 15:
                    saveAddString = AddStringIngredient15ForMeal;
                    saveAddQuantity = AddQuantityIngredient15ForMeal ?? 0;
                    saveAddUnit = AddUnitIngredient15ForMeal ?? "";
                    existingString = StringIngredient15ForMeal;
                    existingQuantity = QuantityIngredient15ForMeal;
                    existingUnit = UnitIngredient15ForMeal;
                    break;
            }

            //check for existing ingredient for meal 
            var ingredientForMealToUpdate = IngredientsForMealVM.FirstOrDefault(idb => idb.MealId == selectedMeal.Id && idb.tempId == IngredientForMealNumber);
            bool anyChangesName = true;
            bool anyChangesQuantity = true;
            bool anyChangesUnit = true;

            //
            if (String.IsNullOrEmpty(saveAddString))
            {
                if (!String.IsNullOrEmpty(existingString))
                {
                    if (ingredientForMealToUpdate is not null)
                    {
                        if (ingredientForMealToUpdate.IngredientName.Equals(existingString))
                        {
                            //this mean that meal never had this ingredient and we do not pass parameters to save something so we exit from procedure
                            anyChangesName = false;
                        }
                        
                    }
                    if (saveAddQuantity == 0 && existingQuantity != null)
                    {
                        if (ingredientForMealToUpdate.Quantity == existingQuantity)
                        {
                            anyChangesQuantity = false;
                        }
                    }
                    if (String.IsNullOrEmpty(saveAddUnit) && !String.IsNullOrEmpty(existingUnit))
                    {
                        if (ingredientForMealToUpdate.Unit == existingUnit)
                        {
                            anyChangesUnit = false;
                        }
                    }
                }
                else
                {
                    //check if there is meal to Delete
                    if (ingredientForMealToUpdate is not null)
                    {
                        //delete
                        var ingredientForMealToDelete = DatabaseCreationTool.MyDatabase.IngredientForMeal.FirstOrDefault(i => i.Id == ingredientForMealToUpdate.Id);
                        DatabaseCreationTool.MyDatabase.IngredientForMeal.Remove(ingredientForMealToDelete);
                        return;
                    }
                    else
                        //no ingredientForMealToUpdate/Add/Delete
                        return;
                }


                if (!anyChangesName && !anyChangesQuantity && !anyChangesUnit) 
                {
                    return;
                }
                else
                {
                    saveAddString = existingString;
                    saveAddQuantity = existingQuantity;
                    saveAddUnit = existingUnit;
                }
                    
            }                
                  
            
            if (ingredientForMealToUpdate != null)
            {
                //check if the ingredients for meal existsted before
                var ingredientToReferenceToUpdate = IngredientsForMealToReference.FirstOrDefault(il => il.Id == ingredientForMealToUpdate.Id && il.MealId == selectedMeal.Id);

                //check if new ingredient exists in the database
                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == saveAddString);

                //ingredient exists
                if (ingredientCheck != null)
                {
                    ingredientToReferenceToUpdate.Quantity = (double)saveAddQuantity;                  
                    ingredientToReferenceToUpdate.Unit = saveAddUnit;
                }
                //new ingredient
                else
                {
                    var lastIngredient = IngedientsListVM.OrderByDescending(m => m.Id).FirstOrDefault();
                    int countedIngredientsInsertId = lastIngredient != null ? lastIngredient.Id : 0;

                    countedIngredientsInsertId += 1;
                    //adding NEW ingredient to the Ingredients local + database
                    var newIngredient = new IngreditenViewModel
                    {
                        Id = countedIngredientsInsertId,
                        Name = saveAddString
                    };

                    IngedientsListVM.Add(newIngredient);

                    //adding new ingredient to the database 
                    DatabaseCreationTool.MyDatabase.Ingredients.Add(new Ingredient
                    {
                        Id = newIngredient.Id,
                        Name = newIngredient.Name
                    });

                    ingredientToReferenceToUpdate.IngredientId = newIngredient.Id;
                    ingredientToReferenceToUpdate.Quantity = (double)saveAddQuantity;
                    ingredientToReferenceToUpdate.Unit = saveAddUnit;
                }

                var ingredientToUpdateDB = DatabaseCreationTool.MyDatabase.IngredientForMeal.FirstOrDefault(i => i.Id == ingredientToReferenceToUpdate.Id);
                if (ingredientToUpdateDB != null)
                {
                    ingredientToUpdateDB.MealId = ingredientToReferenceToUpdate.MealId;
                    ingredientToUpdateDB.IngredientId = ingredientToReferenceToUpdate.IngredientId;
                    ingredientToUpdateDB.Quantity = ingredientToReferenceToUpdate.Quantity;
                    ingredientToUpdateDB.Unit = ingredientToReferenceToUpdate.Unit;
                }
            }
            //if ingredient FOR MEAL is new
            else
            {
                var lastIngredientForMeal = IngredientsForMealVM.OrderByDescending(m => m.Id).FirstOrDefault();
                int countedIngredientsForMealInsertId = lastIngredientForMeal != null ? lastIngredientForMeal.Id : 0;
                countedIngredientsForMealInsertId += 1;

                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == saveAddString);
                //if ingredient already exists
                if (ingredientCheck != null)
                {
                    var newIngredientFromMeal = new IngredientForMealViewModel
                    {
                        tempId = IngredientForMealNumber,
                        Id = countedIngredientsForMealInsertId,
                        MealId = selectedMeal.Id,
                        IngredientId = ingredientCheck.Id,
                        IngredientName = saveAddString,
                        Quantity = (double)saveAddQuantity,
                        Unit = saveAddUnit
                    };
                    IngredientsForMealVM.Add(newIngredientFromMeal);
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Add(new IngredientForMeal
                    {
                        Id = newIngredientFromMeal.Id,
                        MealId = newIngredientFromMeal.MealId,
                        IngredientId = newIngredientFromMeal.IngredientId,
                        Quantity = newIngredientFromMeal.Quantity,
                        Unit = newIngredientFromMeal.Unit
                    });
                }
                //new ingredient
                else
                {
                    var lastIngredient = IngedientsListVM.OrderByDescending(m => m.Id).FirstOrDefault();
                    int countedIngredientsInsertId = lastIngredient != null ? lastIngredient.Id : 0;

                    countedIngredientsInsertId += 1;

                    var newIngredient = new IngreditenViewModel
                    {
                        Id = countedIngredientsInsertId,
                        Name = saveAddString  
                    };
                    IngedientsListVM.Add(newIngredient);
                    DatabaseCreationTool.MyDatabase.Ingredients.Add(new Ingredient
                    {
                        Id= newIngredient.Id,
                        Name = newIngredient.Name
                    });

                    var newIngredientForMeal = new IngredientForMealViewModel
                    {
                        tempId = IngredientForMealNumber,
                        Id = countedIngredientsForMealInsertId,
                        MealId = selectedMeal.Id,
                        IngredientId = countedIngredientsInsertId,
                        IngredientName = saveAddString,
                        Quantity = (double)saveAddQuantity,
                        Unit = saveAddUnit
                    };
                    IngredientsForMealVM.Add(newIngredientForMeal);
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Add(new IngredientForMeal
                    {
                        Id = newIngredientForMeal.Id,
                        MealId = newIngredientForMeal.MealId,
                        IngredientId = newIngredientForMeal.IngredientId,
                        Quantity = newIngredientForMeal.Quantity,
                        Unit = newIngredientForMeal.Unit
                    });
                   
                }
               
            }
        }
        private string AddStringIngredient1ForMeal { get; set; }
        public string StringIngredient1ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(1, selectedIngredient1ForMeal, AddStringIngredient1ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(1, selectedIngredient1ForMeal, AddStringIngredient1ForMeal, value);
                AddStringIngredient1ForMeal = addString;
            }
        }        
        private double? AddQuantityIngredient1ForMeal { get; set; }
        public double? QuantityIngredient1ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(1, selectedIngredient1ForMeal, AddQuantityIngredient1ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(1, selectedIngredient1ForMeal, AddQuantityIngredient1ForMeal, value);
                AddQuantityIngredient1ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient1ForMeal { get; set; }
        public string? UnitIngredient1ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(1, selectedIngredient1ForMeal, AddUnitIngredient1ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(1, selectedIngredient1ForMeal, AddUnitIngredient1ForMeal, value);
                AddUnitIngredient1ForMeal = addUnit;
            }
        }
        private string AddStringIngredient2ForMeal { get; set; }
        public string StringIngredient2ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(2, selectedIngredient2ForMeal, AddStringIngredient2ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(2, selectedIngredient2ForMeal, AddStringIngredient2ForMeal, value);
                AddStringIngredient2ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient2ForMeal { get; set; }
        public double? QuantityIngredient2ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(2, selectedIngredient2ForMeal, AddQuantityIngredient2ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(2, selectedIngredient2ForMeal, AddQuantityIngredient2ForMeal, value);
                AddQuantityIngredient2ForMeal = addDouble;
            }
        }            
        private string? AddUnitIngredient2ForMeal { get; set; }
        public string? UnitIngredient2ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(2, selectedIngredient2ForMeal, AddUnitIngredient2ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(2, selectedIngredient2ForMeal, AddUnitIngredient2ForMeal, value);
                AddUnitIngredient2ForMeal = addUnit;
            }
        }
        private string AddStringIngredient3ForMeal { get; set; }
        public string StringIngredient3ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(3, selectedIngredient3ForMeal, AddStringIngredient3ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(3, selectedIngredient3ForMeal, AddStringIngredient3ForMeal, value);
                AddStringIngredient3ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient3ForMeal { get; set; }
        public double? QuantityIngredient3ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(3, selectedIngredient3ForMeal, AddQuantityIngredient3ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(3, selectedIngredient3ForMeal, AddQuantityIngredient3ForMeal, value);
                AddQuantityIngredient3ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient3ForMeal { get; set; }
        public string? UnitIngredient3ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(3, selectedIngredient3ForMeal, AddUnitIngredient3ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(3, selectedIngredient3ForMeal, AddUnitIngredient3ForMeal, value);
                AddUnitIngredient3ForMeal = addUnit;
            }
        }
        private string AddStringIngredient4ForMeal { get; set; }
        public string StringIngredient4ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(4, selectedIngredient4ForMeal, AddStringIngredient4ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(4, selectedIngredient4ForMeal, AddStringIngredient4ForMeal, value);
                AddStringIngredient4ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient4ForMeal { get; set; }
        public double? QuantityIngredient4ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(4, selectedIngredient4ForMeal, AddQuantityIngredient4ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(4, selectedIngredient4ForMeal, AddQuantityIngredient4ForMeal, value);
                AddQuantityIngredient4ForMeal = addDouble; 
            }
        }
        private string? AddUnitIngredient4ForMeal { get; set; }
        public string? UnitIngredient4ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(4, selectedIngredient4ForMeal, AddUnitIngredient4ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(4, selectedIngredient4ForMeal, AddUnitIngredient4ForMeal, value);
                AddUnitIngredient4ForMeal = addUnit;
            }
        }
        private string AddStringIngredient5ForMeal { get; set; }
        public string StringIngredient5ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(5, selectedIngredient5ForMeal, AddStringIngredient5ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(5, selectedIngredient5ForMeal, AddStringIngredient5ForMeal, value);
                AddStringIngredient5ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient5ForMeal { get; set; }
        public double? QuantityIngredient5ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(5, selectedIngredient5ForMeal, AddQuantityIngredient5ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(5, selectedIngredient5ForMeal, AddQuantityIngredient5ForMeal, value);
                AddQuantityIngredient5ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient5ForMeal { get; set; }
        public string? UnitIngredient5ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(5, selectedIngredient5ForMeal, AddUnitIngredient5ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(5, selectedIngredient5ForMeal, AddUnitIngredient5ForMeal, value);
                AddUnitIngredient5ForMeal = addUnit;
            }
        }
        private string AddStringIngredient6ForMeal { get; set; }
        public string StringIngredient6ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(6, selectedIngredient6ForMeal, AddStringIngredient6ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(6, selectedIngredient6ForMeal, AddStringIngredient6ForMeal, value);
                AddStringIngredient6ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient6ForMeal { get; set; }
        public double? QuantityIngredient6ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(6, selectedIngredient6ForMeal, AddQuantityIngredient6ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(6, selectedIngredient6ForMeal, AddQuantityIngredient6ForMeal, value);
                AddQuantityIngredient6ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient6ForMeal { get; set; }
        public string? UnitIngredient6ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(6, selectedIngredient6ForMeal, AddUnitIngredient6ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(6, selectedIngredient6ForMeal, AddUnitIngredient6ForMeal, value);
                AddUnitIngredient6ForMeal = addUnit;
            }
        }
        private string AddStringIngredient7ForMeal { get; set; }
        public string StringIngredient7ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(7, selectedIngredient7ForMeal, AddStringIngredient7ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(7, selectedIngredient7ForMeal, AddStringIngredient7ForMeal, value);
                AddStringIngredient7ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient7ForMeal { get; set; }
        public double? QuantityIngredient7ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(7, selectedIngredient7ForMeal, AddQuantityIngredient7ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(7, selectedIngredient7ForMeal, AddQuantityIngredient7ForMeal, value);
                AddQuantityIngredient7ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient7ForMeal { get; set; }
        public string? UnitIngredient7ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(7, selectedIngredient7ForMeal, AddUnitIngredient7ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(7, selectedIngredient7ForMeal, AddUnitIngredient7ForMeal, value);
                AddUnitIngredient7ForMeal = addUnit;
            }
        }
        private string AddStringIngredient8ForMeal { get; set; }
        public string StringIngredient8ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(8, selectedIngredient8ForMeal, AddStringIngredient8ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(8, selectedIngredient8ForMeal, AddStringIngredient8ForMeal, value);
                AddStringIngredient8ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient8ForMeal { get; set; }
        public double? QuantityIngredient8ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(8, selectedIngredient8ForMeal, AddQuantityIngredient8ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(8, selectedIngredient8ForMeal, AddQuantityIngredient8ForMeal, value);
                AddQuantityIngredient8ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient8ForMeal { get; set; }
        public string? UnitIngredient8ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(8, selectedIngredient8ForMeal, AddUnitIngredient8ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(8, selectedIngredient8ForMeal, AddUnitIngredient8ForMeal, value);
                AddUnitIngredient8ForMeal = addUnit;
            }
        }
        private string AddStringIngredient9ForMeal { get; set; }
        public string StringIngredient9ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(9, selectedIngredient9ForMeal, AddStringIngredient9ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(9, selectedIngredient9ForMeal, AddStringIngredient9ForMeal, value);
                AddStringIngredient9ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient9ForMeal { get; set; }
        public double? QuantityIngredient9ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(9, selectedIngredient9ForMeal, AddQuantityIngredient9ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(9, selectedIngredient9ForMeal, AddQuantityIngredient9ForMeal, value);
                AddQuantityIngredient9ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient9ForMeal { get; set; }
        public string? UnitIngredient9ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(9, selectedIngredient9ForMeal, AddUnitIngredient9ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(9, selectedIngredient9ForMeal, AddUnitIngredient9ForMeal, value);
                AddUnitIngredient9ForMeal = addUnit;
            }
        }
        private string AddStringIngredient10ForMeal { get; set; }
        public string StringIngredient10ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(10, selectedIngredient10ForMeal, AddStringIngredient10ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(10, selectedIngredient10ForMeal, AddStringIngredient10ForMeal, value);
                AddStringIngredient10ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient10ForMeal { get; set; }
        public double? QuantityIngredient10ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(10, selectedIngredient10ForMeal, AddQuantityIngredient10ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(10, selectedIngredient10ForMeal, AddQuantityIngredient10ForMeal, value);
                AddQuantityIngredient10ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient10ForMeal { get; set; }
        public string? UnitIngredient10ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(10, selectedIngredient10ForMeal, AddUnitIngredient10ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(10, selectedIngredient10ForMeal, AddUnitIngredient10ForMeal, value);
                AddUnitIngredient10ForMeal = addUnit;
            }
        }
        private string AddStringIngredient11ForMeal { get; set; }
        public string StringIngredient11ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(11, selectedIngredient11ForMeal, AddStringIngredient11ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(11, selectedIngredient11ForMeal, AddStringIngredient11ForMeal, value);
                AddStringIngredient11ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient11ForMeal { get; set; }
        public double? QuantityIngredient11ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(11, selectedIngredient11ForMeal, AddQuantityIngredient11ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(11, selectedIngredient11ForMeal, AddQuantityIngredient11ForMeal, value);
                AddQuantityIngredient11ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient11ForMeal { get; set; }
        public string? UnitIngredient11ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(11, selectedIngredient11ForMeal, AddUnitIngredient11ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(11, selectedIngredient11ForMeal, AddUnitIngredient11ForMeal, value);
                AddUnitIngredient11ForMeal = addUnit;
            }
        }
        private string AddStringIngredient12ForMeal { get; set; }
        public string StringIngredient12ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(12, selectedIngredient12ForMeal, AddStringIngredient12ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(12, selectedIngredient12ForMeal, AddStringIngredient12ForMeal, value);
                AddStringIngredient12ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient12ForMeal { get; set; }
        public double? QuantityIngredient12ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(12, selectedIngredient12ForMeal, AddQuantityIngredient12ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(12, selectedIngredient12ForMeal, AddQuantityIngredient12ForMeal, value);
                AddQuantityIngredient12ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient12ForMeal { get; set; }
        public string? UnitIngredient12ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(12, selectedIngredient12ForMeal, AddUnitIngredient12ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(12, selectedIngredient12ForMeal, AddUnitIngredient12ForMeal, value);
                AddUnitIngredient12ForMeal = addUnit;
            }
        }
        private string AddStringIngredient13ForMeal { get; set; }
        public string StringIngredient13ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(13, selectedIngredient13ForMeal, AddStringIngredient13ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(13, selectedIngredient13ForMeal, AddStringIngredient13ForMeal, value);
                AddStringIngredient13ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient13ForMeal { get; set; }
        public double? QuantityIngredient13ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(13, selectedIngredient13ForMeal, AddQuantityIngredient13ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(13, selectedIngredient13ForMeal, AddQuantityIngredient13ForMeal, value);
                AddQuantityIngredient13ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient13ForMeal { get; set; }
        public string? UnitIngredient13ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(13, selectedIngredient13ForMeal, AddUnitIngredient13ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(13, selectedIngredient13ForMeal, AddUnitIngredient13ForMeal, value);
                AddUnitIngredient13ForMeal = addUnit;
            }
        }
        private string AddStringIngredient14ForMeal { get; set; }
        public string StringIngredient14ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(14, selectedIngredient14ForMeal, AddStringIngredient14ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(14, selectedIngredient14ForMeal, AddStringIngredient14ForMeal, value);
                AddStringIngredient14ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient14ForMeal { get; set; }
        public double? QuantityIngredient14ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(14, selectedIngredient14ForMeal, AddQuantityIngredient14ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(14, selectedIngredient14ForMeal, AddQuantityIngredient14ForMeal, value);
                AddQuantityIngredient14ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient14ForMeal { get; set; }
        public string? UnitIngredient14ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(14, selectedIngredient14ForMeal, AddUnitIngredient14ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(14, selectedIngredient14ForMeal, AddUnitIngredient14ForMeal, value);
                AddUnitIngredient14ForMeal = addUnit;
            }
        }
        private string AddStringIngredient15ForMeal { get; set; }
        public string StringIngredient15ForMeal
        {
            get
            {
                return GetStringIngredientForMeal(15, selectedIngredient15ForMeal, AddStringIngredient15ForMeal);
            }
            set
            {
                string addString = SetStringIngredientForMeal(15, selectedIngredient15ForMeal, AddStringIngredient15ForMeal, value);
                AddStringIngredient15ForMeal = addString;
            }
        }
        private double? AddQuantityIngredient15ForMeal { get; set; }
        public double? QuantityIngredient15ForMeal
        {
            get
            {
                return GetQuantityIngredientForMeal(15, selectedIngredient15ForMeal, AddQuantityIngredient15ForMeal);
            }
            set
            {
                double? addDouble = SetQuantityIngredientForMeal(15, selectedIngredient15ForMeal, AddQuantityIngredient15ForMeal, value);
                AddQuantityIngredient15ForMeal = addDouble;
            }
        }
        private string? AddUnitIngredient15ForMeal { get; set; }
        public string? UnitIngredient15ForMeal
        {
            get
            {
                return GetUnitIngredientForMeal(15, selectedIngredient15ForMeal, AddUnitIngredient15ForMeal);
            }
            set
            {
                string addUnit = SetUnitIngredientForMeal(15, selectedIngredient15ForMeal, AddUnitIngredient15ForMeal, value);
                AddUnitIngredient15ForMeal = addUnit;
            }
        }
        //--------------------------------------------------//
        private string? GetStringIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, string addString)
        {
            if (selectedMeal != null && selectedIngredient != null)
            {
                var existingIngredientForMeal = IngredientsForMealToReference.FirstOrDefault(i => i.IngredientId == selectedIngredient.Id);
                if (existingIngredientForMeal != null)
                {
                    addString = selectedIngredient.Name;
                    return selectedIngredient.Name;
                }
                else
                {
                    return addString;
                }
            }
            else
            {
                return addString;
            }
        }
        private string? SetStringIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, string addString, string value)
        {
            if (selectedMeal != null)
            {
                if (selectedIngredient != null)
                {
                    var tempIngredientforMeal = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == tempId);
                    var existingIngredient = IngedientsListVM.FirstOrDefault(i => i.Name == value);
                    if (existingIngredient != null)
                    {
                        //if meal already have ingeredientXforMeal
                        if (tempIngredientforMeal != null)
                        {
                            tempIngredientforMeal.IngredientName = value;
                            tempIngredientforMeal.IngredientId = existingIngredient.Id;
                            addString = value;
                            return addString;
                        }
                        else
                        {
                            int countForId = IngredientsForMealVM.Count() + 1;

                            IngredientsForMealToReference.Add(new IngredientForMealViewModel
                            {
                                tempId = tempId,
                                Id = countForId,
                                MealId = SelectedMeal.Id,
                                IngredientId = existingIngredient.Id,
                                IngredientName = existingIngredient.Name,
                                Quantity = 0,
                                Unit = ""
                            });
                            addString = value;
                            return addString;
                        }
                    }
                    else
                        addString = value;
                    return addString;
                }
                else
                {
                    addString = value;
                    return addString;
                }
            }
            addString = value;
            return addString;
        }
        private double? GetQuantityIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, double? addQuantity)
        {
            //if there is selected meal
            if (selectedMeal != null)
            {
                //if there is selected ingredient 
                if (selectedIngredient != null)
                {
                    var quantityIngredientForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == tempId && i.MealId == selectedMeal.Id);
                    //if ingredient for selected meal has value                   
                    if (quantityIngredientForMealReference != null)
                    {
                        if (quantityIngredientForMealReference.IngredientId == selectedIngredient.Id)
                        {
                            addQuantity = quantityIngredientForMealReference.Quantity;
                            return quantityIngredientForMealReference.Quantity;
                        }

                        addQuantity = null;
                        return addQuantity;
                    }
                    else
                        addQuantity = null;
                    return addQuantity;
                }
                else
                    return addQuantity;
            }
            else
                return null;
        }
        private double? SetQuantityIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, double? addQuantity, double? value)
        {
            //if there is selected meal
            if (selectedMeal != null)
            {
                if (selectedIngredient != null)
                {
                    var quantityIngredientForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == tempId && i.MealId == selectedMeal.Id);
                    //if ingredient 1 for selected meal has value                   
                    if (quantityIngredientForMealReference != null)
                    {
                        if (quantityIngredientForMealReference.IngredientId == selectedIngredient.Id)
                        {
                            quantityIngredientForMealReference.Quantity = (double)value;
                            addQuantity = (double)value;
                            return addQuantity;
                        }
                        else
                            addQuantity = (double)value;
                        return addQuantity;
                    }
                }
                addQuantity = (double)value;
                return addQuantity;
            }
            return null;
        }
        private string GetUnitIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, string AddUnit)
        {
            //if there is selected meal
            if (selectedMeal != null)
            {
                //if there is selected ingredient 
                if (selectedIngredient != null)
                {
                    var unitIngredientForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == tempId && i.MealId == selectedMeal.Id);
                    //if ingredient 1 for selected meal has value                   
                    if (unitIngredientForMealReference != null)
                    {
                        if (unitIngredientForMealReference.IngredientId == selectedIngredient.Id)
                        {
                            AddUnit = unitIngredientForMealReference.Unit;
                            return AddUnit;
                        }

                        AddUnit = "";
                        return AddUnit;
                    }
                    else
                        AddUnit = "";
                    return AddUnit;
                }
                else
                    return AddUnit;
            }
            else
                return "";
        }

        private string SetUnitIngredientForMeal(int tempId, IngreditenViewModel selectedIngredient, string AddUnit, string value)
        {
            //if there is selected meal
            if (selectedMeal != null)
            {
                if (selectedIngredient != null)
                {
                    var unitIngredientForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == tempId && i.MealId == selectedMeal.Id);
                    //if ingredient 1 for selected meal has value                   
                    if (unitIngredientForMealReference != null)
                    {
                        if (unitIngredientForMealReference.IngredientId == selectedIngredient.Id)
                        {
                            unitIngredientForMealReference.Unit = value;
                            AddUnit = value;
                            return AddUnit;
                        }
                        else
                            AddUnit = value;
                        return AddUnit;
                    }
                }
                AddUnit = value;
                return AddUnit;
            }
            return null;
        }
    }
}
