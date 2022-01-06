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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsListWindow.xaml
    /// </summary>
    public partial class ParcelsListWindow : Window
    {
        IBL bl;
        public ParcelsListWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            parcelToLDataGrid.DataContext = bl.ParcelList();
            parcelToLDataGrid.IsReadOnly = true;
        }

        private void addParcelButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).ShowDialog();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void parcelToLDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToL curParcelToL = parcelToLDataGrid.SelectedItem as BO.ParcelToL;
            if (curParcelToL != null)
            {
                BO.Parcel p = bl.GetParcel(curParcelToL.Id);
                new ParcelWindow(bl, p, curParcelToL.Status).ShowDialog();
            }
        }

        private void GroupSenderCheckBox_Click(object sender, RoutedEventArgs e)
        {
            GroupReceiverCheckBox.IsEnabled = false;
            if (GroupSenderCheckBox.IsChecked == true)
            {
                List<IGrouping<string, BO.ParcelToL>> GroupingData = bl.ParcelList()
                    .GroupBy(b =>b.SenderName)
                    .ToList();
                parcelToLDataGrid2.DataContext = GroupingData;
                parcelToLDataGrid2.Visibility = Visibility.Visible;
                parcelToLDataGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                parcelToLDataGrid.Visibility = Visibility.Visible;
                parcelToLDataGrid2.Visibility = Visibility.Collapsed;
            }
        }

        private void GroupReceiverCheckBox_Click(object sender, RoutedEventArgs e)
        {
            GroupSenderCheckBox.IsEnabled = false;
            if (GroupSenderCheckBox.IsChecked == true)
            {
                List<IGrouping<string, BO.ParcelToL>> GroupingData = bl.ParcelList()
                    .GroupBy(b => b.ReceiverName)
                    .ToList();
                parcelToLDataGrid2.DataContext = GroupingData;
                parcelToLDataGrid2.Visibility = Visibility.Visible;
                parcelToLDataGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                parcelToLDataGrid.Visibility = Visibility.Visible;
                parcelToLDataGrid2.Visibility = Visibility.Collapsed;
            }
        }

        private void group(IEnumerable<BO.ParcelToL> parcels)
        {
            if (GroupSenderCheckBox.IsChecked == true)
            {
                List<IGrouping<string, BO.ParcelToL>> GroupingData = parcels
                        .GroupBy(b => b.SenderName)
                        .ToList();
                parcelToLDataGrid2.DataContext = GroupingData;
            }
            else
            {
                List<IGrouping<string, BO.ParcelToL>> GroupingData = parcels
                        .GroupBy(b => b.ReceiverName)
                        .ToList();
                parcelToLDataGrid2.DataContext = GroupingData;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            parcelToLDataGrid.DataContext = bl.ParcelList();
        }
    }
}
