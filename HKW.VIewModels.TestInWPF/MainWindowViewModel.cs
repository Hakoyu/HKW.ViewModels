using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HKW.HKWViewModels;
using HKW.HKWViewModels.Controls;
using HKW.HKWViewModels.Controls.Attachment;

namespace HKW.VIewModels.TestInWPF;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableI18n<Text> _i18n = ObservableI18n<Text>.Create(new());
}

public class Text
{
    public static string Name { get; } = "Name";
}
