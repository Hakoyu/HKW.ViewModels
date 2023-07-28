using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 基础控件模型接口
/// </summary>
public interface IControlVM
{
    /// <summary>
    /// Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// 是否可见
    /// <para>
    /// <see langword="true"/> 为可见 <see langword="false"/> 为不可见 <see langword="null"/> 为不可见并不占用空间
    /// </para>
    /// </summary>
    [DefaultValue(true)]
    public bool? IsVisible { get; set; }

    /// <summary>
    /// 提示
    /// </summary>
    public object? ToolTip { get; set; }

    /// <summary>
    /// 上下文菜单
    /// </summary>
    public ContextMenuVM? ContextMenu { get; set; }
}
