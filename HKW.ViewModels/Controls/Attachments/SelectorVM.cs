using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 选择器视图模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, SelectedIndex = {SelectedIndex}, Attachment = {Attachment}")]
public partial class SelectorVM<T, TAttachment> : ItemCollectionVM<T>, IAttachment<TAttachment>
    where T : ISelectableItemVM
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
