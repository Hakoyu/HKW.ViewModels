using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 菜单项模型,用于MVVM
/// </summary>
[DebuggerDisplay("{Name},Header = {Header},Count = {ItemsSource.Count}")]
public partial class MenuItemVM : HeaderedItemsControlVM<MenuItemVM>
{
    [ObservableProperty]
    private object? _icon;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(MenuItemCommand))]
    private bool _canExecute = true;

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private async Task MenuItem(object parameter)
    {
        CommandEvent?.Invoke(parameter);
        if (CommandEventAsync is null)
            return;
        await CommandEventAsync.Invoke(parameter);
    }

    /// <summary>
    /// 委托
    /// </summary>
    /// <param name="parameter">参数</param>
    public delegate void CommandHandler(object parameter);

    /// <summary>
    /// 事件
    /// </summary>
    public event CommandHandler? CommandEvent;

    /// <summary>
    /// 异步委托
    /// </summary>
    /// <param name="parameter">参数</param>
    public delegate Task CommandHandlerAsync(object parameter);

    /// <summary>
    /// 异步事件
    /// </summary>
    public event CommandHandlerAsync? CommandEventAsync;
}
