using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 拥有项目集合并且具有标题的控件模型接口
/// </summary>
public interface IHeaderedItemsControlVM<T> : IItemCollectionVM<T>
{
    /// <summary>
    /// 标题
    /// </summary>
    public object? Header { get; set; }
}
