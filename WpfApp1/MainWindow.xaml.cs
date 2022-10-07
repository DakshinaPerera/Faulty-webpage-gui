using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ServiceModel;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBaseInterface foob;
        public MainWindow()
        {

                InitializeComponent();
                ChannelFactory<DataBaseInterface> foobFactory;
                NetTcpBinding tcp = new NetTcpBinding();
                string URL = "net.tcp://localhost:8100/DataService";
                foobFactory = new ChannelFactory<DataBaseInterface>(tcp, URL);
                foob = foobFactory.CreateChannel();
                NoItems.Content = "Total Items: " + foob.GetNumEntries();
                LoadData(0);
                IndexBox.Text = "0";

        }

        private void IndexButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IndexBox.Text, out var index))
            {
                LoadData(index);
            }
            else
            {
                MessageBox.Show($"\"{IndexBox.Text}\" is not a valid integer...");
            }
        }

        private void LoadData(int index)
        {
         
                foob.GetValuesForEntry(index, out var accNo, out var pin, out var bal, out var fName, out var lName);
                FirstName.Text = fName;
                LastName.Text = lName;
                Balance.Text = bal.ToString("C");
                AcctNo.Text = accNo.ToString();
                Pin.Text = pin.ToString("D4");
                

          
        }

    }
}
