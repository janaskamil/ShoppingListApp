﻿using System;
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
    /// Interaction logic for CreateList.xaml
    /// </summary>
    public partial class GenerateList : UserControl
    {
        public GenerateList()
        {
            InitializeComponent();

            DataContext = new GenerateListViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
