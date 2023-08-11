using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 组合框项视图模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}, IsSelected = {IsSelected}")]
public partial class ComboBoxItemVM : SelectableItemVM { }
