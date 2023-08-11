using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 选择器视图模型
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
/// <typeparam name="TAttachment">附加值类型</typeparam>
public partial class SelectorVM<T, TAttachment> : ItemCollectionVM<T, TAttachment>, ISelectorVM<T>
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

    partial void OnSelectedItemChanged(T? value)
    {
        if (value is null)
            return;
        SelectedIndex = ItemsSource.IndexOf(value);
    }

    public SelectorVM(IEnumerable<T>? itemsSource = null)
        : base(itemsSource) { }

    /// <inheritdoc cref="ISelectorVM{T}.SelectionChangedCommand"/>
    /// <param name="item">选中项</param>
    [RelayCommand]
    private void SelectionChanged(T item) => SelectionChangedEvent?.Invoke(item);

    /// <inheritdoc/>
    public event ISelectorVM<T>.SelectionChangedHandler? SelectionChangedEvent;
}
