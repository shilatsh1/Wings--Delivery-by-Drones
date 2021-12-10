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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBL.BL bL;
        public DroneListWindow(IBL.BL bl)
        {
            InitializeComponent();
            bL = bl;
            DronesListView.ItemsSource = bl.DroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));

        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bL.GetDronesByPerdicate(d => d.Status == (IBL.BO.DroneStatus)StatusSelector.SelectedItem);
        }
    }
}