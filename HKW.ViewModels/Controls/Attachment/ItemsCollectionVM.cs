using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 项目集合模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count},  Attachment = {Attachment}")]
public partial class ItemCollectionVM<T, TAttachment> : ControlVM<TAttachment>, IItemCollectionVM<T>
{
    /// <inheritdoc cref="IItemCollectionVM.ItemsSource"/>
    [ObservableProperty]
    private ObservableCollection<T> _itemsSource;

    /// <inheritdoc/>
    /// <param name="itemsSource">项目集合</param>
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
    public ItemCollectionVM(IEnumerable<T>? itemsSource = null)
    {
        if (itemsSource is ObservableCollection<T> items)
            ItemsSource = items;
        else if (itemsSource is not null)
            ItemsSource = new(itemsSource);
        else
            ItemsSource = new();
    }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
}
