using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Interfaces;

/// <summary>
/// 项目集合模型接口
/// </summary>
/// <typeparam name="T">项目类型</typeparam>
public interface IItemCollectionVM<T> : IControlVM
{
    /// <summary>
    /// 项目资源
    /// </summary>
    public ObservableCollection<T> ItemsSource { get; set; }
}
