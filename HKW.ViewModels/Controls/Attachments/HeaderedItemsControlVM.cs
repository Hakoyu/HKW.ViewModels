using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 带多个项并且具有标题的控件模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
[DebuggerDisplay(
    "{Name}, Header = {Header}, Count = {ItemsSource.Count},  Attachment = {Attachment}"
)]
public partial class HeaderedItemsControlVM<T, TAttachment>
    : ItemCollectionVM<T, TAttachment>,
        IHeaderedItemsControlVM<T>
{
    /// <inheritdoc cref="IHeaderedItemsControlVM{T}.Header"/>
    [ObservableProperty]
    private object? _header;
}
