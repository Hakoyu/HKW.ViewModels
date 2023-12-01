﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels;
using HKW.HKWViewModels;

namespace HKW.VIewModels.TestOnWPF;

public partial class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
{
    public static ObservableI18nCore I18nCore { get; } = new() { };

    [ObservableProperty]
    private ObservableI18nRes<TestI18nRes> _i18n = I18nCore.Create<TestI18nRes>(new());

    [ObservableProperty]
    private bool _showCanExecute = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private bool _canExecute = true;

    [ObservableProperty]
    private int _count = 0;

    [ObservableProperty]
    private object? _test = null;

    public MainWindowViewModel()
    {
        PropertyChanged += (s, e) =>
        {
            return;
        };
        ValueChanged += (s, e) =>
        {
            var (o, n) = e.GetValue<string>();
            return;
        };
        Test = "114514";
        Test = null;
        //Command1.AddNotifyReceiver(Str1);
        //Command1.NotifyCanExecuteReceived += Command1_NotifyCanExecuteReceived;
        //Command1.ExecuteEvent += Command1_ExecuteEvent;
        //Command1.AsyncExecuteEvent += Command1_AsyncExecuteEvent;
        //ClickCommand.CanExecuteChanged += ClickCommand_CanExecuteChanged;
        //var value2 = new ObservableValue<string>();
        //var group = new ObservableValueGroup<string>() { value1, value2 };
        //value1.Value = "A";
        //value2.Value = "B";
        //group.Remove(value1);
        //value1.Value = "C";
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
