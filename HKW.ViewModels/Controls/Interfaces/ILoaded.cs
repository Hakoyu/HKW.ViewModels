using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 加载完成接口
/// </summary>
///
public interface ILoaded<T>
{
    /// <summary>
    /// 已加载
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// 加载完成命令
    /// </summary>
    public IRelayCommand<object> LoadedCommand { get; }

    /// <summary>
    /// 加载完成委托
    /// </summary>
    /// <param name="items">参数</param>
    public delegate T LoadedHandler();

    /// <summary>
    /// 加载完成事件
    /// </summary>
    public event LoadedHandler? LoadedEvent;
}
