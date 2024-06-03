using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class ExtrasViewModel : BaseViewModel
    {
        public ICommand GenerateDatabaseScript { get; set; }
        public ExtrasViewModel()
        {
            GenerateDatabaseScript = new RelayCommand(GenerateDBScript);
            ReloadVMTables();
        }
        private void GenerateDBScript()
        {
            string txtFileName = "DbBackup.txt";
            string txtFinalPath = Path.Combine(xlsFilePath, txtFileName);

            using (StreamWriter sw = new StreamWriter(txtFinalPath))
            {
                sw.WriteLine("/*ShoppingApp database backup*/");
                sw.WriteLine("/*----------------CREATING TABLES------------------*/");
                sw.WriteLine("/*");
                sw.WriteLine("CREATE TABLE \"Ingredients\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Ingredients\" PRIMARY KEY,\r\n    \"Name\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"Meals\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Meals\" PRIMARY KEY,\r\n    \"MealName\" TEXT NOT NULL,\r\n    \"MealRecipe\" TEXT NOT NULL,\r\n    \"MealType\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"Items\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Items\" PRIMARY KEY,\r\n    \"ItemName\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"IngredientForMeal\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_IngredientForMeal\" PRIMARY KEY,\r\n    \"MealId\" INTEGER NOT NULL,\r\n    \"IngredientId\" INTEGER NOT NULL,\r\n    \"Quantity\" REAL NOT NULL,\r\n    \"Unit\" TEXT NOT NULL\r\n)");
                sw.WriteLine("*/");
                sw.WriteLine("/*----------------TABLES CREATED-------------------*/");
                sw.WriteLine("/*--------INGREDIENTS----------*/");
                sw.WriteLine(GetIngredients());
                sw.WriteLine("/*-----------MEALS-------------*/");
                sw.WriteLine(GetMeals());
                sw.WriteLine("/*----INGREDIENTS_FOR_MEALS----*/");
                sw.WriteLine(GetIngredientsForMEals());
                sw.WriteLine("/*----ITEMS----*/");
                sw.WriteLine(GetItems());

            }
        }

        private string GetIngredients()
        {
            string allIngredientsInsert = "";
            string ingredientInsert = "";
            foreach(var ingredient in IngedientsListVM)
            {
                ingredientInsert = "";
                ingredientInsert = "INSERT INTO INGREDIENTS VALUES (" + ingredient.Id + ", '" + ingredient.Name + "');\r\n";
                allIngredientsInsert += ingredientInsert;
            }
            return allIngredientsInsert;
        }

        private string GetMeals()
        {
            string allMealsInsert = "";
            string mealInsert = "";
            foreach (var meal in MealsList)
            {
                mealInsert = "";
                mealInsert = "INSERT INTO MEALS VALUES (" + meal.Id + ", '" + meal.MealName + "', '" + meal.MealRecipe + "', '" + meal.MealType + "');\r\n";
                allMealsInsert += mealInsert;
            }
            return allMealsInsert;
        }
        private string GetIngredientsForMEals()
        {
            string allIFMInsert = "";
            string IFMInsert = "";
            foreach (var ifm in IngredientsForMealVM)
            {
                IFMInsert = "";
                IFMInsert = "INSERT INTO INGREDIENTFORMEAL VALUES (" + ifm.Id + ", " + ifm.MealId + ", " + ifm.IngredientId + ", " + ifm.Quantity.ToString().Replace(',','.') + ", '" + ifm.Unit + "');\r\n";
                allIFMInsert += IFMInsert;
            }
            return allIFMInsert;
        }
        private string GetItems()
        {
            string allItemsInsert = "";
            string ItemInsert = "";
            foreach (var item in ItemsVM)
            {
                ItemInsert = "";
                ItemInsert = "INSERT INTO ITEMS VALUES (" + item.Id + ", '" + item.ItemName + "');\r\n";
                allItemsInsert += ItemInsert;
            }
            return allItemsInsert;
        }
    }
}
