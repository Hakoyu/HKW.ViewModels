using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

public interface IRadioGroup<T, TItem, TGroup>
    where T : IRadioItemCollectionVM<T, TItem, TGroup>
    where TItem : IRadioItemVM
    where TGroup : IRadioGroup<T, TItem, TGroup>
{
    /// <summary>
    /// 分组标识符
    /// </summary>
    public Guid Guid { get; }

    /// <summary>
    /// 分组集合
    /// </summary>
    public IReadOnlySet<T> Groups { get; }

    /// <summary>
    /// 项目分组集合
    /// </summary>
    public ReadOnlyDictionary<string, IReadOnlySet<TItem>> ItemGroups { get; }
}
