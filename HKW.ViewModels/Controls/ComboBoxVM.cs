using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.HKWViewModels.Controls;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 组合框视图模型
/// </summary>
[DebuggerDisplay("{Name},Count = {ItemsSource.Count}")]
public partial class ComboBoxVM : SelectorVM<ComboBoxItemVM>
{
    /// <inheritdoc/>
    public ComboBoxVM()
    {
        ItemsSource ??= new();
    }

    /// <inheritdoc/>
    /// <param name="itemsSource">子项</param>
    public ComboBoxVM(ObservableCollection<ComboBoxItemVM>? itemsSource = null)
    {
        ItemsSource = itemsSource ?? new();
    }

    /// <inheritdoc/>
    /// <param name="itemsSource">子项委托</param>
    public ComboBoxVM(Func<ObservableCollection<ComboBoxItemVM>>? itemsSource = null)
    {
        ItemsSource = itemsSource?.Invoke() ?? new();
    }
}
