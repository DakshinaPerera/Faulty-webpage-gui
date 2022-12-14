using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DatabseBuissnessTier
{
    [ServiceContract]
    public interface BuissnessTierInterface
    {
                
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);

        [OperationContract]
        void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
    }
}
