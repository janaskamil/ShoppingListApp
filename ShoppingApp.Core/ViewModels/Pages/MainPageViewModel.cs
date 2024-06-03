using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingApp.Core
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IMessageService messageService;
        public BaseViewModel _currentChildView = new GenerateListViewModel();
        public ICommand CloseApp { get; set; }
        public ICommand MinimalizeApp { get; set; }
        public ICommand ShowAddMeal { get; set; }
        public ICommand ShowCheckAndGenerateList { get; set; }
        public ICommand ShowGenerateList { get; set; }
        public ICommand ShowDeleteUnusedIngredients { get; set; }
        public ICommand ShowExtras { get; set; }

        public BaseViewModel CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
            }
        }

        public MainPageViewModel(IMessageService messageService)
        {
            this.messageService = messageService;
            CloseApp = new RelayCommand(CloseApplicationMainPageViewModel);
            MinimalizeApp = new RelayCommand(MinimizeApplicationMainPageViewModel);
            ShowAddMeal = new RelayCommand(ShowAddMealViewCommand);
            ShowDeleteUnusedIngredients = new RelayCommand(ShowDeleteUnusedIngredientsCommand);
            ShowCheckAndGenerateList = new RelayCommand(ShowCheckAndGenerateViewCommand);
            ShowGenerateList = new RelayCommand(ShowGenerateViewCommand);
            ShowExtras = new RelayCommand(ShowExtrasCommand);        
        }        
        private void ShowAddMealViewCommand()
        {
            CurrentChildView = new AddMealViewModel();
        }

        private void ShowCheckAndGenerateViewCommand()
        {
            CurrentChildView = new CheckAndGenerateListViewModel();
        }
        private void ShowGenerateViewCommand()
        {
            CurrentChildView = new GenerateListViewModel();
        }
        private void ShowDeleteUnusedIngredientsCommand()
        {
            CurrentChildView = new DeleteAndModifyIngredients();
        }
        private void ShowExtrasCommand()
        {
            CurrentChildView = new ExtrasViewModel();
        }

        private void CloseApplicationMainPageViewModel()
        {
            messageService.CloseApplication();
        }

        private void MinimizeApplicationMainPageViewModel()
        {
            messageService.MinimalizeApplication();
        }
    }
}
