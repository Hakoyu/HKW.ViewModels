using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWUtils.Collections;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 基础控件模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Tag = {Tag}, Attachment = {Attachment}")]
public partial class ControlVM<T> : ControlVM, IAttachment<T>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private T? _attachment;
}
