using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShoppingApp.Core;

namespace ShoppingApp
{ 
    /// <summary>
    /// Interaction logic for EditDeleteMeal.xaml
    /// </summary>
    public partial class EditDeleteMeal : UserControl
    {
        public EditDeleteMeal()
        {
            InitializeComponent();
            DataContext = new EditDeleteMealViewModel();
        }
    }
}
