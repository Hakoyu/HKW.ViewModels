using HKW.HKWViewModels;

namespace HKW.VIewModels.TestOnWPF;

public class TestI18nRes
{
    private static I18nRes I18nRes { get; } =
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
