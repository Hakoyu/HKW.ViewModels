using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可执行接口
/// </summary>
public interface ICanExecute
{
    /// <summary>
    /// 可执行
    /// </summary>
    public bool CanExecute { get; set; }
}
