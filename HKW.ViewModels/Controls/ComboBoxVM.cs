using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 组合框视图模型
/// </summary>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}")]
public partial class ComboBoxVM : SelectorVM<ComboBoxItemVM>
{
    /// <inheritdoc/>
    /// <param name="itemsSource">项目</param>
    public ComboBoxVM(IEnumerable<ComboBoxItemVM>? itemsSource = null)
        : base(itemsSource) { }
}
