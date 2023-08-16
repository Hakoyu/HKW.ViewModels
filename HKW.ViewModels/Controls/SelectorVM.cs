using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 选择器视图模型接口
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
[DebuggerDisplay("{Name}, SelectedIndex = {SelectedIndex}")]
public partial class SelectorVM<T> : ItemCollectionVM<T>, ISelectorVM<T>, ISelectorVM
    where T : ISelectableItemVM
{
    /// <inheritdoc cref="ISelectorVM{T}.SelectedIndex"/>
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

    /// <inheritdoc cref="ISelectorVM{T}.SelectedItem"/>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedIndex))]
    private T? _selectedItem;

    partial void OnSelectedItemChanged(T? value)
    {
        if (value is null)
            return;
        value.IsSelected = true;
        SelectedIndex = ItemsSource.IndexOf(value);
    }

    /// <inheritdoc cref="ISelectorVM{T}.SelectedItem"/>
    object? ISelectorVM.SelectedItem
    {
        get => SelectedItem;
        set => SelectedItem = (T?)value;
    }

    /// <inheritdoc cref="ISelectorVM{T}.SelectedItem"/>
    IRelayCommand ISelectorVM.SelectionChangedCommand => SelectionChangedCommand;

    /// <inheritdoc cref="ISelectorVM{T}.SelectedItem"/>
    IList IItemCollectionVM.ItemsSource
    {
        get => ItemsSource;
        set => ItemsSource = new ObservableCollection<T>(value.Cast<T>());
    }

    public SelectorVM(IEnumerable<T>? itemsSource = null)
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

    /// <inheritdoc cref="ISelectorVM{T}.SelectionChangedCommand"/>
    /// <param name="item">选中项</param>
    [RelayCommand]
    private void SelectionChanged(T item) => SelectionChangedEvent?.Invoke(item);

    /// <inheritdoc/>
    public event ISelectorVM<T>.SelectionChangedHandler? SelectionChangedEvent;
}
