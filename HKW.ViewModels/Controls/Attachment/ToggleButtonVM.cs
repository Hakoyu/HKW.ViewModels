﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 可切换按钮
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, IsChecked = {IsChecked}, Attachment = {Attachment}")]
public partial class ToggleButtonVM<T> : ButtonVM<T>
{
    /// <summary>
    /// 已选中
    /// </summary>
    [ObservableProperty]
    private bool _isChecked;
}