using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 基础控件模型
/// </summary>
[DebuggerDisplay("{Name}, Count = {TagDictionary.Count}")]
public partial class ControlVMBase : ObservableObject
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
