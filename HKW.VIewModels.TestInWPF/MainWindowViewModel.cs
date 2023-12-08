using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels;
using System;
using System.Globalization;

namespace HKW.VIewModels.TestOnWPF;

public partial class MainWindowViewModel : ObservableObject
{
    public static ObservableI18nCore I18nCore { get; } = new() { };

    [ObservableProperty]
    private ObservableI18nResource<TestI18nRes> _i18n = I18nCore.Create<TestI18nRes>(new());

    [ObservableProperty]
    private bool _showCanExecute = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private bool _canExecute = true;

    [ObservableProperty]
    private string _text;

    //public ObservableValue<string> Text = new();

    public MainWindowViewModel() { }

    private void ClickCommand_CanExecuteChanged(object? sender, EventArgs e)
    {
        ShowCanExecute = CanExecute;
    }

    [RelayCommand]
    private void Click()
    {
        //await Task.Delay(1000);
        //if (I18nCore.CurrentCulture.Name == CultureName.CN)
        //    I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.EN);
        //else
        //    I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.CN);
        //CanExecute = false;
    }

    [RelayCommand]
    private void CultureChange()
    {
        if (I18nCore.CurrentCulture.Name == CultureName.CN)
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.EN);
        else
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.CN);
    }
}

public partial class Test : ObservableObject
{
    [ObservableProperty]
    private string? _name;
}

public static class CultureName
{
    public const string CN = "zh-CN";
    public const string EN = "en-US";
}
