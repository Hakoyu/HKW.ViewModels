using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachment;

/// <summary>
/// 按钮视图模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Content = {Content}, Attachment = {Attachment}")]
public partial class ButtonVM<T> : ContentControlVM<T>, IButtonVM, IButtonCommandVM
{
    /// <inheritdoc cref="IButtonVM.CanExecute"/>
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
