using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            //InitializeComponent() 就是 “把 XAML 变成真正可用 UI 的自动生成方法”。
            //InitializeComponent() 不是手写的代码，而是 XAML 编译器在构建过程中自动生成的，通常生成在：obj/Debug/.../MainWindow.g.cs
            InitializeComponent();
            /*
                DataContext 是什么？
                DataContext 是 WPF 数据绑定系统的 “默认数据源”。
                当控件内部没有显式指定 Source 时，绑定会从 DataContext 中取值。
                简单说：你在 XAML 里写的 {Binding ...}，默认就是去 DataContext 上找对应属性。
             */
            DataContext = new DirectoryStructureViewModel();
        }
        #endregion
    }
}
