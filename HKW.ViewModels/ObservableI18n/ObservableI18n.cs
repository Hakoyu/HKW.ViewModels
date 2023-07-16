using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace HKW.HKWViewModels;

/// <summary>
/// 可观测本地化资源实例
/// <para>创建实例:<code><![CDATA[    public partial class MainWindowViewModel : ObservableObject
/// {
///     [ObservableProperty]
///     public ObservableI18n<MainWindowI18nRes> _i18n = ObservableI18n<MainWindowI18nRes>.Create(new());
/// }
/// ]]></code></para>
/// <para>在 xaml 中使用:<code><![CDATA[    <Window.DataContext>
///     <local:MainWindowViewModel />
/// </Window.DataContext>
/// <Grid>
///     <Label Content="{Binding I18nRes.I18nRes.Key}" />
///     <Label Content="{Binding LabelContent}" />
/// </Grid>
/// ]]></code></para>
/// <para>在代码中使用:<code><![CDATA[    [ObservableProperty]
/// private string labelContent;
/// public MainWindowViewModel()
/// {
///     I18nRes.AddPropertyChangedAction(() =>
///     {
///        LabelContent = MainWindowI18nRes.Test;
///     });
/// }
/// ]]></code></para>
/// </summary>
/// <typeparam name="TI18nRes">I18n资源</typeparam>
public class ObservableI18n<TI18nRes> : ObservableI18n
    where TI18nRes : notnull
{
    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResName => r_resName;

    /// <summary>
    /// 本地化资源
    /// </summary>
    public TI18nRes I18nRes => (TI18nRes)r_i18nRes;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="i18nRes">本地化资源</param>
    /// <param name="resName">资源名称</param>
    private ObservableI18n(TI18nRes i18nRes, string resName)
        : base(i18nRes, resName) { }

    /// <summary>
    /// 创造可观测本地化资源实例
    /// </summary>
    /// <param name="i18nRes">本地化资源</param>
    /// <returns>若检测到同名的实例,返回已有实例,否则新建实例</returns>
    public static ObservableI18n<TI18nRes> Create(TI18nRes i18nRes)
    {
        var resName = i18nRes.GetType().FullName!;
        return ObservableI18nAllRes.TryGetValue(resName, out var value)
            ? (ObservableI18n<TI18nRes>)value
            : new(i18nRes, resName);
    }

    /// <summary>
    /// 刷新I18n资源
    /// </summary>
    public void Refresh()
    {
        Refresh(ResName);
    }
}

/// <summary>
/// 可观测本地化
/// <para>用例:
/// <c>ObservableI18n.Language = "zh-CN"</c>
/// </para>
/// </summary>
public class ObservableI18n : INotifyPropertyChanged
{
    /// <summary>
    /// 本地化资源
    /// </summary>
    protected readonly object r_i18nRes;

    /// <summary>
    /// 本地化资源
    /// </summary>
    protected readonly string r_resName;

    /// <summary>
    /// 本地化资源实例集合
    /// </summary>
    protected static Dictionary<string, ObservableI18n> ObservableI18nAllRes { get; } = new();

    private static CultureInfo s_currentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// 当前文化
    /// </summary>
    public static CultureInfo CurrentCulture
    {
        get => s_currentCulture;
        set
        {
            if (s_currentCulture == value)
                return;
            s_currentCulture = value;
            CultureInfo.CurrentCulture = value;
            Thread.CurrentThread.CurrentCulture = value;
            Thread.CurrentThread.CurrentUICulture = value;
            foreach (var observableI18nRes in ObservableI18nAllRes.Values)
                observableI18nRes.PropertyChanged?.Invoke(observableI18nRes, new(null));
            CultureChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="i18nRes">本地化资源</param>
    /// <param name="resName">资源名称</param>
    protected ObservableI18n(object i18nRes, string resName)
    {
        r_i18nRes = i18nRes;
        r_resName = resName;
        ObservableI18nAllRes.TryAdd(resName, this);
    }

    /// <summary>
    /// 添加文化改变委托
    /// </summary>
    /// <param name="cultureChangedAction">文化改变委托</param>
    public void AddCultureChangedAction(Action<CultureInfo> cultureChangedAction)
    {
        PropertyChanged += (s, e) =>
        {
            cultureChangedAction(CultureInfo.CurrentCulture);
        };
    }

    /// <summary>
    /// 刷新I18n资源
    /// </summary>
    /// <param name="resName">资源名称</param>
    protected static void Refresh(string resName)
    {
        var observableI18n = ObservableI18nAllRes[resName];
        observableI18n.PropertyChanged?.Invoke(observableI18n, new(null));
    }

    /// <summary>
    /// 绑定两个值, 在触发 <see cref="CultureChanged"/> 时会对目标重新赋值
    /// <para>示例:
    /// <code>target = source</code>
    /// 等同于:
    /// <code><![CDATA[
    /// ObservableI18n.BindingValue((value) => target = value, () => source);
    /// ]]></code>
    /// </para>
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="target">目标</param>
    /// <param name="source">源</param>
    /// <returns>源的返回值</returns>
    public static T BindingValue<T>(Action<T> target, Func<T> source)
    {
        CultureChanged += (culture) => target(source());
        return source();
    }

    /// <summary>
    /// 属性改变委托
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 文化改变委托
    /// </summary>
    public static event CultureChangedEventHander? CultureChanged;
}
