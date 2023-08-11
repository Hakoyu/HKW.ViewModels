using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 可单选集合视图模型
/// </summary>
public partial class RadioItemCollectionVM<T>
    : SelectorVM<T>,
        IRadioItemCollectionVM<RadioItemCollectionVM<T>, T, RadioGroup<RadioItemCollectionVM<T>, T>>
    where T : IRadioItemVM
{
    private static readonly Dictionary<
        Guid,
        RadioGroup<RadioItemCollectionVM<T>, T>
    > sr_radioGroups = new();

    /// <inheritdoc/>
    public RadioGroup<RadioItemCollectionVM<T>, T>? GroupInfo { get; protected set; }

    /// <inheritdoc/>
    public void SetItemsGroup(string groupName)
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        var radioGroup = sr_radioGroups[groupInfo.Guid];
        if (radioGroup.IItemGroups.TryGetValue(groupName, out var itemsGroup) is false)
            radioGroup.IItemGroups.Add(groupName, itemsGroup = new());
        foreach (var group in radioGroup.IGroups)
        {
            itemsGroup.UnionWith(group.ItemsSource);
            foreach (var item in group.ItemsSource)
                item.GroupName = groupName;
        }
    }

    /// <inheritdoc/>
    public void RemoveItemsGroup(string groupName)
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        var radioGroup = sr_radioGroups[groupInfo.Guid];
        if (radioGroup.IItemGroups.TryGetValue(groupName, out var itemsGroup) is false)
            throw new ArgumentException("Group not exist", nameof(groupName));
        foreach (var item in itemsGroup)
            item.GroupName = null;
        radioGroup.IItemGroups.Remove(groupName);
    }

    /// <inheritdoc/>
    public void ReplaceItemsGroup(string groupName, string newGroupName)
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        var radioGroup = sr_radioGroups[groupInfo.Guid];
        if (radioGroup.IItemGroups.TryGetValue(groupName, out var itemsGroup) is false)
            throw new ArgumentException("Group not exist", nameof(groupName));
        radioGroup.IItemGroups.Remove(groupName);
        radioGroup.IItemGroups.Add(newGroupName, itemsGroup);
        foreach (var item in ItemsSource)
            if (item.GroupName == groupName)
                item.GroupName = newGroupName;
    }

    /// <inheritdoc/>
    public void ClearItemsGroup()
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        foreach (var group in groupInfo.IGroups)
        {
            foreach (var item in group.ItemsSource)
                item.GroupName = null;
        }
        groupInfo.IItemGroups.Clear();
    }

    /// <inheritdoc/>
    public void AddRadioCollection(params RadioItemCollectionVM<T>[] collections)
    {
        if (GroupInfo is null)
        {
            GroupInfo = new();
            RadioGroup<RadioItemCollectionVM<T>, T> radioGroup = new();
            foreach (var collection in collections)
            {
                collection.GroupInfo = GroupInfo;
                radioGroup.IGroups.Add(collection);
            }
            sr_radioGroups.Add(GroupInfo.Guid, GroupInfo);
        }
        else
        {
            var radioGroup = sr_radioGroups[GroupInfo.Guid];
            foreach (var collection in collections)
            {
                collection.GroupInfo = GroupInfo;
                radioGroup.IGroups.Add(collection);
            }
        }
    }

    /// <inheritdoc/>
    public void RemoveRadioCollection(params RadioItemCollectionVM<T>[] collections)
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        else
        {
            var radioGroup = sr_radioGroups[groupInfo.Guid];
            foreach (var collection in collections)
            {
                collection.GroupInfo = null;
                foreach (var item in collection.ItemsSource)
                {
                    // 删除指定分组内的项目
                    if (item.GroupName is string groupName)
                    {
                        radioGroup.IItemGroups[groupName].Remove(item);
                        item.GroupName = string.Empty;
                    }
                }
                radioGroup.IGroups.Remove(collection);
            }
            ClearEmptyGroup(radioGroup);
        }
    }

    /// <inheritdoc/>
    public void ClearGroupInfo()
    {
        if (GroupInfo is not RadioGroup<RadioItemCollectionVM<T>, T> groupInfo)
            throw new ArgumentNullException(nameof(GroupInfo), "No group have been added");
        var radioGroup = sr_radioGroups[groupInfo.Guid];
        foreach (var items in radioGroup.IItemGroups)
            items.Value.ExceptWith(ItemsSource);
        foreach (var item in ItemsSource)
            item.GroupName = null;
        radioGroup.IGroups.Remove(this);
        ClearEmptyGroup(radioGroup);
    }

    private static void ClearEmptyGroup(RadioGroup<RadioItemCollectionVM<T>, T> radioGroup)
    {
        foreach (var group in radioGroup.ItemGroups.Where(kv => kv.Value.Count is 0))
            radioGroup.IItemGroups.Remove(group.Key);
    }
}
