using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 可包含任意类型内容的控件模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class ContentControlVM<T> : ControlVM<T>, IContentControlVM
{
    /// <inheritdoc cref="IContentControlVM.Icon"/>
    [ObservableProperty]
    private object? _icon;

    /// <inheritdoc cref="IContentControlVM.Content"/>
    [ObservableProperty]
    private object? _content;
}
