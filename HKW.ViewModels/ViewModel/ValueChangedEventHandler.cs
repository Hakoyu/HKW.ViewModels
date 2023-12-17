using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels;

/// <summary>
/// 属性值改变后事件
/// </summary>
public delegate void ValueChangedEventHandler<T>(T sender, ValueChangedEventArgs e);
