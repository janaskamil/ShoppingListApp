using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class DeleteUnusedIngredientsViewModel : BaseViewModel
    {
        public ObservableCollection<IngreditenViewModel> UnusedIngredients { get; set; } = new ObservableCollection<IngreditenViewModel>();
        public ICommand DeleteIngredients { get; set; }

        public DeleteUnusedIngredientsViewModel()
        {
            DeleteIngredients = new RelayCommand(DeleteCheckedIngredients);
            Reload();
        }

        private void Reload()
        {
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
                }); ;
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
    }
}
