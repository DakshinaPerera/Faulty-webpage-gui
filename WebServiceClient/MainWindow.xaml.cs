using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using API_Classes;
using Newtonsoft.Json;
using RestSharp;

namespace WebServiceClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient client;
        public MainWindow()
        {
            InitializeComponent();
            string URL = "http://localhost:57791/";
            client = new RestClient(URL);
            RestRequest request = new RestRequest("api/values");
            RestResponse numOfThings = client.Get(request);
            totalNo.Text = numOfThings.Content;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(indexNo.Text);
            //Then, set up and call the API method...
            RestRequest request = new RestRequest("api/values/" + index.ToString());
            RestResponse resp = client.Get(request);
            DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(resp.Content);
            //And now, set the values in the GUI!
            firstName.Text = dataIntermed.fname;
            lastName.Text = dataIntermed.lname;
            balance.Text = dataIntermed.bal.ToString("C");
            accNum.Text = dataIntermed.acct.ToString();
            pinNo.Text = dataIntermed.pin.ToString("D4");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchData mySearch = new SearchData();
            mySearch.searchStr = search.Text;
            //Build a request with the json in the body
            RestRequest restRequest = new RestRequest("api/Searchs", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(mySearch));
            //Do the request
            RestResponse restResponse = client.Execute(restRequest);
            //Deserialize the result
            DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);
            //aaaaand input the data
            firstName.Text = dataIntermed.fname;
            lastName.Text = dataIntermed.lname;
            balance.Text = dataIntermed.bal.ToString("C");
            accNum.Text = dataIntermed.acct.ToString();
            pinNo.Text = dataIntermed.pin.ToString("D4");
        }
    }
}
