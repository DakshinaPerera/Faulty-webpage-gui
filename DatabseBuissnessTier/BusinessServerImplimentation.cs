using DataBaseServer;
using System.ServiceModel;
using System.Threading;

namespace DatabseBuissnessTier
{
    internal class BusinessServerImplimentation : BuissnessTierInterface
    {

        private DataBaseInterface foob;
        public BusinessServerImplimentation()
        {

            ChannelFactory<DataBaseInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataBaseInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();


        }


        public int GetNumEntries()
        {
            return foob.GetNumEntries();
            
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName);
        }

        public void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            fName = null;
            lName = null;
            bal = 0;
            pin = 0;
            acctNo = 0;
            string searchFname = null;
            string searchLname = null;

            string[] nameList =  searchText.Split(' ');
            searchFname = nameList[0];
            searchLname = nameList[1];

            int numEntry = foob.GetNumEntries();
            for (int i = 1; i <= numEntry; i++)
            {
                string sfName;
                string slName;
                int sBal;
                uint sPin;
                uint sAcctNo;

                foob.GetValuesForEntry(i, out sAcctNo, out sPin, out sBal, out sfName, out slName);
                if ((sfName.ToLower().Contains(searchFname.ToLower())) && (slName.ToLower().Contains(searchLname.ToLower())))
                {
                    fName = sfName;
                    lName = slName;
                    bal = sBal;
                    pin = sPin;
                    acctNo = sAcctNo;
                    break;
                }
            }
            Thread.Sleep(5000); //Forced sleep for two seconds
        }
    }
}
