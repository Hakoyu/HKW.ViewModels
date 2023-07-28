using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls;
using HKW.HKWViewModels.Controls.Attachment;

namespace HKW.VIewModels.TestInWPF;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ControlVM<Test> _control = new() { Attachment = new() { Name = "114" } };

    //[ObservableProperty]
    //private ContextMenuVM _contextMenu =
    //    new(() =>
    //    {
    //        ObservableCollection<MenuItemVM> items = new();
    //        MenuItemVM menuItem = new();
    //        items.Add(menuItem);
    //        return items;
    //    });

    ButtonVM buttonVM = new() { };

    [RelayCommand]
    private void Button()
    {
        Control.Attachment!.Name = "514";
    }
}

public class Test
{
    public string Name { get; set; }
}
