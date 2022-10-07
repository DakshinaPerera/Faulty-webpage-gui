using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Database
    {
        private readonly List<DataStruct> database;


        public static Database Instance { get; } = new Database();
        static Database() { }

        public Database()
        {
            database = new List<DataStruct>();
            var generator = new DataGen();
            for (var i = 0; i < generator.NumOAccts(); i++)
            {
                var temp = new DataStruct();
                generator.GetNextAccount(out temp.pin, out temp.acctNo, out temp.firstName, out temp.lastName, out temp.balance);
                database.Add(temp);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            return database[index].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return database[index].pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            return database[index].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return database[index].lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            return database[index].balance;
        }



        public int GetNumRecords()
        {
            return database.Count;
        }
    }
}
