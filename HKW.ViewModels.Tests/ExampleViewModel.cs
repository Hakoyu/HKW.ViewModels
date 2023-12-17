using HKW.HKWUtils.Observable;
using HKW.HKWViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Tests;

public class ExampleViewModel : ViewModelBase<ExampleViewModel>
{
    public static int Value1Default => 114;
    private int _value1 = Value1Default;

    public int Value1
    {
        get => _value1;
        set => SetProperty(ref _value1, value);
    }

    public static int Value2Default => 514;
    private int _value2 = Value2Default;
    public int Value2
    {
        get => _value2;
        set => SetProperty(ref _value2, value);
    }
    public static int Value3Default => 1919;
    private int _value3 = Value3Default;
    public int Value3
    {
        get => _value3;
        set => SetProperty(ref _value3, value);
    }
    public static int Value4Default => 810;
    private int _value4 = Value4Default;
    public int Value4
    {
        get => _value4;
        set => SetProperty(ref _value4, value);
    }
}
