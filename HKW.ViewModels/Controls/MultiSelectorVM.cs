using HKW.HKWViewModels.Controls.Attachments;
using HKW.HKWViewModels.Controls.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 多选选择器视图模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
[DebuggerDisplay("{Name}, SelectedCount = {SelectedItems.Count}")]
public partial class MultiSelectorVM<T> : ItemCollectionVM<T>, IMultiSelectorVM<T>, IMultiSelectorVM
    where T : IMultiSelectableItemVM
{
    public ObservableCollection<T> SelectedItems { get; } = new();

    IList? IMultiSelectorVM.SelectedItems => SelectedItems;

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectedIndex"/>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedItem))]
    private int _selectedIndex = -1;

    partial void OnSelectedIndexChanged(int value)
    {
        if (value < 0)
            SelectedItem = default;
        else
            SelectedItem = ItemsSource[value];
        if (SelectedItem is not null)
            SelectedItem.IsSelected = true;
        SelectionChangedCommand.Execute(SelectedItem);
    }

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectedItem"/>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedIndex))]
    private T? _selectedItem;

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectedItem"/>
    object? ISelectorVM.SelectedItem
    {
        get => SelectedItem;
        set => SelectedItem = (T?)value;
    }

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectedItem"/>
    IRelayCommand ISelectorVM.SelectionChangedCommand => SelectionChangedCommand;

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectedItem"/>
    IList IItemCollectionVM.ItemsSource
    {
        get => ItemsSource;
        set => ItemsSource = new ObservableCollection<T>(value.Cast<T>());
    }

    partial void OnSelectedItemChanged(T? value)
    {
        if (value is null)
            return;
        value.IsSelected = true;
        SelectedIndex = ItemsSource.IndexOf(value);
    }

    public MultiSelectorVM(IEnumerable<T>? itemsSource = null)
        : base(itemsSource)
    {
        foreach (var item in ItemsSource)
            item.Parent = this;
    }

    /// <inheritdoc/>
    event ISelectorVM.SelectionChangedHandler? ISelectorVM.SelectionChangedEvent
    {
        add =>
            SelectionChangedEvent += (sender) =>
            {
                value?.Invoke(sender);
            };
        remove =>
            SelectionChangedEvent -= (sender) =>
            {
                value?.Invoke(sender);
            };
    }

    /// <inheritdoc cref="IMultiSelectorVM{T}.SelectionChangedCommand"/>
    /// <param name="item">选中项</param>
    [RelayCommand]
    private void SelectionChanged(T item) => SelectionChangedEvent?.Invoke(item);

    /// <inheritdoc/>
    public event IMultiSelectorVM<T>.SelectionChangedHandler? SelectionChangedEvent;
}
