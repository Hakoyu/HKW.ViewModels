using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可切换按钮
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}, IsChecked = {IsChecked}")]
public partial class ToggleButtonVM : ButtonVM
{
    /// <summary>
    /// 已选中
    /// </summary>
    [ObservableProperty]
    private bool _isChecked;
}
