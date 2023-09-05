using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace HKW.HKWViewModels;

/// <summary>
/// 可观察本地化
/// <para>用例:
/// <c>I18nCore.CurrentCulture = CultureInfo.GetCultureInfo("zh-CN")</c>
/// </para>
/// </summary>
public class ObservableI18nCore
{
    /// <summary>
    /// 改变 <see cref="CurrentCulture"/> 同时修改 <see cref="Thread.CurrentThread"/> 的文化
    /// </summary>
    [DefaultValue(false)]
    public bool ChangeThreadCulture { get; set; } = false;

    /// <summary>
    /// 本地化资源实例集合
    /// </summary>
    protected Dictionary<string, ObservableI18nRes> AllObservableI18nRes { get; } = new();

    private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// 当前文化
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (_currentCulture == value)
                return;
            _currentCulture = value;
            if (ChangeThreadCulture)
            {
                CultureInfo.CurrentCulture = value;
                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
            }
            CultureChanged?.Invoke(value);
            RefreshAll();
        }
    }

    /// <summary>
    /// 创建新的I18nRes
    /// <para>示例:
    /// <code><![CDATA[
    /// public partial class MainWindowViewModel : ObservableObject
    /// {
    ///     public static ObservableI18nCore I18nCore { get; } = new();
    ///     [ObservableProperty]
    ///     public ObservableI18nRes<TestI18nRes> _i18n = I18nCore.Create<TestI18nRes>(new());
    /// }
    /// ]]>
    /// </code></para>
    /// </summary>
    /// <typeparam name="TI18nRes">资源类型</typeparam>
    /// <param name="i18nRes">I18n资源</param>
    /// <returns>可观察的I18n资源</returns>
    public ObservableI18nRes<TI18nRes> Create<TI18nRes>(TI18nRes i18nRes)
        where TI18nRes : class
    {
        var name = i18nRes.GetType().FullName!;
        if (AllObservableI18nRes.TryGetValue(name, out var value))
            return (ObservableI18nRes<TI18nRes>)value;
        var res = new ObservableI18nRes<TI18nRes>(i18nRes);
        AllObservableI18nRes.Add(name, res);
        Refresh(name);
        return res;
    }

    /// <summary>
    /// 刷新指定I18n资源
    /// </summary>
    /// <param name="resName">资源名称</param>
    public void Refresh(string resName)
    {
        CultureChanged?.Invoke(CurrentCulture);
        var observableI18n = AllObservableI18nRes[resName];
        observableI18n.Refresh();
    }

    /// <summary>
    /// 刷新所有I18n资源
    /// </summary>
    public void RefreshAll()
    {
        CultureChanged?.Invoke(CurrentCulture);
        foreach (var observableI18nRes in AllObservableI18nRes.Values)
            observableI18nRes.Refresh();
    }

    #region BindingValue
    /// <summary>
    /// 绑定两个值, 在触发 <see cref="CultureChanged"/> 时会对目标重新赋值
    /// <para>示例:
    /// <code>target = source</code>
    /// 等同于:
    /// <code><![CDATA[
    /// target = ObservableI18nCore.BindingValue((value) => target = value, () => source);
    /// ]]></code>
    /// </para>
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="setTargetValue">设置目标值</param>
    /// <param name="getSourceValue">获取源值</param>
    /// <returns>源的返回值</returns>
    public T BindingValue<T>(Action<T> setTargetValue, Func<T> getSourceValue)
    {
        CultureChanged += (culture) => setTargetValue(getSourceValue());
        return getSourceValue();
    }

    /// <summary>
    /// 绑定两个值, 在触发 <see cref="CultureChanged"/> 时会对目标重新赋值
    /// <para>示例:
    /// <code>target.Value = source</code>
    /// 等同于:
    /// <code><![CDATA[
    /// target.Value = ObservableI18nCore.BindingValue(target, (value, target) => target.Value = value, () => source);
    /// ]]></code>
    /// </para>
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    /// <param name="target">目标</param>
    /// <param name="setTargetValue">设置目标值</param>
    /// <param name="getSourceValue">获取源值</param>
    /// <returns>源的返回值</returns>
    public T BindingValue<T, TTarget>(
        TTarget target,
        Action<T, TTarget> setTargetValue,
        Func<T> getSourceValue
    )
        where TTarget : class
    {
        CultureChanged += (culture) => setTargetValue(getSourceValue(), target);
        return getSourceValue();
    }

    /// <summary>
    /// 使用弱引用绑定目标, 在触发 <see cref="CultureChanged"/> 时会对目标重新赋值
    /// <para>示例:
    /// <code>target.Value = source</code>
    /// 等同于:
    /// <code><![CDATA[
    /// target.Value = ObservableI18nCore.BindingValueOnWeakReference(target, (value, target) => target.Value = value, () => source);
    /// ]]></code>
    /// </para>
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    /// <param name="target">目标</param>
    /// <param name="setTargetValue">设置目标值</param>
    /// <param name="getSourceValue">获取源值</param>
    /// <returns>源的返回值</returns>
    public T BindingValueOnWeakReference<T, TTarget>(
        TTarget target,
        Action<T, TTarget> setTargetValue,
        Func<T> getSourceValue
    )
        where TTarget : class
    {
        var weakReference = new WeakReference<TTarget>(target);
        CultureChanged += CultureChangedEvent;
        return getSourceValue();

        void CultureChangedEvent(CultureInfo culture)
        {
            if (weakReference.TryGetTarget(out var target))
                setTargetValue(getSourceValue(), target);
            else
                CultureChanged -= CultureChangedEvent;
        }
    }
    #endregion

    /// <summary>
    /// 文化改变委托
    /// </summary>
    public event CultureChangedEventHander? CultureChanged;
}
