using System.ComponentModel;
using System.Diagnostics;

namespace HKW.HKWViewModels;

/// <summary>
/// 属性值改变后事件参数
/// </summary>
[DebuggerDisplay("PropertyName = {PropertyName}")]
public class PropertyValueChangedEventArgs : CancelEventArgs
{
    /// <summary>
    /// 属性名
    /// </summary>
    public string PropertyName { get; }

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
    public PropertyValueChangedEventArgs(string propertyName, object? oldValue, object? newValue)
    {
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <returns>(OldValue, NewValue)</returns>
    public (T oldVale, T newVlaue) GetValue<T>()
    {
        return ((T)OldValue!, (T)NewValue!);
    }
}
