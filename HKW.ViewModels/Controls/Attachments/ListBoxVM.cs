using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 列表模型
/// </summary>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}")]
public partial class ListBoxVM<TAttachment> : MultiSelectorVM<ListBoxItemVM<TAttachment>>
{
    /// <inheritdoc/>
    /// <param name="itemsSource">项目</param>
    public ListBoxVM(IEnumerable<ListBoxItemVM<TAttachment>>? itemsSource = null)
        : base(itemsSource) { }
}
