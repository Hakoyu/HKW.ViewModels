using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Controls.Attachments;

/// <summary>
/// 附加值接口
/// </summary>
/// <typeparam name="T">附加值类型</typeparam>
public interface IAttachment<T>
{
    /// <summary>
    /// 附加值
    /// </summary>
    public T Attachment { get; set; }
}
