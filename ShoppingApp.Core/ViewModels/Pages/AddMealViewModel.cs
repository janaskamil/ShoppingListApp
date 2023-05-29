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
        //collection of Ingredients for Meals to edit
        public ObservableCollection<IngredientForMealViewModel> IngredientsForMealVM { get; set; } = new ObservableCollection<IngredientForMealViewModel>();
        //collection of Ingredients for Meals to reference (check for changes)
        private List<IngredientForMealViewModel> IngredientsForMealToReference { get; set; } = new List<IngredientForMealViewModel>();

        private MealViewModel selectedMeal { get; set; }

        public MealViewModel SelectedMeal
        {
            get { return selectedMeal; }
            set
            {
                selectedMeal = value;
                if (selectedMeal != null)
                {
                    AddStringIngredient1ForMeal = null;
                    AddQuantityIngredient1ForMeal = null;
                    //all ingredient for meals to edit/save
                    foreach (var ingredientformeal in IngredientsForMealVM.Where(i => i.MealId == selectedMeal.Id).ToList())
                    {
                        IngredientsForMealToReference.Add(ingredientformeal);
                    }
                   
                    var temp1 = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                    if (temp1 != null)
                    {
                        selectedIngredient1ForMeal = IngedientsListVM.First(i => i.Id == temp1.Id);
                    }
                    else
                        selectedIngredient1ForMeal = null;
                }
            }
        }

        public IngreditenViewModel selectedIngredient1ForMeal { get; set; }
        public IngreditenViewModel SelectedIngredient1ForMeal
        {
            get {return selectedIngredient1ForMeal;}
            set
            {
                if (selectedMeal != null)
                {
                    AddStringIngredient1ForMeal = null;
                    AddQuantityIngredient1ForMeal = null;
                    var temp = IngredientsForMealVM.FirstOrDefault(i => i.MealId == selectedMeal.Id && i.tempId == 1);
                    if (temp != null)
                    {
                        selectedIngredient1ForMeal = value;
                    }
                    else
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
        private int? AddQuantityIngredient1ForMeal { get; set; }
        public int? QuantityIngredient1ForMeal
        {
            get
            {
                //if there is selected meal
                if (selectedMeal != null && selectedIngredient1ForMeal != null)
                {
                    var quantityIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                    var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                    //if ingredient 1 for selected meal has value                   
                    if (quantityIngredient1ForMeal != null)
                    {
                        if (quantityIngredient1ForMeal.IngredientId == selectedIngredient1ForMeal.Id)
                        {
                            return quantityIngredient1ForMeal.Quantity;
                        }

                        return AddQuantityIngredient1ForMeal;
                    }
                    else
                        return AddQuantityIngredient1ForMeal;
                }
                else
                    return 0;
            }
            set
            {
                //if there is selected meal
                if (selectedMeal != null && selectedIngredient1ForMeal != null)
                {
                    var quantityIngredient1ForMeal = IngredientsForMealVM.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);
                    var quantityIngredient1ForMealReference = IngredientsForMealToReference.FirstOrDefault(i => i.tempId == 1 && i.MealId == selectedMeal.Id);

                    //if ingredient 1 for selected meal has value
                    if (quantityIngredient1ForMeal != null)
                    {
                        if (quantityIngredient1ForMealReference.IngredientId == SelectedIngredient1ForMeal.Id)
                        {
                            quantityIngredient1ForMealReference.Quantity = (int)value;
                        }
                        else
                        AddQuantityIngredient1ForMeal = (int)value;
                    }


                }
            }
        }

        //method to save completly new and changes to existing meals
        public void SaveMeal()
        {
            if ((!String.IsNullOrEmpty(AddMealName)))
            {
                //update existing recipe
                if (AddMealName.Equals(SelectedMeal?.MealName.ToString()))
                {
                    var mealToUpdate = DatabaseCreationTool.MyDatabase.Meals.Find(SelectedMeal.Id);
                    mealToUpdate.MealRecipe = SelectedMeal.MealRecipe;
                    mealToUpdate.MealType = SelectedMeal.MealType;
                    TempName(SelectedIngredient1ForMeal);


                    DatabaseCreationTool.MyDatabase.SaveChanges();
                    //clear everything to force user to select new meal to create/edit after saving
                    AddMealName = null;
                    AddMealRecipe = null;
                    AddMealType = null;
                    AddStringIngredient1ForMeal = null;
                }
                //save new recipe
                else
                {
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

        private string AddStringIngredient1ForMeal;

        public string StringIngredient1ForMeal
        {
            get
            {
                if (selectedMeal != null && selectedIngredient1ForMeal != null)
                {
                    return selectedIngredient1ForMeal.Name;                      
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
                        var xd = IngredientsForMealToReference.FirstOrDefault(i => i.IngredientId == selectedIngredient1ForMeal.Id);
                        var xd2 = IngedientsListVM.FirstOrDefault(i => i.Name == value);
                        if(xd2 != null)
                        {
                            xd.IngredientName = value;
                            xd.IngredientId = xd2.Id;
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

        public void TempName(IngreditenViewModel ingredientToUpdate)
        {
            var ingredient1toUpdateLocal = IngredientsForMealVM.FirstOrDefault(il => il.MealId == selectedMeal.Id && il.tempId == 1);
            //if ingredient FOR MEAL exists
            if (ingredient1toUpdateLocal != null)
            {
                //if ingredient already exists
                var ingredient1ToUpdateDB = DatabaseCreationTool.MyDatabase.IngredientForMeal.FirstOrDefault(idb => idb.Id == ingredient1toUpdateLocal.Id);
                var ingredientCheck = IngedientsListVM.FirstOrDefault(i => i.Name == AddStringIngredient1ForMeal);
                if (ingredientCheck != null)
                {
                    ingredient1toUpdateLocal.IngredientId = SelectedIngredient1ForMeal.Id;
                    ingredient1ToUpdateDB.IngredientId = SelectedIngredient1ForMeal.Id;
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
                        Id = newIngredient1.Id,
                        Name = newIngredient1.Name
                    });

                    ingredient1toUpdateLocal.IngredientId = newIngredient1.Id;
                    ingredient1ToUpdateDB.IngredientId = newIngredient1.Id;
                }
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
                        IngredientId = SelectedIngredient1ForMeal.Id,
                        IngredientName = AddStringIngredient1ForMeal,
                        Quantity = 0,
                        Unit = "test"
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
                        Quantity = 0,
                        Unit = "testnew"
                    };
                    IngredientsForMealVM.Add(newIngredient1FromMeal);
                    DatabaseCreationTool.MyDatabase.IngredientForMeal.Add(new IngredientForMeal
                    {
                        Id = newIngredient1FromMeal.Id,
                        MealId = newIngredient1FromMeal.MealId,
                        IngredientId = newIngredient1FromMeal.IngredientId,
                        Quantity = 0,
                        Unit = "testnew"
                    });
                   
                }
               
            }
        }    
    }
}
