using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 多选选择器视图模型接口
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
public interface IMultiSelectorVM<T> : IItemCollectionVM<T>
    where T : IMultiSelectableItemVM
{
    /// <summary>
    /// 选中项的索引
    /// </summary>
    public int SelectedIndex { get; set; }

    /// <summary>
    /// 选中项
    /// </summary>
    public T? SelectedItem { get; set; }

    /// <summary>
    /// 选中项
    /// </summary>
    public ObservableCollection<T> SelectedItems { get; }

    /// <summary>
    /// 选中项改变命令
    /// </summary>
    public IRelayCommand<T> SelectionChangedCommand { get; }

    /// <summary>
    /// 选中项改变委托
    /// </summary>
    /// <param name="item">参数</param>
    public delegate void SelectionChangedHandler(T item);

    /// <summary>
    /// 选中项改变事件
    /// </summary>
    public event SelectionChangedHandler? SelectionChangedEvent;
}

/// <summary>
/// 多选选择器视图模型接口
/// </summary>
public interface IMultiSelectorVM : ISelectorVM
{
    /// <summary>
    /// 选中项
    /// </summary>
    public IList? SelectedItems { get; }
}
