using DataWebApi.Models;
using Newtonsoft.Json;
using RestSharp;
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

namespace WebApiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient("http://localhost:57791/");
            RestRequest restRequest = new RestRequest("api/Generate", Method.Get);
            restRequest.AddBody(11);
            RestResponse restResponse = restClient.Execute(restRequest);

            BankDetail bankDetail = JsonConvert.DeserializeObject<BankDetail>(restResponse.Content);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient("http://localhost:57791/");
            RestRequest restRequest = new RestRequest("api/Search/{searchText}", Method.Get);
            restRequest.AddUrlSegment("searchText", search.Text);
            RestResponse restResponse = restClient.Execute(restRequest);

            BankDetail bankDetail = JsonConvert.DeserializeObject<BankDetail>(restResponse.Content);
            indexNo.Text = bankDetail.Id.ToString();
            firstName.Text = bankDetail.firstName;
            lastName.Text = bankDetail.lastName;
            balance.Text = bankDetail.balance.ToString();
            accNum.Text = bankDetail.acctNo.ToString();
            pinNo.Text = bankDetail.pin.ToString();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            BankDetail bankDetail = new BankDetail();
            bankDetail.Id = Int32.Parse(indexNo.Text);
            bankDetail.firstName = firstName.Text;
            bankDetail.lastName = lastName.Text;
            bankDetail.balance = Int32.Parse(balance.Text);
            bankDetail.acctNo = Int32.Parse(accNum.Text);
            bankDetail.pin = Int32.Parse(pinNo.Text);



            RestClient restClient = new RestClient("http://localhost:57791/");
            RestRequest restRequest = new RestRequest("api/Bank/{id}", Method.Put);
            restRequest.AddUrlSegment("id", Int32.Parse(indexNo.Text));
            restRequest.AddJsonBody(JsonConvert.SerializeObject(bankDetail));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankDetail returnStudent = JsonConvert.DeserializeObject<BankDetail>(restResponse.Content);
            if (returnStudent != null)
            {
                MessageBox.Show("Data Successfully Inserted");
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
        }

        private void InButton_Click(object sender, RoutedEventArgs e)
        {
            BankDetail bankDetail = new BankDetail();
            bankDetail.Id = Int32.Parse(indexNo.Text);
            bankDetail.firstName = firstName.Text;
            bankDetail.lastName = lastName.Text;
            bankDetail.balance = Int32.Parse(balance.Text);
            bankDetail.acctNo = Int32.Parse(accNum.Text);
            bankDetail.pin = Int32.Parse(pinNo.Text);



            RestClient restClient = new RestClient("http://localhost:57791/");
            RestRequest restRequest = new RestRequest("api/Bank/", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(bankDetail));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankDetail returnStudent = JsonConvert.DeserializeObject<BankDetail>(restResponse.Content);
            if (returnStudent != null)
            {
                MessageBox.Show("Data Successfully Inserted");
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
        }
    }
}
