using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 列表模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count} ,  Attachment = {Attachment}")]
public partial class ListBoxVM<T> : MultiSelectorVM<ListBoxItemVM<T>>
{
    /// <inheritdoc/>
    /// <param name="itemsSource">项目</param>
    public ListBoxVM(IEnumerable<ListBoxItemVM<T>>? itemsSource = null)
        : base(itemsSource) { }
}
