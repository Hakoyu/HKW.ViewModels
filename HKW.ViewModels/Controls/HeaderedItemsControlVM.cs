using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 拥有项目集合并且具有标题的控件模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
[DebuggerDisplay("{Name}, Header = {Header}, Count = {ItemsSource.Count}")]
public partial class HeaderedItemsControlVM<T> : ItemCollectionVM<T>, IHeaderedItemsControlVM<T>
{
    /// <inheritdoc cref="IHeaderedItemsControlVM{T}.Header"/>
    [ObservableProperty]
    private object? _header;
}
