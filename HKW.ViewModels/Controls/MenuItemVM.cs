using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 菜单项模型
/// </summary>
[DebuggerDisplay("{Name}, Header = {Header}, Count = {ItemsSource.Count}")]
public partial class MenuItemVM
    : HeaderedItemsControlVM<MenuItemVM>,
        IHeaderedItemsControlVM<MenuItemVM>,
        IButtonCommandVM,
        IIconVM
{
    /// <inheritdoc cref="IIconVM.Icon"/>
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
        foreach (
            var command in CommandEventAsync
                .GetInvocationList()
                .Cast<IButtonCommandVM.CommandHandlerAsync>()
        )
        {
            await command.Invoke(parameter);
        }
    }

    public event IButtonCommandVM.CommandHandler? CommandEvent;

    public event IButtonCommandVM.CommandHandlerAsync? CommandEventAsync;
}
