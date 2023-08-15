﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可选中的控件模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class SelectableItemVM : ContentControlVM, ISelectableItemVM
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
            foreach (ISelectableItemVM item in Parent.ItemsSource)
                item.IsSelected = false;
            Parent.SelectedItem = this;
        }
        else
        {
            if (Parent.SelectedItem is not null)
                Parent.SelectedItem = null;
        }
    }

    private ISelectorVM? _parent;

    /// <inheritdoc cref="ISelectableItemVM.Parent"/>
    public ISelectorVM? Parent
    {
        get => _parent;
        set => _parent = _parent is not null ? throw new Exception("Cannot change parent") : value;
    }
}
