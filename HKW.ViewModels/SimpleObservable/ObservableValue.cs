using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.SimpleObservable;

/// <summary>
/// 可观察值
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObservableValue<T> : INotifyPropertyChanging, INotifyPropertyChanged
    where T : notnull
{
    private T? _value = default;

    /// <summary>
    /// 当前值
    /// </summary>
    public T? Value
    {
        get => _value;
        set
        {
            if (_value?.Equals(value) is true)
                return;
            PropertyChanging?.Invoke(this, new(nameof(Value)));
            _value = value;
            PropertyChanged?.Invoke(this, new(nameof(Value)));
        }
    }

    /// <inheritdoc/>
    public ObservableValue() { }

    /// <inheritdoc/>
    /// <param name="value">值</param>
    public ObservableValue(T value)
        : this(value, null!, null!) { }

    /// <inheritdoc/>
    /// <param name="value">值</param>
    /// <param name="propertyChanging">属性改变时事件</param>
    public ObservableValue(T value, PropertyChangingEventHandler propertyChanging)
        : this(value, propertyChanging, null!) { }

    /// <inheritdoc/>
    /// <param name="value">值</param>
    /// <param name="propertyChanged">属性改变后事件</param>
    public ObservableValue(T value, PropertyChangedEventHandler propertyChanged)
        : this(value, null!, propertyChanged) { }

    /// <inheritdoc/>
    /// <param name="value">值</param>
    /// <param name="propertyChanging">属性改变时事件</param>
    /// <param name="propertyChanged">属性改变后事件</param>
    public ObservableValue(
        T value,
        PropertyChangingEventHandler propertyChanging,
        PropertyChangedEventHandler propertyChanged
    )
    {
        _value = value;
        PropertyChanging += propertyChanging;
        PropertyChanged += propertyChanged;
    }

    /// <summary>
    /// 属性改变时事件
    /// </summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>
    /// 属性改变后事件
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
