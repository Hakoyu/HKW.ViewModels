using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 按钮命令接口
/// </summary>
public interface IButtonCommand : ICanExecute
{
    /// <summary>
    /// 异步点击命令
    /// </summary>
    public IAsyncRelayCommand<object> ClickCommand { get; }

    /// <summary>
    /// 命令委托
    /// </summary>
    /// <param name="parameter">参数</param>
    public delegate void CommandHandler(object parameter);

    /// <summary>
    /// 命令事件
    /// </summary>
    public event CommandHandler? CommandEvent;

    /// <summary>
    /// 异步命令委托
    /// </summary>
    /// <param name="parameter">参数</param>
    public delegate Task CommandHandlerAsync(object parameter);

    /// <summary>
    /// 异步命令事件
    /// </summary>
    public event CommandHandlerAsync? CommandEventAsync;
}
