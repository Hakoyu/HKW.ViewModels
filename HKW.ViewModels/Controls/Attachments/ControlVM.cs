using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWUtils.Collections;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 基础控件模型
/// </summary>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Tag = {Tag}, Attachment = {Attachment}")]
public partial class ControlVM<TAttachment> : ControlVM, IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
