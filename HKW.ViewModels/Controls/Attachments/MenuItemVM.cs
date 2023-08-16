using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 菜单项模型
/// </summary>
/// <typeparam name="TAttachment">附加值类型</typeparam>
[DebuggerDisplay(
    "{Name}, Header = {Header}, Count = {ItemsSource.Count}, Attachment = {Attachment}"
)]
public partial class MenuItemVM<TAttachment>
    : HeaderedItemsControlVM<MenuItemVM<TAttachment>>,
        IAttachment<TAttachment>
{
    /// <inheritdoc cref="IAttachment{T}.Attachment"/>
    [ObservableProperty]
    private TAttachment? _attachment;
}
