﻿namespace HKW.HKWViewModels.Dialogs;

/// <summary>
/// 基础描述
/// </summary>
public class DescriptionBase
{
    /// <summary>标题</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 过滤器
    /// <para>
    /// 文件选择类型
    /// <para>单个文件: <code>描述|*.*</code></para>
    /// <para>多个文件: <code>描述|*.*;*.*</code></para>
    /// </para>
    /// <para>示例:
    /// <code><![CDATA[
    /// Exe File|*.exe
    /// Exe File, Txt File|*.exe;*.txt
    /// ]]></code>
    /// </para>
    /// </summary>
    public string Filter { get; set; } = string.Empty;

    /// <summary>起始目录</summary>
    public string InitialDirectory { get; set; } = string.Empty;

    /// <summary>文件名</summary>
    public string FileName { get; set; } = string.Empty;
}
