using HKW.HKWUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

//public abstract class ObservableObject1
//{
//    protected bool SetProperty<T>(
//        [NotNullIfNotNull(nameof(newValue))] ref T field,
//        T newValue,
//        [CallerMemberName] string? propertyName = null
//    )
//    {
//        if (EqualityComparer<T>.Default.Equals(field, newValue))
//        {
//            return false;
//        }
//        var oldValue = field;
//        //OnPropertyChanging(propertyName);
//        if (AnotherPropertyChanging is not null)
//            AnotherPropertyChanging?.Invoke(this, new(propertyName, oldValue, newValue));
//        field = newValue;

//        //OnPropertyChanged(propertyName);
//        if (AnotherPropertyChanged is not null)
//            AnotherPropertyChanged?.Invoke(this, new(propertyName, oldValue, newValue));
//        return true;
//    }

//    public event AnotherPropertyChangingEventHandler? AnotherPropertyChanging;
//    public event AnotherPropertyChangedEventHandler? AnotherPropertyChanged;
//}

//public delegate void AnotherPropertyChangingEventHandler(
//    object sender,
//    AnotherPropertyChangingEventArgs e
//);
//public delegate void AnotherPropertyChangedEventHandler(
//    object sender,
//    AnotherPropertyChangedEventArgs e
//);

//public class AnotherPropertyChangingEventArgs : PropertyChangingEventArgs
//{
//    public object? OldValue { get; }

//    public object? NewValue { get; }

//    public AnotherPropertyChangingEventArgs(
//        string? propertyName,
//        object? oldValue,
//        object? newValue
//    )
//        : base(propertyName)
//    {
//        OldValue = oldValue;
//        NewValue = newValue;
//    }
//}

//public class AnotherPropertyChangedEventArgs : PropertyChangedEventArgs
//{
//    public object? OldValue { get; }

//    public object? NewValue { get; }

//    public AnotherPropertyChangedEventArgs(string? propertyName, object? oldValue, object? newValue)
//        : base(propertyName)
//    {
//        OldValue = oldValue;
//        NewValue = newValue;
//    }
//}

//public partial class MainWindowViewModel : ObservableObject
//{
//    AnotherViewModel anotherViewModel = new();

//    [ObservableProperty]
//    private int _oldValue = 0;

//    [ObservableProperty]
//    private int _newValue = 0;

//    public MainWindowViewModel()
//    {
//        anotherViewModelAnotherPropertyChanged += (s, e) =>
//        {
//            if (e.PropertyName == nameof(MainWindowViewModel.Value))
//            {
//                OldValue = e.OldValue;
//                NewValue = e.NewValue;
//            }
//        };
//    }
//}

//public partial class AnotherViewModel : ObservableObject
//{
//    [ObservableProperty]
//    private int _value = 0;
//}
