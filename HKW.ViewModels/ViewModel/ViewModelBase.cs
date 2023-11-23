using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels;

/// <summary>
/// 基础视图模型
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
    /// <summary>
    /// 启用后会在触发 <see cref="ObservableObject.PropertyChanged"/> 时触发 <see cref="ValueChanged"/>
    /// </summary>
    [DefaultValue(false)]
    public bool EnableValueChangeEvents { get; set; } = false;

    /// <summary>
    /// 类型
    /// </summary>
    private readonly Type _type;

    /// <summary>
    /// 旧值
    /// </summary>
    public object? _oldValue = null;

    /// <summary>
    /// 新值
    /// </summary>
    public object? _newValue = null;

    /// <inheritdoc/>
    public ViewModelBase()
    {
        _type = GetType();
    }

    /// <inheritdoc/>
    /// <param name="enableValueChangeEvents">启用值改变事件</param>
    public ViewModelBase(bool enableValueChangeEvents)
        : this()
    {
        EnableValueChangeEvents = enableValueChangeEvents;
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);
        if (EnableValueChangeEvents && ValueChanged is not null)
            _oldValue = GetPropertyValue(e.PropertyName);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (EnableValueChangeEvents && ValueChanged is not null)
        {
            _newValue = GetPropertyValue(e.PropertyName);
            ValueChanged?.Invoke(this, new(e.PropertyName, _oldValue, _newValue));
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
        return _type.GetProperty(propertyName)?.GetValue(this);
    }

    /// <summary>
    /// 属性值改变后事件参数
    /// </summary>
    public event ValueChangedEventHandler? ValueChanged;
}

/// <summary>
/// 属性值改变后事件参数
/// </summary>
public delegate void ValueChangedEventHandler(ObservableObject sender, ValueChangedEventArgs e);

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
}


///// <summary>
///// 属性值改变前事件
///// </summary>
//public delegate void ValueChangingEventHandler(ObservableObject sender, ValueChangingEventArgs e);

///// <summary>
///// 属性值改变前事件参数
///// </summary>
//public class ValueChangingEventArgs : PropertyChangingEventArgs
//{
//    /// <summary>
//    /// 旧值
//    /// </summary>
//    public object? OldValue { get; } = null;

//    /// <summary>
//    /// 新值
//    /// </summary>
//    public object? NewValue { get; } = null;

//    /// <inheritdoc/>
//    /// <param name="propertyName">属性名</param>
//    /// <param name="oldValue">旧值</param>
//    /// <param name="newValue">新值</param>
//    public ValueChangingEventArgs(string? propertyName, object? oldValue, object? newValue)
//        : base(propertyName)
//    {
//        OldValue = oldValue;
//        NewValue = newValue;
//    }
//}
