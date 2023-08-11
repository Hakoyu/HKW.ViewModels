using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWUtils.Collections;
using HKW.HKWUtils.Extensions;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 单选分组
/// </summary>
/// <typeparam name="T">单选集合类型</typeparam>
/// <typeparam name="TItem"></typeparam>
public class RadioGroup<T, TItem> : IRadioGroup<T, TItem, RadioGroup<T, TItem>>
    where T : IRadioItemCollectionVM<T, TItem, RadioGroup<T, TItem>>
    where TItem : IRadioItemVM
{
    /// <inheritdoc/>
    public Guid Guid { get; } = Guid.NewGuid();

    /// <inheritdoc/>
    public IReadOnlySet<T> Groups { get; }

    internal HashSet<T> IGroups { get; } = new();

    /// <inheritdoc/>
    public ReadOnlyDictionary<string, IReadOnlySet<TItem>> ItemGroups { get; }

    internal Dictionary<string, HashSet<TItem>> IItemGroups { get; } = new();

    public RadioGroup()
    {
        Groups = IGroups;
        ItemGroups = IItemGroups.AsReadOnlyOnWrapper<string, HashSet<TItem>, IReadOnlySet<TItem>>();
    }
}
