using HKW.HKWViewModels.Controls.Attachments;
using HKW.HKWViewModels.Controls.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 多选选择器视图模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, SelectedCount = {SelectedItems.Count}, Attachment = {Attachment}")]
public partial class MultiSelectorVM<T, TAttachment> : MultiSelectorVM<T>, IAttachment<TAttachment>
    where T : IMultiSelectableItemVM
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
