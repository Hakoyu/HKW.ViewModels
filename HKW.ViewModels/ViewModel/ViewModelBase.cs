using HKW.FastMember;
using HKW.HKWUtils;
using System.ComponentModel;

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

    /// <summary>
    /// 触发值改变的属性名
    /// <para>
    /// 在其中的属性名不会再触发 <see cref="ValueChanged"/> 事件
    /// </para>
    /// </summary>
    private readonly HashSet<string> _valueChangeProperties = new();

    /// <summary>
    /// 对象访问器
    /// </summary>
    private readonly ObjectAccessor _accessor;

    /// <inheritdoc/>
    public ViewModelBase()
    {
        _accessor = ObjectAccessor.Create(this);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);

        if (
            string.IsNullOrWhiteSpace(e.PropertyName) is false
            && ValueChanged is not null
            && _valueChangeProperties.Contains(e.PropertyName!) is false
        )
        {
            // 获取属性旧值
            _oldValue = _accessor[e.PropertyName];
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (
            string.IsNullOrWhiteSpace(e.PropertyName) is false
            && ValueChanged is not null
            && _valueChangeProperties.Contains(e.PropertyName!) is false
        )
        {
            // 记录当前属性
            _valueChangeProperties.Add(e.PropertyName!);
            _newValue = _accessor[e.PropertyName];
            // 获取属性新值
            var args = new ValueChangedEventArgs(e.PropertyName!, _oldValue, _newValue);
            // 触发事件
            ValueChanged?.Invoke((T)this, args);
            // 取消赋值
            if (args.Cancel)
            {
                // 取消后将旧值恢复
                _accessor[e.PropertyName] = _oldValue;
            }
            // 清空缓存值
            _oldValue = _newValue = null;
            // 删除记录值
            _valueChangeProperties.Remove(e.PropertyName!);
        }
    }

    /// <summary>
    /// 属性值改变后事件参数
    /// <para>
    /// 如果在此事件中改变触发事件的属性值, 则不会再次触发当前属性的 <see cref="ValueChanged"/> 事件 (其它属性仍会触发)
    /// </para>
    /// <para>
    /// 当前属性的 <see cref="ObservableObject.PropertyChanging"/> 和 <see cref="ObservableObject.PropertyChanged"/> 仍会触发
    /// </para>
    /// <para>示例:<code><![CDATA[
    /// var triggerCount = 0;
    /// var vm = new ViewModel();
    /// vm.ValueChanged += (s, e) =>
    /// {
    ///     // e.PropertyName == nameof(ViewModel.Value)
    ///     s.Value = newValue2; // 在事件中设置触发此事件的属性值时, 不会再次使当前属性触发这个事件 (其它属性仍会触发)
    ///     triggerCount++;
    /// };
    /// vm.Value = newValue1;
    /// // result: vm.Value == newValue2, triggerCount == 1
    /// ]]></code></para>
    /// </summary>
    public event ValueChangedEventHandler<T>? ValueChanged;
}
