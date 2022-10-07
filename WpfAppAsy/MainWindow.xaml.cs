using ClassLibrary1;
using DatabseBuissnessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
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

namespace WpfAppAsy
{

    public delegate DataStruct Search(string value);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BuissnessTierInterface foob;
        private Search searchDB;
        private string searchvalue;
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<BuissnessTierInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/DataBusinessService";
            foobFactory = new ChannelFactory<BuissnessTierInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            //Also, tell me how many entries are in the DB.
            totalNo.Text = foob.GetNumEntries().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            //On click, Get the index....
            index = Int32.Parse(indexNo.Text);
            //Then, run our RPC function, using the out mode parameters...
            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
            firstName.Text = fName;
            lastName.Text = lName;
            balance.Text = bal.ToString("C");
            accNum.Text = acct.ToString();
            pinNo.Text = pin.ToString("D4");
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchvalue = search.Text;
            Task<DataStruct> task = new Task<DataStruct>(SearchDB);
            task.Start();
            statusLabel.Content = "Searching starts.....";
            DataStruct student = await task;
            UpdateGui(student);
            statusLabel.Content = "Searching ends.....";

        }

        private DataStruct SearchDB()
        {
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            foob.GetValuesForSearch(searchvalue, out acct, out pin, out bal, out fName, out lName);
            if (!(fName.Equals("")))
            {
                DataStruct aDB = new DataStruct();
                aDB.acctNo = acct;
                aDB.balance = bal;
                aDB.lastName = lName;
                aDB.firstName = fName;
                aDB.pin = pin;
                return aDB;
            }
            return null;
        }

        private void UpdateGui(DataStruct aDB)
        {

            firstName.Dispatcher.Invoke(new Action(() => firstName.Text = aDB.firstName));
            lastName.Dispatcher.Invoke(new Action(() => lastName.Text = aDB.lastName));
            balance.Dispatcher.Invoke(new Action(() => balance.Text = aDB.balance.ToString("C")));
            accNum.Dispatcher.Invoke(new Action(() => accNum.Text = aDB.acctNo.ToString()));
            pinNo.Dispatcher.Invoke(new Action(() => pinNo.Text = aDB.pin.ToString("D4")));
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

