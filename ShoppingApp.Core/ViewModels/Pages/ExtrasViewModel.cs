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
        }
        private void GenerateDBScript()
        {
            string txtFileName = "DbBackup.txt";
            string txtFinalPath = Path.Combine(xlsFilePath, txtFileName);

            using (StreamWriter sw = new StreamWriter(txtFinalPath))
            {
                sw.WriteLine("/*ShoppingApp database backup*/");
                sw.WriteLine("/*");
                sw.WriteLine("--------------CREATING TABLES---------------");
                sw.WriteLine("CREATE TABLE \"Ingredients\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Ingredients\" PRIMARY KEY,\r\n    \"Name\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"Meals\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Meals\" PRIMARY KEY,\r\n    \"MealName\" TEXT NOT NULL,\r\n    \"MealRecipe\" TEXT NOT NULL,\r\n    \"MealType\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"Items\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Items\" PRIMARY KEY,\r\n    \"ItemName\" TEXT NOT NULL\r\n)");
                sw.WriteLine("CREATE TABLE \"IngredientForMeal\" (\r\n    \"Id\" INTEGER NOT NULL CONSTRAINT \"PK_IngredientForMeal\" PRIMARY KEY,\r\n    \"MealId\" INTEGER NOT NULL,\r\n    \"IngredientId\" INTEGER NOT NULL,\r\n    \"Quantity\" REAL NOT NULL,\r\n    \"Unit\" TEXT NOT NULL\r\n)");
                sw.WriteLine("--------------TABLES CREATED----------------");
                sw.WriteLine("*/");
                sw.WriteLine("--------------INSERTING DATA INTO TABLES----------------");
                sw.WriteLine(GetIngredients());

            }
        }

        private string GetIngredients()
        {
            return "xd";
        }
    }
}
