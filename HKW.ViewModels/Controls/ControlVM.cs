using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWUtils.Collections;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 基础控件模型
/// </summary>
[DebuggerDisplay("{Name}, Tag = {Tag}")]
public partial class ControlVM : ObservableObject
{
    /// <summary>
    /// Id
    /// </summary>
    [ObservableProperty]
    private string? _id;

    /// <summary>
    /// 名称
    /// </summary>
    [ObservableProperty]
    private string? _name;

    /// <summary>
    /// 标签
    /// </summary>
    [ObservableProperty]
    private object? _tag;

    /// <summary>
    /// 是否可见
    /// <para>
    /// <see langword="true"/> 为可见 <see langword="false"/> 为不可见 <see langword="null"/> 为不可见并不占用空间
    /// </para>
    /// </summary>
    [ObservableProperty]
    private bool? _isVisible = true;

    /// <summary>
    /// 提示
    /// </summary>
    [ObservableProperty]
    private object? _toolTip;

    /// <summary>
    /// 上下文菜单
    /// </summary>
    [ObservableProperty]
    private ContextMenuVM? _contextMenu;
}
