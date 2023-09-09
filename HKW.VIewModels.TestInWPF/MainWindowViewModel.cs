using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public static ObservableI18nCore I18nCore { get; } = new() { };

    [ObservableProperty]
    private ObservableI18nRes<TestI18nRes> _i18n = I18nCore.Create<TestI18nRes>(new());

    [ObservableProperty]
    private bool _showCanExecute = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private bool _canExecute = true;

    public ObservableCommand Command1 { get; } = new();

    public ObservableValue<string> Str1 { get; } = new("114514");
    public ObservableValue<string> Str2 { get; } = new();

    public MainWindowViewModel()
    {
        Command1.AddNotifyReceiver(Str1);
        Command1.NotifyCanExecuteReceived += Command1_NotifyCanExecuteReceived;
        //Command1.ExecuteEvent += Command1_ExecuteEvent;
        //Command1.AsyncExecuteEvent += Command1_AsyncExecuteEvent;
        //ClickCommand.CanExecuteChanged += ClickCommand_CanExecuteChanged;
    }

    private void Command1_NotifyCanExecuteReceived(ref bool value)
    {
        value = Str1.Value == "114514";
    }

    private void Str2_NotifiedPropertyChange(ref string value)
    {
        value = Str1.Value + "1919810";
    }

    private void ClickCommand_CanExecuteChanged(object? sender, EventArgs e)
    {
        ShowCanExecute = CanExecute;
    }

    private async Task Command1_AsyncExecuteEvent()
    {
        await Task.Delay(1000);
        if (I18nCore.CurrentCulture.Name == CultureName.CN)
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.EN);
        else
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.CN);
        Command1.CanExecuteProperty.Value = false;
    }

    private void Command1_ExecuteEvent()
    {
        if (I18nCore.CurrentCulture.Name == CultureName.CN)
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.EN);
        else
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.CN);
    }

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private async Task Click()
    {
        await Task.Delay(1000);
        if (I18nCore.CurrentCulture.Name == CultureName.CN)
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.EN);
        else
            I18nCore.CurrentCulture = CultureInfo.GetCultureInfo(CultureName.CN);
        CanExecute = false;
    }
}

public partial class Test : ObservableObject
{
    [ObservableProperty]
    private string? _name;
}

public class TestI18nRes
{
    public static I18nRes I18nRes { get; } =
        new(MainWindowViewModel.I18nCore) { CanOverride = true };
    public static string Name => I18nRes.GetCultureData(nameof(Name));

    static TestI18nRes()
    {
        I18nRes.AddCulture(CultureName.CN);
        I18nRes.AddCulture(CultureName.EN);
        I18nRes.AddCultureData(CultureName.CN, nameof(Name), "姓名");
        I18nRes.AddCultureData(CultureName.EN, nameof(Name), nameof(Name));
    }
}

public static class CultureName
{
    public const string CN = "zh-CN";
    public const string EN = "en-US";
}
