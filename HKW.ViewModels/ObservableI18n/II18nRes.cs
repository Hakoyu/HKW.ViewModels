using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.ObservableI18n;

/// <summary>
/// I18n资源接口
/// </summary>
public interface II18nRes
{
    /// <summary>
    /// I18n资源类
    /// </summary>
    public static I18nRes I18nRes { get; } = null!;
}
