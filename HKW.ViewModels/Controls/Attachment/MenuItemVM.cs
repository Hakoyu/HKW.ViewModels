using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 菜单项模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay(
    "{Name}, Header = {Header}, Count = {ItemsSource.Count}, Attachment = {Attachment}"
)]
public partial class MenuItemVM<T>
    : HeaderedItemsControlVM<MenuItemVM<T>, T>,
        IHeaderedItemsControlVM<MenuItemVM<T>>,
        IButtonCommand,
        IIcon
{
    /// <inheritdoc cref="IIcon.Icon"/>
    [ObservableProperty]
    private object? _icon;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private bool _canExecute = true;

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private async Task ClickAsync(object parameter)
    {
        CommandEvent?.Invoke(parameter);
        if (CommandEventAsync is null)
            return;
        await CommandEventAsync.Invoke(parameter);
    }

    public event IButtonCommand.CommandHandler? CommandEvent;

    public event IButtonCommand.CommandHandlerAsync? CommandEventAsync;
}
