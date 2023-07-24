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
                    SaveIngredientForMealLogic(selectedIngredient1ForMeal, 1);
                    
                    
                    //save changes to the database
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    

                    //clear everything to force user to select new meal to create/edit after saving
                    SelectedMeal = null;
                    AddMealName = null;
                    AddMealRecipe = null;
                    AddMealType = null;
                    SelectedIngredient1ForMeal = null;
                    AddStringIngredient1ForMeal = null;
                    AddQuantityIngredient1ForMeal = null;
                    AddUnitIngredient1ForMeal = null;
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
                    SelectedMeal = null;
                    AddMealName = null;
                    AddMealRecipe = null;
                    AddMealType = null;
                    SelectedIngredient1ForMeal = null;
                    AddStringIngredient1ForMeal = null;
                    AddQuantityIngredient1ForMeal = null;
                    AddUnitIngredient1ForMeal = null;
                }
            }                    
        }

        //deleting existing meals
        public void DeleteMeal()
        {
            if (selectedMeal != null)
            {

                var mealToDelete = DatabaseCreationTool.MyDatabase.Meals.Find(selectedMeal.Id);
                DatabaseCreationTool.MyDatabase.Meals.Remove(mealToDelete);
                DatabaseCreationTool.MyDatabase.SaveChanges();
                MealsList.Remove(selectedMeal);
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
        
        public void SaveIngredientForMealLogic(IngreditenViewModel ingredientToUpdate, int IngredientForMealNumber)
        {
            switch(IngredientForMealNumber)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

            //check for existing ingredient for meal locally
            var ingredient1toUpdateLocal = IngredientsForMealToReference.FirstOrDefault(il => il.MealId == selectedMeal.Id && il.tempId == IngredientForMealNumber);
                        
            if (ingredient1toUpdateLocal != null)
            {
                //check if the ingredients for meal existsted before
                var ingredient1ToUpdateReference = IngredientsForMealVM.FirstOrDefault(idb => idb.Id == ingredient1toUpdateLocal.Id && idb.MealId == selectedMeal.Id);

                //check if new ingredient exists in the database
                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == AddStringIngredient1ForMeal);

                //ingredient exists
                if (ingredientCheck != null)
                {
                    ingredient1toUpdateLocal.Quantity = (int)AddQuantityIngredient1ForMeal;
                    ingredient1ToUpdateReference.Quantity = (int)AddQuantityIngredient1ForMeal;
                    
                    ingredient1toUpdateLocal.Unit = AddUnitIngredient1ForMeal;
                    ingredient1ToUpdateReference.Unit = AddUnitIngredient1ForMeal;
                }
                //new ingredient
                else
                {
                    var countedIngredients = IngedientsListVM.ToList();
                    int countedIngredientsInsertId;
                    if (countedIngredients.Count > 0)
                    {
                        countedIngredientsInsertId = IngedientsListVM.OrderByDescending(i => i.Id).First().Id + 1;
                    }
                    else
                    {
                        countedIngredientsInsertId = 1;
                    }
                    //adding NEW ingredient to the Ingredients local + database
                    var newIngredient1 = new IngreditenViewModel
                    {
                        Id = countedIngredientsInsertId,
                        Name = AddStringIngredient1ForMeal
                    };

                    IngedientsListVM.Add(newIngredient1);

                    //adding new ingredient to the database 
                    DatabaseCreationTool.MyDatabase.Ingredients.Add(new Ingredient
                    {
                        Id = newIngredient1.Id,
                        Name = newIngredient1.Name
                    });

                    ingredient1toUpdateLocal.IngredientId = newIngredient1.Id;
                    ingredient1ToUpdateReference.IngredientId = newIngredient1.Id;

                    ingredient1toUpdateLocal.Quantity = (int)AddQuantityIngredient1ForMeal;
                    ingredient1ToUpdateReference.Quantity = (int)AddQuantityIngredient1ForMeal;

                    ingredient1toUpdateLocal.Unit = AddUnitIngredient1ForMeal;
                    ingredient1ToUpdateReference.Unit = AddUnitIngredient1ForMeal;
                }

                var ingredient1ToUpdateDB = DatabaseCreationTool.MyDatabase.IngredientForMeal.FirstOrDefault(i => i.Id == ingredient1ToUpdateReference.Id);
                if (ingredient1ToUpdateDB != null)
                {
                    ingredient1ToUpdateDB.MealId = ingredient1toUpdateLocal.MealId;
                    ingredient1ToUpdateDB.IngredientId = ingredient1toUpdateLocal.IngredientId;
                    ingredient1ToUpdateDB.Quantity = ingredient1toUpdateLocal.Quantity;
                    ingredient1ToUpdateDB.Unit = ingredient1toUpdateLocal.Unit;
                }

                //var entry = DatabaseCreationTool.MyDatabase.Entry(ingredient1ToUpdateDB);
                //entry.State = EntityState.Detached;
            }
            //if ingredient FOR MEAL is new
            else
            {
                var countedIngredientsForMeals = IngredientsForMealVM.Where(i => i.MealId == selectedMeal.Id).ToList();
                int countedIngredientsForMealsInsertId;
                if (countedIngredientsForMeals.Count > 0)
                {
                    countedIngredientsForMealsInsertId = DatabaseCreationTool.MyDatabase.IngredientForMeal.OrderByDescending(m => m.Id).First().Id + 1;
                }
                else
                {
                    countedIngredientsForMealsInsertId = 1;
                }
                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == AddStringIngredient1ForMeal);
                //if ingredient already exists
                if (ingredientCheck != null)
                {
                    var newIngredient1FromMeal = new IngredientForMealViewModel
                    {
                        tempId = 1,
                        Id = countedIngredientsForMealsInsertId,
                        MealId = selectedMeal.Id,
                        IngredientId = selectedIngredient1ForMeal.Id,
                        IngredientName = AddStringIngredient1ForMeal,
                        Quantity = (int)AddQuantityIngredient1ForMeal,
                        Unit = AddUnitIngredient1ForMeal
                    };
                    IngredientsForMealVM.Add(newIngredient1FromMeal);
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Add(new IngredientForMeal
                    {
                        Id = newIngredient1FromMeal.Id,
                        MealId = newIngredient1FromMeal.MealId,
                        IngredientId = 10,
                        Quantity = 0,
                        Unit = "test"
                    });
                }
                //new ingredient
                else
                {
                    var countedIngredients = IngedientsListVM.ToList();
                    int countedIngredientsInsertId;
                    if (countedIngredients.Count > 0)
                    {
                        countedIngredientsInsertId = DatabaseCreationTool.MyDatabase.Ingredients.OrderByDescending(i => i.Id).First().Id + 1;
                    }
                    else
                    {
                        countedIngredientsInsertId = 1;
                    }

                    var newIngredient1 = new IngreditenViewModel
                    {
                        Id = countedIngredientsInsertId,
                        Name = AddStringIngredient1ForMeal
                    };
                    IngedientsListVM.Add(newIngredient1);
                    DatabaseCreationTool.MyDatabase.Ingredients.Add(new Ingredient
                    {
                        Id= newIngredient1.Id,
                        Name = newIngredient1.Name
                    });

                    var xd = IngredientsForMealVM.ToList();
                    int xdint;
                    if (xd.Count > 0)
                    {
                        xdint = DatabaseCreationTool.MyDatabase.IngredientForMeal.OrderByDescending(m => m.Id).First().Id + 1;
                    }
                    else
                    {
                        xdint = 1;
                    }

                    var newIngredient1FromMeal = new IngredientForMealViewModel
                    {
                        tempId = countedIngredientsForMealsInsertId,
                        Id = xdint,
                        MealId = selectedMeal.Id,
                        IngredientId = countedIngredientsInsertId,
                        IngredientName = AddStringIngredient1ForMeal,
                        Quantity = (int)AddQuantityIngredient1ForMeal,
                        Unit = AddUnitIngredient1ForMeal
                    };
                    IngredientsForMealVM.Add(newIngredient1FromMeal);
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Add(new IngredientForMeal
                    {
                        Id = newIngredient1FromMeal.Id,
                        MealId = newIngredient1FromMeal.MealId,
                        IngredientId = newIngredient1FromMeal.IngredientId,
                        Quantity = (int)AddQuantityIngredient1ForMeal,
                        Unit = AddUnitIngredient1ForMeal
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
                        var quantityIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (quantityIngredient1ForMeal != null)
                        {
                            if (quantityIngredient1ForMeal.IngredientId == selectedIngredient1ForMeal.Id)
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
                        var quantityIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);

                        //if ingredient 1 for selected meal has value
                        if (quantityIngredient1ForMeal != null)
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
                        var unitIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        var unitIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        //if ingredient 1 for selected meal has value                   
                        if (unitIngredient1ForMeal != null)
                        {
                            if (unitIngredient1ForMeal.IngredientId == selectedIngredient1ForMeal.Id)
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
                        var unitIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                        var unitIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);

                        //if ingredient 1 for selected meal has value
                        if (unitIngredient1ForMeal != null)
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
