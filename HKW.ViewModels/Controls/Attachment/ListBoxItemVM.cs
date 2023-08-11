using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 列表项视图模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class ListBoxItemVM<T> : SelectableItemVM<T> { }
