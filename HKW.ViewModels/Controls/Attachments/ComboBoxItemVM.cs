using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 组合框项视图模型
/// </summary>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class ComboBoxItemVM<TAttachment> : ComboBoxItemVM, IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
