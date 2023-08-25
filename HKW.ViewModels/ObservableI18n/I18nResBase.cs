using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels;

/// <summary>
/// I18n资源基础
/// 配合 <see cref="ObservableI18n{T}"/> 使用
/// <para>示例:
/// <code><![CDATA[
/// public partial class MainWindowViewModel : ObservableObject
/// {
///     [ObservableProperty]
///     public ObservableI18n<TestI18nRes> _i18n = ObservableI18n<TestI18nRes>.Create(new());
/// }
/// public class TestI18nRes : I18nResBase
/// {
///     public static string Name => GetCultureData(nameof(Name));
/// }
/// ]]>
/// </code>
/// </para>
/// </summary>
public class I18nResBase
{
    /// <summary>
    /// 严格模式 默认为: <see langword="false"/>
    /// <para>开启后任何失败操作均会触发异常</para>
    /// </summary>
    public static bool SrictMode { get; set; } = false;

    /// <summary>
    /// 覆盖模式 <see langword="false"/>
    /// <para>开启后添加已存在的新值覆盖旧值</para>
    /// </summary>
    public static bool EnableOverride { get; set; } = false;

    #region I18nData
    /// <summary>
    /// 所有文化数据
    /// <para>(Culture.Name, <see cref="CurrentCultureData"/>)</para>
    /// </summary>
    protected static Dictionary<string, Dictionary<string, string>> CultureDatas { get; } = new();

    /// <summary>
    /// 当前文化数据
    /// <para>(key, value)</para>
    /// </summary>
    protected static Dictionary<string, string> CurrentCultureData { get; private set; } = new();
    #endregion
    /// <summary>
    /// 注册文化改变事件
    /// </summary>
    static I18nResBase()
    {
        ObservableI18n.CultureChanged += (c) =>
        {
            SetCurrentCulture(c.Name);
        };
    }

    #region CurrentCultureData Operation
    /// <summary>
    /// 添加文化数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool AddCultureData(string key, string value)
    {
        if (EnableOverride)
        {
            if (CurrentCultureData.TryAdd(key, value) is false)
                CurrentCultureData[key] = value;
            return true;
        }
        if (SrictMode)
        {
            CurrentCultureData.Add(key, value);
            return true;
        }
        return CurrentCultureData.TryAdd(key, value);
    }

    /// <summary>
    /// 删除文化数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool RemoveCultureData(string key)
    {
        return CurrentCultureData.Remove(key);
    }

    /// <summary>
    /// 获取文化数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>文化数据</returns>
    public static string GetCultureData(string key)
    {
        if (SrictMode)
        {
            return CurrentCultureData[key];
        }
        if (CurrentCultureData.TryGetValue(key, out var value))
            return value;
        return string.Empty;
    }

    /// <summary>
    /// 尝试获取文化数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryGetCultureData(string key, [NotNullWhen(true)] out string? value)
    {
        return CurrentCultureData.TryGetValue(key, out value);
    }

    /// <summary>
    /// 清空文化数据
    /// </summary>
    public static void ClearCultureData()
    {
        CurrentCultureData.Clear();
    }
    #endregion
    #region  CultureData Operation
    /// <summary>
    /// 添加文化数据
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool AddCultureData(string cultureName, string key, string value)
    {
        if (SrictMode)
        {
            if (EnableOverride)
            {
                if (CultureDatas[cultureName].TryAdd(key, value) is false)
                    CultureDatas[cultureName][key] = value;
                return true;
            }
            CultureDatas[cultureName].Add(key, value);
            return true;
        }
        if (CultureDatas.TryGetValue(cultureName, out var data))
        {
            if (data.TryAdd(key, value) is false && EnableOverride)
                data[key] = value;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 添加文化数据
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <param name="key">键</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool RemoveCultureData(string cultureName, string key)
    {
        if (SrictMode)
        {
            CultureDatas[cultureName].Remove(key);
            return true;
        }
        if (CultureDatas.TryGetValue(cultureName, out var data))
            return data.Remove(key);
        return false;
    }

    /// <summary>
    /// 获取文化数据
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <param name="key">键</param>
    /// <returns>文化数据</returns>
    public static string GetCultureData(string cultureName, string key)
    {
        if (SrictMode)
        {
            return CultureDatas[cultureName][key];
        }
        if (CultureDatas.TryGetValue(cultureName, out var data))
            if (data.TryGetValue(key, out var value))
                return value;
        return string.Empty;
    }

    /// <summary>
    /// 尝试获取文化数据
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool TryGetCultureData(
        string cultureName,
        string key,
        [NotNullWhen(true)] out string? value
    )
    {
        value = null;
        if (CultureDatas.TryGetValue(cultureName, out var data))
            return data.TryGetValue(key, out value);
        return false;
    }

    /// <summary>
    /// 清空文化数据
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    public static void ClearCultureData(string cultureName)
    {
        if (SrictMode)
        {
            CultureDatas[cultureName].Clear();
        }
        if (CultureDatas.TryGetValue(cultureName, out var data))
            data.Clear();
    }
    #endregion

    #region Culture Operation
    /// <summary>
    /// 设置当前文化
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool SetCurrentCulture(string cultureName)
    {
        if (SrictMode)
        {
            CurrentCultureData = CultureDatas[cultureName];
            return true;
        }
        if (CultureDatas.TryGetValue(cultureName, out var cultureData))
        {
            CurrentCultureData = cultureData;
            return true;
        }
        CurrentCultureData = CultureDatas
            .FirstOrDefault(defaultValue: new(string.Empty, CurrentCultureData))
            .Value;
        return false;
    }

    /// <summary>
    /// 添加文化
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool AddCulture(string cultureName)
    {
        if (SrictMode)
        {
            CultureDatas.Add(cultureName, new());
            return true;
        }
        return CultureDatas.TryAdd(cultureName, new());
    }

    /// <summary>
    /// 删除文化
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    public static bool RemoveCulture(string cultureName)
    {
        return CultureDatas.Remove(cultureName);
    }

    /// <summary>
    /// 清空所有文化
    /// <para>注意:
    /// 此操作会将 <see cref="CurrentCultureData"/> 设置为 <see langword="null"/>
    /// </para>
    /// </summary>
    public static void ClearCulture()
    {
        CultureDatas.Clear();
        CurrentCultureData = null!;
    }

    ///// <summary>
    ///// 改变文化
    ///// </summary>
    ///// <param name="oldCultureName">旧文化名称</param>
    ///// <param name="newCultureName">新文化名称</param>
    ///// <returns>成功为 <see langword="true"/> 失败为 <see langword="false"/></returns>
    //public static bool ChangeCulture(string oldCultureName, string newCultureName)
    //{
    //    if (
    //        CultureDatas.ContainsKey(oldCultureName) is false
    //        || CultureDatas.ContainsKey(newCultureName)
    //    )
    //        return false;
    //    var cultureData = CultureDatas[oldCultureName];
    //    CultureDatas.Remove(oldCultureName);
    //    CultureDatas.TryAdd(newCultureName, cultureData);
    //    return true;
    //}
    #endregion
}
