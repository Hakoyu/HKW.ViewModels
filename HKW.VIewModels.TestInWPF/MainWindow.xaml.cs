using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HKW.VIewModels.TestOnWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

    public MainWindow()
    {
        //RadioButton button = new();
        //button.GroupName = "Test";
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}

public static class ToggleButtonHelper
{
    /// <summary>
    /// 设置选中项
    /// </summary>
    /// <param name="toggleButton">可切换按钮</param>
    /// <returns>绑定的列表</returns>
    public static bool GetCanNotUncheckOnClick(ToggleButton toggleButton)
    {
        return (bool)toggleButton.GetValue(CanNotUncheckOnClickProperty);
    }

    /// <summary>
    /// 设置选中项
    /// </summary>
    /// <param name="toggleButton">可切换按钮</param>
    /// <param name="value">选中项</param>
    /// <exception cref="Exception">禁止使用次方法</exception>
    public static void SetCanNotUncheckOnClick(ToggleButton toggleButton, bool value)
    {
        toggleButton.SetValue(CanNotUncheckOnClickProperty, value);
    }

    /// <summary>
    /// 已选中项目属性
    /// </summary>
    public static readonly DependencyProperty CanNotUncheckOnClickProperty =
        DependencyProperty.RegisterAttached(
            "CanNotUncheckOnClick",
            typeof(bool),
            typeof(ToggleButtonHelper),
            new FrameworkPropertyMetadata(false, Initialize)
        );

    private static void Initialize(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is not ToggleButton toggleButton)
            return;
        if (e.NewValue is true)
            toggleButton.Click += OnClick;
    }

    private static void OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton)
            return;
        if (toggleButton.IsChecked is false)
            toggleButton.IsChecked = true;
    }
}
