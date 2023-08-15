using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 选择器视图模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay("{Name}, SelectedIndex = {SelectedIndex}, Attachment = {Attachment}")]
public partial class SelectorVM<T, TAttachment>
    : ItemCollectionVM<T, TAttachment>,
        ISelectorVM<T>,
        ISelectorVM
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
        SelectionChangedCommand.Execute(SelectedItem);
    }

    /// <inheritdoc cref="ISelectorVM{T}.SelectedItem"/>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedIndex))]
    private T? _selectedItem;

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

    partial void OnSelectedItemChanged(T? value)
    {
        if (value is null)
            return;
        SelectedIndex = ItemsSource.IndexOf(value);
    }

    public SelectorVM(IEnumerable<T>? itemsSource = null)
        : base(itemsSource) { }

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
