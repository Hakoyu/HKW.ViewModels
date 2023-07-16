using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 列表项视图模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class ListBoxItemVM : ContentControlVM
{
    [ObservableProperty]
    private string? _group;

    [ObservableProperty]
    private bool _isSelected = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public ListBoxItemVM() { }
}
