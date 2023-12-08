using HKW.HKWUtils;
using System.ComponentModel;
using System.Diagnostics;

namespace HKW.HKWViewModels;

/// <summary>
/// 基础视图模型
/// </summary>
public abstract class ViewModelBase<T> : ObservableObject
    where T : ViewModelBase<T>
{
    /// <summary>
    /// 旧值
    /// </summary>
    private object? _oldValue = null;

    /// <summary>
    /// 新值
    /// </summary>
    private object? _newValue = null;

    /// <inheritdoc/>
    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);
        if (ValueChanged is not null)
        {
            _oldValue = GetPropertyValue(e.PropertyName);
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (ValueChanged is not null)
        {
            _newValue = GetPropertyValue(e.PropertyName);
            ValueChanged?.Invoke((T)this, new(e.PropertyName, _oldValue, _newValue));
            _oldValue = _newValue = null;
        }
    }

    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <returns>属性值</returns>
    protected virtual object? GetPropertyValue(string? propertyName)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            return null;
        return PropertyCallAdapterProvider<T>.GetInstance(propertyName)?.InvokeGet((T)this);
        //return typeof(T).GetProperty(propertyName)?.GetValue(this);
    }

    /// <summary>
    /// 属性值改变后事件参数
    /// </summary>
    public event ValueChangedEventHandler? ValueChanged;

    /// <summary>
    /// 属性值改变后事件参数
    /// </summary>
    public delegate void ValueChangedEventHandler(T sender, ValueChangedEventArgs e);
}

/// <summary>
/// 属性值改变后事件参数
/// </summary>
[DebuggerDisplay("PropertyName = {PropertyName}")]
public class ValueChangedEventArgs : PropertyChangedEventArgs
{
    /// <summary>
    /// 旧值
    /// </summary>
    public object? OldValue { get; }

    /// <summary>
    /// 新值
    /// </summary>
    public object? NewValue { get; }

    /// <inheritdoc/>
    /// <param name="propertyName">属性名</param>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    public ValueChangedEventArgs(string? propertyName, object? oldValue, object? newValue)
        : base(propertyName)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <returns>(OldValue, NewValue)</returns>
    public (T, T) GetValue<T>()
    {
        return ((T)OldValue!, (T)NewValue!);
    }
}
