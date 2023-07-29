using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        private List<IngredientForMealViewModel> IngredientsForMealToReference { get; set; } = new List<IngredientForMealViewModel>();

        private MealViewModel? selectedMeal { get; set; }

        public MealViewModel SelectedMeal
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
                        selectedIngredient1ForMeal = IngedientsListVM.First(i => i.Id == ingredient1.IngredientId);
                    }
                    else
                    {
                        selectedIngredient1ForMeal = null;
                    }
                }
            }
        }

        public IngreditenViewModel? selectedIngredient1ForMeal { get; set; }
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

        public string[] MealTypesList { get; set; } = new string[] {"Breakfast", "Lunch", "Dinner", "Tea", "Supper"};
        public string AddMealName { get; set; }
        private string AddMealType { get; set; }
        private string AddMealRecipe { get; set; }
        public ICommand SaveMealCommand { get; set; }
        public ICommand DeleteMealCommand { get; set; }
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
            SelectedIngredient1ForMeal = null;
            AddStringIngredient1ForMeal = null;
            AddQuantityIngredient1ForMeal = null;
            AddUnitIngredient1ForMeal = null;
            MealsList.Clear();
            IngedientsListVM.Clear();
            IngredientsForMealVM.Clear();
            selectedMeal = null;
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
                }
            }
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
                    SaveIngredientForMealLogic(1);
                    
                    
                    //save changes to the database
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    
                    //clear everything to force user to select new meal to create/edit after saving
                    Reload();

                }
                //save new recipe
                else
                {
                    //assing id in the meals
                    var countedMealsId = MealsList.ToList();
                    int mealIdInsert;
                    if (countedMealsId.Count > 0)
                    {
                        mealIdInsert = DatabaseCreationTool.MyDatabase.Meals.OrderByDescending(m => m.Id).First().Id + 1;
                    }
                    else
                    {
                        mealIdInsert = 1;
                    }
                    
                    var NewMeal = new MealViewModel
                    {
                        Id = mealIdInsert,
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
            string saveAddString = "";
            int? saveAddQuantity = 0;
            string saveAddUnit = "";
            switch (IngredientForMealNumber)
            {
                case 1:
                    saveAddString = AddStringIngredient1ForMeal;
                    saveAddQuantity = AddQuantityIngredient1ForMeal;
                    saveAddUnit= AddUnitIngredient1ForMeal;
                    break;
                case 2:
                    break;
            }

            //check for existing ingredient for meal locally
            //var ingredientToReferenceToUpdate = IngredientsForMealToReference.FirstOrDefault(il => il.MealId == selectedMeal.Id && il.tempId == IngredientForMealNumber);
            var ingredientForMealToUpdate = IngredientsForMealVM.FirstOrDefault(idb => idb.MealId == selectedMeal.Id && idb.tempId == IngredientForMealNumber);

            if (ingredientForMealToUpdate != null)
            {
                //check if the ingredients for meal existsted before
                //var ingredientForMealToUpdate = IngredientsForMealVM.FirstOrDefault(idb => idb.Id == ingredientToReferenceToUpdate.Id && idb.MealId == selectedMeal.Id);
                var ingredientToReferenceToUpdate = IngredientsForMealToReference.FirstOrDefault(il => il.Id == ingredientForMealToUpdate.Id && il.MealId == selectedMeal.Id);

                //check if new ingredient exists in the database
                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == saveAddString);

                //ingredient exists
                if (ingredientCheck != null)
                {
                    ingredientToReferenceToUpdate.Quantity = (int)saveAddQuantity;                  
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
                    ingredientToReferenceToUpdate.Quantity = (int)saveAddQuantity;
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
                        Quantity = (int)saveAddQuantity,
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
                        tempId = countedIngredientsForMealInsertId,
                        Id = countedIngredientsForMealInsertId,
                        MealId = selectedMeal.Id,
                        IngredientId = countedIngredientsInsertId,
                        IngredientName = saveAddString,
                        Quantity = (int)saveAddQuantity,
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
                if (selectedMeal != null && selectedIngredient1ForMeal != null)
                {
                    var asdasd = IngredientsForMealToReference.FirstOrDefault(i => i.IngredientId == selectedIngredient1ForMeal.Id);
                    if (asdasd != null)
                    {
                        AddStringIngredient1ForMeal = selectedIngredient1ForMeal.Name;
                        return selectedIngredient1ForMeal.Name;
                    }
                    else
                    {
                        return AddStringIngredient1ForMeal;
                    }
                }
                else
                {
                    return AddStringIngredient1ForMeal;
                }
            }
            set
            {
                if (selectedMeal != null)
                {
                    if (selectedIngredient1ForMeal != null)
                    {
                        var tempIngredientforMeal = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1);
                        var existingIngredient = IngedientsListVM.FirstOrDefault(i => i.Name == value);
                        if (existingIngredient != null)
                        {
                            //if meal already have ingeredientXforMeal
                            if (tempIngredientforMeal != null)
                            {
                                tempIngredientforMeal.IngredientName = value;
                                tempIngredientforMeal.IngredientId = existingIngredient.Id;
                                AddStringIngredient1ForMeal = value;
                            }
                            else
                            {
                                int countForId = IngredientsForMealVM.Count() + 1;

                                IngredientsForMealToReference.Add(new IngredientForMealViewModel
                                {
                                    tempId = 1,
                                    Id = countForId,
                                    MealId = SelectedMeal.Id,
                                    IngredientId = existingIngredient.Id,
                                    IngredientName = existingIngredient.Name,
                                    Quantity = 0,
                                    Unit = ""
                                });

                                AddStringIngredient1ForMeal = value;
                            }
                        }
                        else
                            AddStringIngredient1ForMeal = value;
                    }
                    else
                    {
                        AddStringIngredient1ForMeal = value;
                    }
                }
            }
        }

        private int? AddQuantityIngredient1ForMeal { get; set; }
        public int? QuantityIngredient1ForMeal
        {
            get
            {
                //if there is selected meal
                if (selectedMeal != null)
                {
                    //if there is selected ingredient 
                    if (selectedIngredient1ForMeal != null)
                    {
                        var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (quantityIngredient1ForMealReference != null)
                        {
                            if (quantityIngredient1ForMealReference.IngredientId == selectedIngredient1ForMeal.Id)
                            {
                                AddQuantityIngredient1ForMeal = quantityIngredient1ForMealReference.Quantity;
                                return quantityIngredient1ForMealReference.Quantity;
                            }

                            AddQuantityIngredient1ForMeal = null;
                            return AddQuantityIngredient1ForMeal;
                        }
                        else
                            AddQuantityIngredient1ForMeal = null;
                        return AddQuantityIngredient1ForMeal;
                    }
                    else
                        return AddQuantityIngredient1ForMeal;
                }
                else
                    return null;
            }
            set
            {
                //if there is selected meal
                if (selectedMeal != null)
                {
                    if (selectedIngredient1ForMeal != null)
                    {
                          var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (quantityIngredient1ForMealReference != null)
                        {
                            if (quantityIngredient1ForMealReference.IngredientId == selectedIngredient1ForMeal.Id)
                            {
                                quantityIngredient1ForMealReference.Quantity = (int)value;
                                AddQuantityIngredient1ForMeal = (int)value;
                            }
                            else
                                AddQuantityIngredient1ForMeal = (int)value;
                        }
                    }
                    AddQuantityIngredient1ForMeal = (int)value;
                }
            }
        }
                 
        private string? AddUnitIngredient1ForMeal { get; set; }

        public string? UnitIngredient1ForMeal
        {
            get
            {
                //if there is selected meal
                if (selectedMeal != null)
                {
                    //if there is selected ingredient 
                    if (selectedIngredient1ForMeal != null)
                    {                     
                        var unitIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (unitIngredient1ForMealReference != null)
                        {
                            if (unitIngredient1ForMealReference.IngredientId == selectedIngredient1ForMeal.Id)
                            {
                                AddUnitIngredient1ForMeal = unitIngredient1ForMealReference.Unit;
                                return unitIngredient1ForMealReference.Unit;
                            }

                            AddUnitIngredient1ForMeal = "";
                            return AddUnitIngredient1ForMeal;
                        }
                        else
                            AddUnitIngredient1ForMeal = "";
                        return AddUnitIngredient1ForMeal;
                    }
                    else
                        return AddUnitIngredient1ForMeal;
                }
                else
                    return "";
            }
            set
            {
                //if there is selected meal
                if (selectedMeal != null)
                {
                    if (selectedIngredient1ForMeal != null)
                    {
                        var unitIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (unitIngredient1ForMealReference != null)
                        {
                            if (unitIngredient1ForMealReference.IngredientId == selectedIngredient1ForMeal.Id)
                            {
                                unitIngredient1ForMealReference.Unit = value;
                                AddUnitIngredient1ForMeal = value;
                            }
                            else
                                AddUnitIngredient1ForMeal = value;
                        }
                    }
                    AddUnitIngredient1ForMeal = value;
                }
            }
        }
    }
}
