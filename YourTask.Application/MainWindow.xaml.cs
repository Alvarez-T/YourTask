using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YourTask.Application.Helpers;
using YourTask.Application.Services;
using YourTask.Application.ViewModels;

namespace YourTask.Application;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : HandyControl.Controls.Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ServiceLocator.GetService<MainViewModel>();

        cbStatus.ItemsSource = EnumHelper.GetEnumDescriptions<StatusTarefa>();

        if (DesignerProperties.GetIsInDesignMode(this))
        {
            DataContext = DesignData.LoadDesignData();
        }
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await ((MainViewModel)DataContext).CarregarTarefas();
    }
}