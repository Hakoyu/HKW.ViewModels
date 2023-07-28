﻿using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可包含任意类型内容的控件模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class ContentControlVM : ControlVM, IContentControlVM
{
    /// <inheritdoc cref="IContentControlVM.Icon"/>
    [ObservableProperty]
    private object? _icon;

    /// <inheritdoc cref="IContentControlVM.Content"/>
    [ObservableProperty]
    private object? _content;
}
