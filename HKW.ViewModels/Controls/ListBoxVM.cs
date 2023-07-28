using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 列表模型
/// </summary>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}")]
public partial class ListBoxVM : SelectorVM<ListBoxItemVM>
{
    /// <inheritdoc/>
    /// <param name="itemsSource">项目</param>
    public ListBoxVM(IEnumerable<ListBoxItemVM>? itemsSource = null)
        : base(itemsSource) { }
}
