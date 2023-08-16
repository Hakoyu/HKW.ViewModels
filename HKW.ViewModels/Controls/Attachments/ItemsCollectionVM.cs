using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 项目集合模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count},  Attachment = {Attachment}")]
public partial class ItemCollectionVM<T, TAttachment>
    : ItemCollectionVM<T>,
        IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
