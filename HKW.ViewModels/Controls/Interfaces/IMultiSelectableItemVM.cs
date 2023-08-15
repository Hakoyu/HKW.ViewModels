using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可被多选的可选中的控件模型接口
/// </summary>
public interface IMultiSelectableItemVM
{
    /// <summary>
    /// 已选中
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public IMultiSelectorVM? Parent { get; set; }
}
