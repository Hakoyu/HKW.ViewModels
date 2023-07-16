using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 组合框项视图模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class ComboBoxItemVM : ContentControlVM
{
    [ObservableProperty]
    private bool _isSelected = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public ComboBoxItemVM() { }
}
