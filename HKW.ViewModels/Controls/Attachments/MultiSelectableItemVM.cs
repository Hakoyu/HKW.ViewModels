using HKW.HKWViewModels.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 可被多选的可选中的控件模型
/// </summary>
/// <typeparam name="TAttachment">附加类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class MultiSelectableItemVM<TAttachment>
    : MultiSelectableItemVM,
        IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
