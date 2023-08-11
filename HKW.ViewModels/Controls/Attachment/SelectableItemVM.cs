using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 可选中的控件模型
/// </summary>
public partial class SelectableItemVM<T> : ContentControlVM<T>, ISelectableItemVM
{
    /// <inheritdoc cref="ISelectableItemVM.IsSelected"/>
    [ObservableProperty]
    private bool _isSelected = false;
}
