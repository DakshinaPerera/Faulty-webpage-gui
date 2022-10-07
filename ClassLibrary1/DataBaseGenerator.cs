// Alex Starling - Distributed Computing - 2021
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ClassLibrary1
{
    internal class DataGen
    {
        private readonly Random rand = new Random();

        private readonly string[] fullNameList = { "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth", "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica" };

        private readonly string[] lastlNameList = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore" };


        private string GetFirstName()
        {
            return fullNameList[rand.Next(fullNameList.Length)];
        }
        private string GetLastName()
        {
            return lastlNameList[rand.Next(lastlNameList.Length)];
        }

        private uint GetPIN()
        {
            return (uint)rand.Next(99999);
        }

        private uint GetAcctNo()
        {
            return (uint)rand.Next(000000000, 999999999);
        }

        private int GetBalance()
        {
            return rand.Next(-99999999, 99999999);
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();

        }

        public int NumOAccts()
        {
            return rand.Next(100000, 999999);
        }
        
    }
}

