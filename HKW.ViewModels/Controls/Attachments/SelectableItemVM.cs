using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 可选中的控件模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class SelectableItemVM<TAttachment> : SelectableItemVM, IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
