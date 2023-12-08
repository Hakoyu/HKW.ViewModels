namespace HKW.HKWViewModels;

/// <summary>
/// 可观察本地化资源实例
/// </summary>
/// <typeparam name="TResource">I18n资源</typeparam>
public class ObservableI18nResource<TResource> : ObservableI18nResource
    where TResource : class
{
    /// <summary>
    /// 本地化资源
    /// </summary>
    public new TResource I18nRes => (TResource)base.I18nRes;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="i18nRes">本地化资源</param>
    public ObservableI18nResource(TResource i18nRes)
        : base(i18nRes.GetType().FullName!, i18nRes) { }
}
