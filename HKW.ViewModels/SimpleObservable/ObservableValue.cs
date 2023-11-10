using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.SimpleObservable;

/// <summary>
/// 可观察值
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("\\{ObservableValue, Value = {Value}\\}")]
public class ObservableValue<T>
    : INotifyPropertyChanging,
        INotifyPropertyChanged,
        IEquatable<ObservableValue<T>>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private T _value = default!;

    /// <summary>
    /// 当前值
    /// </summary>
    public T Value
    {
        get => _value;
        set
        {
            if (_value?.Equals(value) is true)
                return;
            var oldValue = _value;
            if (NotifyPropertyChanging(oldValue, value))
                return;
            _value = value;
            NotifyPropertyChanged(oldValue, value);
        }
    }

    /// <summary>
    /// 包含值
    /// </summary>
    public bool HasValue => Value != null;

    /// <summary>
    /// 唯一标识符
    /// </summary>
    public Guid Guid { get; } = Guid.NewGuid();

    /// <summary>
    ///
    /// </summary>
    public ObservableValueGroup<T>? Group { get; internal set; }

    #region Ctor
    /// <inheritdoc/>
    public ObservableValue() { }

    /// <inheritdoc/>
    /// <param name="value">初始值</param>
    public ObservableValue(T value)
    {
        _value = value;
    }
    #endregion

    #region NotifyProperty
    /// <summary>
    /// 通知属性改变前
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <returns>取消改变</returns>
    private bool NotifyPropertyChanging(T oldValue, T newValue)
    {
        PropertyChanging?.Invoke(this, new(nameof(Value)));
        var cancel = false;
        // 若全部事件取消改变 则取消改变
        ValueChanging?.Invoke(oldValue, newValue, ref cancel);
        return cancel;
    }

    /// <summary>
    /// 通知属性改变后
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    private void NotifyPropertyChanged(T oldValue, T newValue)
    {
        PropertyChanged?.Invoke(this, new(nameof(Value)));
        ValueChanged?.Invoke(oldValue, newValue);
        if (oldValue is null || newValue is null)
            PropertyChanged?.Invoke(this, new(nameof(HasValue)));
    }
    #endregion

    #region NotifySender
    /// <summary>
    /// 通知发送者
    /// </summary>
    public ICollection<ObservableValue<T>> NotifySenders => _notifySenders.Values;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Dictionary<Guid, ObservableValue<T>> _notifySenders = new();

    /// <summary>
    /// 添加通知发送者
    /// <para>
    /// 添加的发送者改变后会执行 <see cref="NotifyReceived"/>
    /// </para>
    /// <para>示例:
    /// <code><![CDATA[
    /// ObservableValue<string> value1 = new();
    /// ObservableValue<string> value2 = new();
    /// value2.AddNotifySender(value1);
    /// value2.NotifyReceived += (source, sender) =>
    /// {
    ///     source.Value = sender.Value;
    /// };
    /// value1.Value = "A";
    /// // value1.Value == "A", value2.Value == "A"
    /// ]]>
    /// </code></para>
    /// </summary>
    /// <param name="items">发送者</param>
    public void AddNotifySender(params ObservableValue<T>[] items)
    {
        foreach (var item in items)
        {
            item.PropertyChanged += Notify_PropertyChanged;
            _notifySenders.Add(item.Guid, item);
        }
    }

    /// <summary>
    /// 删除通知发送者
    /// </summary>
    /// <param name="items">发送者</param>
    public void RemoveNotifySender(params ObservableValue<T>[] items)
    {
        foreach (var item in items)
        {
            item.PropertyChanged -= Notify_PropertyChanged;
            _notifySenders.Remove(item.Guid);
        }
    }

    /// <summary>
    /// 清空通知发送者
    /// </summary>
    public void ClearNotifySender()
    {
        foreach (var sender in _notifySenders.Values)
            sender.PropertyChanged -= Notify_PropertyChanged;
        _notifySenders.Clear();
    }

    private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        NotifyReceived?.Invoke(this, (ObservableValue<T>)sender!);
    }
    #endregion

    #region Other
    /// <inheritdoc/>
    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as ObservableValue<T>);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }

    /// <inheritdoc/>
    public bool Equals(ObservableValue<T>? other)
    {
        return Guid.Equals(other?.Guid) is true;
    }

    /// <summary>
    /// 判断 <see cref="Value"/> 相等
    /// </summary>
    /// <param name="value1">左值</param>
    /// <param name="value2">右值</param>
    /// <returns>相等为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool operator ==(ObservableValue<T> value1, ObservableValue<T> value2)
    {
        return value1.Value?.Equals(value2.Value) is true;
    }

    /// <summary>
    /// 判断 <see cref="Value"/> 不相等
    /// </summary>
    /// <param name="value1">左值</param>
    /// <param name="value2">右值</param>
    /// <returns>不相等为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool operator !=(ObservableValue<T> value1, ObservableValue<T> value2)
    {
        return value1.Value?.Equals(value2.Value) is not true;
    }

    #endregion

    #region Event
    /// <summary>
    /// 属性改变前事件
    /// </summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>
    /// 属性改变后事件
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 值改变前事件
    /// </summary>
    public event ValueChangingEventHandler? ValueChanging;

    /// <summary>
    /// 值改变后事件
    /// </summary>
    public event ValueChangedEventHandler? ValueChanged;

    /// <summary>
    /// 通知接收事件
    /// </summary>
    public event NotifyReceivedHandler? NotifyReceived;
    #endregion

    #region Delegate
    /// <summary>
    /// 值改变事件
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <param name="cancel">取消</param>
    public delegate void ValueChangingEventHandler(T oldValue, T newValue, ref bool cancel);

    /// <summary>
    /// 值改变后事件
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    public delegate void ValueChangedEventHandler(T oldValue, T newValue);

    /// <summary>
    /// 通知接收器
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="sender">发送者</param>
    public delegate void NotifyReceivedHandler(
        ObservableValue<T> source,
        ObservableValue<T> sender
    );
    #endregion
}
