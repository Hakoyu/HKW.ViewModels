using HKW.HKWViewModels.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可被多选的可选中的控件模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class MultiSelectableItemVM : ContentControlVM, IMultiSelectableItemVM
{
    /// <inheritdoc cref="ISelectableItemVM.IsSelected"/>
    [ObservableProperty]
    private bool _isSelected = false;

    partial void OnIsSelectedChanging(bool value)
    {
        if (Parent is null)
            return;
        if (value is true)
        {
            Parent.SelectedItem = this;
        }
        else
        {
            if (Parent.SelectedItem == this)
                Parent.SelectedItem = null;
        }
    }

    private IMultiSelectorVM? _parent;

    /// <inheritdoc cref="IMultiSelectorVM.Parent"/>
    public IMultiSelectorVM? Parent
    {
        get => _parent;
        set => _parent = _parent is not null ? throw new Exception("Cannot change parent") : value;
    }
}
