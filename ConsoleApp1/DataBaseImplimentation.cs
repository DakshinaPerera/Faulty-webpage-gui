using System.ServiceModel;
using ClassLibrary1;

namespace DataBaseServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataBaseImplimentation : DataBaseInterface
    {
        private readonly Database databa = Database.Instance;

        public int GetNumEntries()
        {
            return databa.GetNumRecords();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {

            acctNo = databa.GetAcctNoByIndex(index);
            pin = databa.GetPINByIndex(index);
            bal = databa.GetBalanceByIndex(index);
            fName = databa.GetFirstNameByIndex(index);
            lName = databa.GetLastNameByIndex(index);

        }
    }
}