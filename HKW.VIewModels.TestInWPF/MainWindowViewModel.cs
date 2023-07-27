using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls;

namespace HKW.VIewModels.TestInWPF;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    public ContextMenuVM contextMenu =
        new(() =>
        {
            ObservableCollection<MenuItemVM> items = new();
            MenuItemVM menuItem = new();
            items.Add(menuItem);
            return items;
        });
}
