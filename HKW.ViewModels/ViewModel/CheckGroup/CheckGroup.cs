using HKW.HKWUtils.Observable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels;

/// <summary>
/// 检查组
/// </summary>
public partial class CheckGroup : ViewModelBase<CheckGroup>
{
    /// <summary>
    /// 队长已检查
    /// <para>
    /// 修改此值会改变 <see cref="CheckInfos"/> 中检查信息的值
    /// </para>
    /// </summary>
    [ObservableProperty]
    private bool? _leaderIsChecked = false;

    /// <summary>
    /// 检查信息
    /// </summary>
    public ObservableList<ICheckInfo> CheckInfos { get; set; } = new();

    /// <summary>
    /// 改变中
    /// </summary>
    private bool _changing = false;

    /// <inheritdoc/>
    public CheckGroup()
    {
        CheckInfos.ListChanged += CheckInfos_ListChanged;
    }

    /// <inheritdoc/>
    /// <param name="leaderIsChecked">队长已检查</param>
    public CheckGroup(bool? leaderIsChecked)
        : this()
    {
        _leaderIsChecked = leaderIsChecked;
    }

    partial void OnLeaderIsCheckedChanged(bool? oldValue, bool? newValue)
    {
        if (_changing || newValue is null)
            return;
        _changing = true;
        if (newValue is true)
        {
            var count = 0;
            foreach (var check in CheckInfos)
            {
                if (check is ICheckInfo canCheck && canCheck.CanCheck is false)
                    continue;
                check.IsChecked = true;
                count++;
            }
            if (count != CheckInfos.Count)
            {
                LeaderIsChecked = null;
                CheckAllFalse?.Invoke(this, new());
            }
        }
        else
        {
            foreach (var check in CheckInfos)
                check.IsChecked = false;
        }
        _changing = false;
    }

    private void CheckInfos_ListChanged(
        IObservableList<ICheckInfo> sender,
        NotifyListChangedEventArgs<ICheckInfo> e
    )
    {
        var newItem = e.NewItems?.FirstOrDefault();
        var oldItem = e.OldItems?.FirstOrDefault();
        if (e.Action is ListChangeAction.Add)
        {
            newItem!.PropertyChanged += ChechInfo_PropertyChanged;
        }
        else if (e.Action is ListChangeAction.Remove)
        {
            oldItem!.PropertyChanged += ChechInfo_PropertyChanged;
        }
        else if (e.Action is ListChangeAction.Replace)
        {
            oldItem!.PropertyChanged += ChechInfo_PropertyChanged;
            newItem!.PropertyChanged += ChechInfo_PropertyChanged;
        }
    }

    private void ChechInfo_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_changing)
            return;
        if (sender is not ICheckInfo info)
            return;
        _changing = true;
        var count = CheckInfos.Count(x => x.IsChecked);
        if (count == CheckInfos.Count)
            LeaderIsChecked = true;
        else if (count == 0)
            LeaderIsChecked = false;
        else
            LeaderIsChecked = null;
        _changing = false;
    }

    /// <summary>
    /// 检查所有时失败事件
    /// <para>
    /// 当 <see cref="LeaderIsChecked"/> 赋值为 <see langword="true"/> 但 <see cref="CheckInfos"/> 中有子项 <see cref="ICheckInfo.CanCheck"/> 为 <see langword="false"/> 时, 触发此事件
    /// </para>
    /// <para>
    /// 此时 <see cref="CheckInfos"/> 中 <see cref="ICheckInfo.CanCheck"/> 为 <see langword="true"/> 的子项的 <see cref="ICheckInfo.IsChecked"/> 会被设为 <see langword="true"/>
    /// </para>
    /// <para>
    /// 同时 <see cref="LeaderIsChecked"/> 将自动修改为 <see langword="null"/> (第三状态)
    /// </para>
    /// </summary>
    public event CheckAllFalseEventHandler? CheckAllFalse;
}

/// <summary>
/// 检查所有时失败事件
/// </summary>
/// <param name="sender">发送者</param>
/// <param name="e">参数</param>
public delegate void CheckAllFalseEventHandler(CheckGroup sender, EventArgs e);
