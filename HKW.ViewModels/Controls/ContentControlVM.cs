using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可包含任意类型内容的控件模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class ContentControlVM : ControlVMBase
{
    [ObservableProperty]
    private object? _icon;

    [ObservableProperty]
    private object? _content;
}
