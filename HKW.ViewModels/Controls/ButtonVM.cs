using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 按钮视图模型
/// </summary>
[DebuggerDisplay("{Name}, Content = {Content}")]
public partial class ButtonVM : ContentControlVM, IButtonVM, IButtonCommandVM
{
    /// <inheritdoc cref="ICanExecuteVM.CanExecute"/>
    [ObservableProperty]
    private bool _canExecute = true;

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private async Task ClickAsync(object parameter)
    {
        CommandEvent?.Invoke(parameter);
        if (CommandEventAsync is null)
            return;
        await CommandEventAsync.Invoke(parameter);
    }

    public event IButtonCommandVM.CommandHandler? CommandEvent;

    public event IButtonCommandVM.CommandHandlerAsync? CommandEventAsync;
}
