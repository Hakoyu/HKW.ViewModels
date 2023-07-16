using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 组合框视图模型
/// </summary>
[DebuggerDisplay("{Name},Count = {ItemsSource.Count}")]
public partial class ComboBoxVM : SelectorVM<ComboBoxItemVM>
{
    /// <summary>
    /// 构造
    /// </summary>
    public ComboBoxVM()
    {
        ItemsSource ??= new();
    }
}
