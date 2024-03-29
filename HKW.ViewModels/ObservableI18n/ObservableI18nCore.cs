﻿using System.ComponentModel;
using System.Globalization;

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
    /// <para>(ResourceName, ObservableI18nResource)</para>
    /// </summary>
    protected Dictionary<string, ObservableI18nResource> ObservableI18nResources { get; } = new();

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
    ///     public ObservableI18nResource<TestI18nResource> _i18n = I18nCore.Create<TestI18nResource>(new());
    /// }
    /// ]]>
    /// </code></para>
    /// </summary>
    /// <typeparam name="TResource">资源类型</typeparam>
    /// <param name="resource">I18n资源</param>
    /// <returns>可观察的I18n资源</returns>
    public ObservableI18nResource<TResource> Create<TResource>(TResource resource)
        where TResource : class
    {
        var name = resource.GetType().FullName!;
        if (ObservableI18nResources.TryGetValue(name, out var value))
            return (ObservableI18nResource<TResource>)value;
        var res = new ObservableI18nResource<TResource>(resource);
        ObservableI18nResources.Add(name, res);
        Refresh(name);
        return res;
    }

    /// <summary>
    /// 刷新指定I18n资源
    /// </summary>
    /// <param name="resName">资源名称</param>
    public void Refresh(string resName)
    {
        CultureChanged?.Invoke(this, new(CurrentCulture));
        var observableI18n = ObservableI18nResources[resName];
        observableI18n.Refresh();
    }

    /// <summary>
    /// 刷新所有I18n资源
    /// </summary>
    public void RefreshAll()
    {
        CultureChanged?.Invoke(this, new(CurrentCulture));
        foreach (var observableI18nRes in ObservableI18nResources.Values)
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
        CultureChanged += (s, e) => setTargetValue(getSourceValue());
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
        CultureChanged += (s, e) => setTargetValue(getSourceValue(), target);
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

        void CultureChangedEvent(object? sender, CultureChangedEventArgs e)
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
