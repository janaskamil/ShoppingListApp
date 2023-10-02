using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class DeleteAndModifyIngredients : BaseViewModel
    {
        public ObservableCollection<IngreditenViewModel> UnusedIngredients { get; set; } = new ObservableCollection<IngreditenViewModel>();
        public ObservableCollection<IngreditenViewModel> UsedIngredients { get; set; } = new ObservableCollection<IngreditenViewModel>();
        public IngreditenViewModel SelectedIngredientToModify { get; set; }
        public ICommand DeleteIngredients { get; set; }
        public ICommand UpdateIngredient { get; set; }

        public DeleteAndModifyIngredients()
        {
            DeleteIngredients = new RelayCommand(DeleteCheckedIngredients);
            UpdateIngredient = new RelayCommand(UpdateSelectedIngredient);
            Reload();
        }
        private string AddStringSelectedIngredient { get; set; }
        public string StringSelectedIngredient
        {
            get
            {
                if (SelectedIngredientToModify != null)
                {
                    var xd = AddStringSelectedIngredient;
                    xd = SelectedIngredientToModify.Name;
                    return xd;
                }
                else
                    return string.Empty;
            }
            set
            {
                AddStringSelectedIngredient = value;
            }
        }

        private void Reload()
        {
            UnusedIngredients.Clear();
            UsedIngredients.Clear();
            var unusedIngredientsQuery = from ingredient in DatabaseCreationTool.MyDatabase.Ingredients
                                         where !DatabaseCreationTool.MyDatabase.IngredientForMeal
                                                  .Any(i => i.IngredientId == ingredient.Id)
                                         select ingredient;
            foreach (var ing in unusedIngredientsQuery)
            {
                UnusedIngredients.Add(new IngreditenViewModel
                {
                    isSelected = false,
                    Id = ing.Id,
                    Name = ing.Name
                }); 
            }

            var usedIngredientsQuery = from ingredient in DatabaseCreationTool.MyDatabase.Ingredients
                                       where DatabaseCreationTool.MyDatabase.IngredientForMeal
                                                .Any(i => i.IngredientId == ingredient.Id)
                                       select ingredient;
            foreach (var ing in usedIngredientsQuery)
            {
                UsedIngredients.Add(new IngreditenViewModel
                {
                    isSelected = false,
                    Id = ing.Id,
                    Name = ing.Name
                }); 
            }
        }
      
        public void DeleteCheckedIngredients()
        {
            var checkedIngredient = UnusedIngredients.Where(x => x.isSelected == true).ToList();

            foreach (var ingredient in checkedIngredient)
            {
                var ingredientToDelete = DatabaseCreationTool.MyDatabase.Ingredients.FirstOrDefault(x=>x.Id == ingredient.Id);

                if (ingredientToDelete != null)
                {
                    DatabaseCreationTool.MyDatabase.Ingredients.Remove(ingredientToDelete);
                }
            }
            DatabaseCreationTool.MyDatabase.SaveChanges();
            Reload();
        }

        public void UpdateSelectedIngredient()
        {
            if(SelectedIngredientToModify != null)
            {
                var checkForExistingIngredientName = DatabaseCreationTool.MyDatabase.Ingredients.FirstOrDefault(x => x.Name.Equals(AddStringSelectedIngredient));
                var updatedIngredient = DatabaseCreationTool.MyDatabase.Ingredients.FirstOrDefault(x => x.Id == SelectedIngredientToModify.Id);

                if (checkForExistingIngredientName == null)
                {            
                    updatedIngredient.Name = AddStringSelectedIngredient;
                    //save changes to the database
                    DatabaseCreationTool.MyDatabase.SaveChanges();
                }
            }
            Reload();
        }
    }
}
