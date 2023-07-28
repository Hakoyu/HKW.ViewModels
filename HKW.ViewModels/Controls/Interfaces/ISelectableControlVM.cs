using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可选中的控件模型接口
/// </summary>
public interface ISelectableControlVM : IContentControlVM
{
    /// <summary>
    /// 已选中
    /// </summary>
    public bool IsSelected { get; set; }
}
