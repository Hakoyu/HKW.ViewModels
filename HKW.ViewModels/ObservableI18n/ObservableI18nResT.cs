namespace HKW.HKWViewModels;

/// <summary>
/// 可观察本地化资源实例
/// </summary>
/// <typeparam name="TI18nRes">I18n资源</typeparam>
public class ObservableI18nRes<TI18nRes> : ObservableI18nRes
    where TI18nRes : class
{
    /// <summary>
    /// 本地化资源
    /// </summary>
    public new TI18nRes I18nRes => (TI18nRes)base.I18nRes;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="i18nRes">本地化资源</param>
    public ObservableI18nRes(TI18nRes i18nRes)
        : base(i18nRes.GetType().FullName!, i18nRes) { }
}
