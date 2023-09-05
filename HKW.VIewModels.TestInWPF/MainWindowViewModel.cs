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

    public MainWindowViewModel() { }

    [RelayCommand]
    private void Click()
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
