using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可包含任意类型内容的控件模型接口
/// </summary>
public interface IContentControlVM : IControlVM
{
    /// <summary>
    /// 图标
    /// </summary>
    public object? Icon { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public object? Content { get; set; }
}
