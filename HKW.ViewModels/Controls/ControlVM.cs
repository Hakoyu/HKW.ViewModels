using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWUtils.Collections;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 基础控件模型
/// </summary>
[DebuggerDisplay("Name = {Name}")]
public partial class ControlVM : ObservableObject, IControlVM
{
    /// <inheritdoc cref="IControlVM.Id"/>
    [ObservableProperty]
    private string? _id;

    /// <inheritdoc cref="IControlVM.Name"/>
    [ObservableProperty]
    private string? _name;

    /// <inheritdoc cref="IControlVM.Tag"/>
    [ObservableProperty]
    private object? _tag;

    /// <inheritdoc cref="IControlVM.IsVisible"/>
    [ObservableProperty]
    private bool? _isVisible = true;

    /// <inheritdoc cref="IControlVM.ToolTip"/>
    [ObservableProperty]
    private object? _toolTip;

    /// <inheritdoc cref="IControlVM.ContextMenu"/>
    [ObservableProperty]
    private ContextMenuVM? _contextMenu;
}
