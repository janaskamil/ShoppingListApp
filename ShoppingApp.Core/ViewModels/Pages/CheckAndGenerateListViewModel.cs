using Microsoft.EntityFrameworkCore;
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
        
        public CheckAndGenerateListViewModel()
        {
            CreateXls = new RelayCommand(GenerateXLS);
            AddItems = new RelayCommand(AddItemsToList);
            SaveItems = new RelayCommand(SaveItemsToDB);
            DeleteItems = new RelayCommand(DeleteItemsFromDB);
            DeleteShoppingListWithIngredientsCommand = new RelayCommand(DeleteShoppingListWithIngredients);
            ReloadVMTables();
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
            var checkedItems = ItemsVM.Where(i=>i.isChecked).ToList();
            foreach(var item in checkedItems)
            {
                var checkForExistingItem = IngredientsToBuy.FirstOrDefault(i => i.IngredientName.Equals(item.ItemName));

                if(checkForExistingItem != null)
                {
                    IngredientsToBuy.Remove(checkForExistingItem);
                    checkForExistingItem.Quantity += 1;
                    IngredientsToBuy.Add(checkForExistingItem);
                }
                else
                {
                    var newItemOnTheList = new IngredientsToListViewModel
                    {
                        IngredientId = -1,
                        IngredientName = item.ItemName.ToUpper(),
                        Quantity = 1,
                        Unit = "SZT",
                        Regdate = currentTimestamp
                    };
                    IngredientsToBuy.Add(newItemOnTheList);
                }               
            }
            ReloadVMTables();

        }
        private void DeleteItemsFromDB()
        {
            var checkedItems = ItemsVM.Where(i => i.isChecked).ToList();
            foreach(var item in checkedItems)
            {
                ItemsVM.Remove(item);
                var itemToRemoveDB = DatabaseCreationTool.MyDatabase.Items.Where(i=>i.Id == item.Id).FirstOrDefault();
                if(itemToRemoveDB != null)
                {
                    DatabaseCreationTool.MyDatabase.Items.Remove(itemToRemoveDB);
                }
            }
            DatabaseCreationTool.MyDatabase.SaveChanges();
            ReloadVMTables();
        }
        private void SaveItemsToDB()
        {   
            foreach (var item in ItemsVM)
            {
                var currentItemName = DatabaseCreationTool.MyDatabase.Items.Where(i => i.ItemName == item.ItemName.ToUpper()).FirstOrDefault();
                //new item
                if(currentItemName == null)
                {
                    var lastItemId = DatabaseCreationTool.MyDatabase.Items.OrderByDescending(i => i.Id).FirstOrDefault();
                    int countedLastItemToInsert = lastItemId != null ? lastItemId.Id : 0;
                    countedLastItemToInsert += 1;

                    var NewItem = new ItemViewModel
                    {
                        Id = countedLastItemToInsert,
                        ItemName = item.ItemName.ToUpper(),
                        isChecked = true
                    };
                    DatabaseCreationTool.MyDatabase.Items.Add(new Items
                    {
                        Id = NewItem.Id,
                        ItemName = NewItem.ItemName,
                    });
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                }
                //existing item - do nothing              
            }          
            ReloadVMTables();
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
                // set column widths
                worksheet.Column(1).Width = 35; 
                worksheet.Column(2).Width = 15; 
                worksheet.Column(3).Width = 20; 
                foreach (var ingredient in IngredientsToBuy)
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
                // set column widths
                worksheet2.Column(1).Width = 25; 
                worksheet2.Column(2).Width = 15; 
                worksheet2.Column(3).Width = 150;              
                worksheet2.Column(3).Style.WrapText = true;
                foreach (var meal in MealsForShoppingList)
                {
                    worksheet2.Cells[rowMeal, 1].Value = meal.MealName;
                    worksheet2.Cells[rowMeal, 2].Value = meal.MealCount;
                    worksheet2.Cells[rowMeal, 3].Value = meal.MealRecipe;
                    worksheet2.Row(rowMeal).Height = 70;
                    rowMeal += 1;
                }
                package.Save();
            }
        }
    }
}
