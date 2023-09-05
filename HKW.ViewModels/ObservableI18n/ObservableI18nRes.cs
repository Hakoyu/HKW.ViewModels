using System.ComponentModel;

namespace HKW.HKWViewModels;

/// <summary>
/// 可观察的I18n资源
/// </summary>
public class ObservableI18nRes : INotifyPropertyChanged
{
    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResName { get; private set; }

    /// <summary>
    /// 资源
    /// </summary>
    public object I18nRes { get; private set; }

    /// <inheritdoc/>
    /// <param name="resName">资源名称</param>
    /// <param name="i18nRes">资源</param>
    protected ObservableI18nRes(string resName, object i18nRes)
    {
        ResName = resName;
        I18nRes = i18nRes;
    }

    /// <summary>
    /// 刷新资源
    /// </summary>
    public void Refresh()
    {
        PropertyChanged?.Invoke(this, new(null));
    }

    /// <summary>
    /// 属性改变委托
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
