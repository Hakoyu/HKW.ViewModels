using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 组合框视图模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}, Attachment = {Attachment}")]
public partial class ComboBoxVM<T> : SelectorVM<ComboBoxItemVM<T>>
{
    /// <inheritdoc/>
    /// <param name="itemsSource">项目集合</param>
    public ComboBoxVM(IEnumerable<ComboBoxItemVM<T>>? itemsSource = null)
        : base(itemsSource) { }
}
