using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWUtils.Observable;
using HKW.HKWViewModels;
using System;
using System.Globalization;
using System.Linq;

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

    [ObservableProperty]
    private CheckGroup _checkGroup = new();

    //public ObservableValue<string> Text = new();
    public ObservableList<Test> Tests { get; set; } =
        new(Enumerable.Range(0, 100).Select(i => new Test() { Name = i.ToString() }));

    public MainWindowViewModel()
    {
        foreach (var test in Tests)
            CheckGroup.CheckInfos.Add(test);
    }

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

public partial class Test : ObservableObject, ICheckInfo
{
    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private bool _isChecked;

    [ObservableProperty]
    private bool _canCheck = true;
}

public static class CultureName
{
    public const string CN = "zh-CN";
    public const string EN = "en-US";
}
