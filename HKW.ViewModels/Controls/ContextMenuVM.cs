﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

/// <summary>
/// 上下文菜单模型
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
[DebuggerDisplay("{Name}, Count = {ItemsSource.Count}")]
public partial class ContextMenuVM
    : ItemCollectionVM<MenuItemVM>,
        ILoaded<ObservableCollection<MenuItemVM>>
{
    /// <summary>
    /// 已打开
    /// </summary>
    [ObservableProperty]
    private bool _isOpen = false;

    /// <inheritdoc cref="ILoaded{T}.IsLoaded"/>
    [ObservableProperty]
    private bool _isLoaded = false;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="handler">委托</param>
    public ContextMenuVM(ILoaded<ObservableCollection<MenuItemVM>>.LoadedHandler? handler = null)
    {
        if (handler is not null)
            LoadedEvent += handler;
    }

    /// <summary>
    /// 加载命令
    /// <para>xaml示例
    /// <code><![CDATA[
    /// <i:Interaction.Triggers>
    ///   <!--  使用Loaded时, 如果有多个未载入的菜单, 使用右键挨个点击只会载入第一个, 故使用Opened  -->
    ///   <i:EventTrigger EventName="Opened">
    ///     <i:InvokeCommandAction Command="{Binding ContextMenu.LoadedCommand}"/>
    ///   </i:EventTrigger>
    /// </i:Interaction.Triggers>
    /// ]]>
    /// </code></para>
    /// </summary>
    /// <param name="parameter">参数</param>
    [RelayCommand]
    private void Loaded(object parameter)
    {
        if (LoadedEvent is not null && IsLoaded is false)
        {
            ItemsSource = LoadedEvent();
            IsLoaded = true;
        }
    }

    /// <inheritdoc cref="ILoaded{T}.LoadedEvent"/>
    public event ILoaded<ObservableCollection<MenuItemVM>>.LoadedHandler? LoadedEvent;
}
