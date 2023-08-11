using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 可单选集合接口
/// </summary>
/// <typeparam name="T">单选集合类型</typeparam>
/// <typeparam name="TItem">单选项目类型</typeparam>
/// <typeparam name="TGroup">单选分组类型</typeparam>
public interface IRadioItemCollectionVM<T, TItem, TGroup> : ISelectorVM<TItem>
    where T : IRadioItemCollectionVM<T, TItem, TGroup>
    where TItem : IRadioItemVM
    where TGroup : IRadioGroup<T, TItem, TGroup>
{
    /// <summary>
    /// 分组信息
    /// </summary>
    public TGroup? GroupInfo { get; }

    /// <summary>
    /// 设置当前集合内所有项目的分组名称
    /// </summary>
    /// <param name="groupName">分组名称</param>
    public void SetItemsGroup(string groupName);

    /// <summary>
    /// 删除指定名称的分组
    /// </summary>
    /// <param name="groupName">分组名称</param>
    public void RemoveItemsGroup(string groupName);

    /// <summary>
    /// 替换指定名称的分组名称
    /// </summary>
    /// <param name="groupName">原分组名称</param>
    /// <param name="newGroupName">新分组名称</param>
    public void ReplaceItemsGroup(string groupName, string newGroupName);

    /// <summary>
    /// 清除所有项目的分组
    /// </summary>
    public void ClearItemsGroup();

    /// <summary>
    /// 添加单选分组, 用于多列表互斥
    /// </summary>
    /// <param name="collections">多个分组</param>
    public void AddRadioCollection(params T[] collections);

    /// <summary>
    /// 删除单选分组, 用于多列表互斥
    /// </summary>
    /// <param name="collections">多个分组</param>
    public void RemoveRadioCollection(params T[] collections);

    /// <summary>
    /// 清除当前列表的分组信息
    /// </summary>
    public void ClearGroupInfo();
}
