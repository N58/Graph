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
using System.Windows.Shapes;

namespace Graph.Windows
{
    /// <summary>
    /// Interaction logic for EditMenu.xaml
    /// </summary>
    public partial class EditingMenu : Window
    {
        public EditingMenu()
        {
            InitializeComponent();
        }

        private void ConfirmEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
