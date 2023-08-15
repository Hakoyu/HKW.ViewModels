using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels;
using HKW.HKWViewModels.Controls;
using HKW.HKWViewModels.Controls.Attachments;

namespace HKW.VIewModels.TestInWPF;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableI18n<Text> _i18n = ObservableI18n<Text>.Create(new());

    [ObservableProperty]
    private ObservableCollection<Test> _tests =
        new()
        {
            new() { Name = "1" },
            new() { Name = "2" },
            new() { Name = "3" },
            new() { Name = "4" },
            new() { Name = "5" }
        };

    [ObservableProperty]
    private ListBoxVM _listBox = new(new Func<ObservableCollection<ListBoxItemVM>>(() =>
            {
                var items = new ObservableCollection<ListBoxItemVM>();
                var item = new ListBoxItemVM() { Content = 1 };
                items.Add(item);
                item = new ListBoxItemVM() { Content = 2 };
                items.Add(item);
                item = new ListBoxItemVM() { Content = 3 };
                items.Add(item);
                item = new ListBoxItemVM() { Content = 4 };
                items.Add(item);
                item = new ListBoxItemVM() { Content = 5 };
                items.Add(item);
                return items;
            })());

    [RelayCommand]
    private void Click() { }
}

public partial class Test : ObservableObject
{
    [ObservableProperty]
    private string? _name;
}

public class Text
{
    public static string Name { get; } = "Name";
}
