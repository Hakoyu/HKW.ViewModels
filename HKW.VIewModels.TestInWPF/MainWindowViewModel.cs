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
using HKW.HKWViewModels.SimpleObservable;

namespace HKW.VIewModels.TestOnWPF;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableValue<string> Text { get; } = new("aaa");

    [ObservableProperty]
    private ObservableI18n<Text> _i18n = ObservableI18n<Text>.Create(new());

    public MainWindowViewModel()
    {
        Text.PropertyChanged += Text_PropertyChanged;
    }

    private void Text_PropertyChanged(
        object? sender,
        System.ComponentModel.PropertyChangedEventArgs e
    )
    {
        throw new NotImplementedException();
    }
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
