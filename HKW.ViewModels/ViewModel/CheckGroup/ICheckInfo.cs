using System.ComponentModel;

namespace HKW.HKWViewModels;

/// <summary>
/// 检查信息接口
/// </summary>
public interface ICheckInfo : INotifyPropertyChanged
{
    /// <summary>
    /// 已检查
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// 能检查
    /// </summary>
    public bool CanCheck { get; set; }
}
