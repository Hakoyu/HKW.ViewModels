using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 列表项视图模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}, IsSelected = {IsSelected}")]
public partial class ListBoxItemVM : SelectableControlVM { }
