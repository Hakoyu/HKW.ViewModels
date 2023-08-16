using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 上下文菜单模型
/// </summary>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}")]
public partial class ContextMenuVM<TAttachment> : ItemCollectionVM<MenuItemVM<TAttachment>> { }
