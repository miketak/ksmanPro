﻿using DataObjects;
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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for subfrmManageEmployee.xaml
    /// </summary>
    public partial class subfrmManageEmployee 
    {
        User _user = null;
        public subfrmManageEmployee(User user)
        {
            _user = user;
            InitializeComponent();
            txtName.Text = _user.FirstName + " " + _user.LastName;
        }

        private void globalEmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnSearch(object sender, RoutedEventArgs e)
        {

        }
    }
}