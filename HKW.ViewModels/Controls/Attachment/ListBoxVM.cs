using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 列表模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count} ,  Attachment = {Attachment}")]
public partial class ListBoxVM<T> : SelectorVM<ListBoxItemVM<T>> { }
