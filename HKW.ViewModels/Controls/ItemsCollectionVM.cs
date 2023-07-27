using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 项目集合模型
/// </summary>
[DebuggerDisplay("{Name},Count = {ItemsSource.Count}")]
public partial class ItemsCollectionVM<T> : ControlVM
{
    /// <summary>
    /// 项目资源
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<T> _itemsSource = null!;
}
