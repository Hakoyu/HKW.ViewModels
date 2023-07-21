using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 列表模型,用于MVVM
/// </summary>
[DebuggerDisplay("{Name},Count = {ItemsSource.Count}")]
public partial class ListBoxVM : SelectorVM<ListBoxItemVM>
{
    /// <inheritdoc/>
    public ListBoxVM()
    {
        ItemsSource ??= new();
    }

    /// <inheritdoc/>
    /// <param name="itemsSource">子项</param>
    public ListBoxVM(ObservableCollection<ListBoxItemVM>? itemsSource = null)
    {
        ItemsSource = itemsSource ?? new();
    }

    /// <inheritdoc/>
    /// <param name="itemsSource">子项委托</param>
    public ListBoxVM(Func<ObservableCollection<ListBoxItemVM>>? itemsSource = null)
    {
        ItemsSource = itemsSource?.Invoke() ?? new();
    }
}
