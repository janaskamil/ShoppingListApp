using OfficeOpenXml;
using ShoppingApp.Core.Helpers;
using ShoppingApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core
{
    public class CheckAndGenerateListViewModel :BaseViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();
        public CheckAndGenerateListViewModel()
        {
            CreateXls = new RelayCommand(GenerateXLS);
            AddItems = new RelayCommand(AddItemsToList);
        }

        private void AddItemsToList()
        {
            //declaring moment of list generation
            DateTime currentTimestamp = DateTime.Now;
            //checking if list already exists 
            if (IngredientsToBuy.Count() > 0)
            {
                var timeCheck = IngredientsToBuy.FirstOrDefault();
                timeCheck.Regdate = currentTimestamp;
            }
            var newItemOnTheList = new IngredientsToListViewModel
            {
                IngredientId = -1,
                IngredientName = "test",
                Quantity = 1,
                Unit = "szt",
                Regdate = currentTimestamp
            };
            IngredientsToBuy.Add(newItemOnTheList);
        }
       
        private void GenerateXLS()
        {
            //declare name
            DateTime now = DateTime.Now;
            string xlsFileName = now.ToString("yyyyMMddHHmmss") + ".xlsx";
            //declare final path
            string xlsFinalPath = Path.Combine(xlsFilePath, xlsFileName);
            int rowIngredient = 2;
            int rowMeal = 2;

            //create xls
            FileInfo file = new FileInfo(xlsFinalPath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("INGREDIENTS");
                worksheet.Cells[1, 1].Value = "INGREDIENT";
                worksheet.Cells[1, 2].Value = "QUANTITY";
                worksheet.Cells[1, 3].Value = "UNIT";
                foreach(var ingredient in IngredientsToBuy)
                {
                    worksheet.Cells[rowIngredient, 1].Value = ingredient.IngredientName;
                    worksheet.Cells[rowIngredient, 2].Value = ingredient.Quantity;
                    worksheet.Cells[rowIngredient, 3].Value = ingredient.Unit;
                    rowIngredient += 1;
                }

                ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("MEALS WITH RECIPES");
                worksheet2.Cells[1, 1].Value = "MEAL NAME";
                worksheet2.Cells[1, 2].Value = "QUANTITY";
                worksheet2.Cells[1, 3].Value = "RECIPE";
                foreach (var meal in MealsForShoppingList)
                {
                    worksheet2.Cells[rowMeal, 1].Value = meal.MealName;
                    worksheet2.Cells[rowMeal, 2].Value = meal.MealCount;
                    worksheet2.Cells[rowMeal, 3].Value = meal.MealRecipe;
                    rowMeal += 1;
                }
                package.Save();
            }

        }

    }
}
