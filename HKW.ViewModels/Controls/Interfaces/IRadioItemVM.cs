using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可单选项目接口
/// </summary>
public interface IRadioItemVM : ISelectableItemVM
{
    /// <summary>
    /// 分组名称
    /// </summary>
    public string? GroupName { get; set; }
}
