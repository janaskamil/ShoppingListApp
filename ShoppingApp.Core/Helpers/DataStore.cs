using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core.Helpers
{
    public class DataStore : INotifyPropertyChanged
    {
        private static DataStore _instance;
        public static DataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataStore();
                }
                return _instance;
            }
        }

        private ObservableCollection<MealViewModel> _mealsForShoppingList = new ObservableCollection<MealViewModel>();
        public ObservableCollection<MealViewModel> MealsForShoppingList
        {
            get { return _mealsForShoppingList; }
            set
            {
                _mealsForShoppingList = value;
                OnPropertyChanged(nameof(MealsForShoppingList));
            }
        }
        private ObservableCollection<IngredientsToListViewModel> _ingredientsToBuy = new ObservableCollection<IngredientsToListViewModel>();
        public ObservableCollection<IngredientsToListViewModel> IngredientsToBuy
        {
            get { return _ingredientsToBuy; }
            set
            {
                _ingredientsToBuy = value;
                OnPropertyChanged(nameof(IngredientsToBuy));
            }
        }

        // Implementacja interfejsu INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
