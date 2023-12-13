using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Topic6_Example
{
    [Serializable()]
    public class Account:ISerializable
    {
        private String pin;
        private int balance;
        private String lastTransaction;
        private String accountnumber;
        private int timeloaded;

        public String Pin {  get; set; }
        public int Balance { get; set; }
        public String LastTransaction { get; set; }
        public String AccountNumber { get; set; }
        public int Timeloaded { get; set; }

        public Account (string an,string p,int b)
        {
            this.AccountNumber = an;
            this.Pin = p;
            this.Balance = b;
            this.Timeloaded = 0;
        }

        public Account (SerializationInfo info, StreamingContext context)
        {
            this.Pin = (String)info.GetValue("PIN",typeof(String));
            this.AccountNumber = (String)info.GetValue("Account No",typeof(String));
            this.Balance = (int)info.GetValue("Balance",typeof(int));
            this.LastTransaction = (string)info.GetValue("Last Transaction", typeof(String));
            this.Timeloaded = (int)info.GetValue("Times",typeof (int));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PIN",this.Pin);
            info.AddValue("Account No", this.AccountNumber);
            info.AddValue("Balance", this.Balance);
            info.AddValue("Last Transaction", this.LastTransaction);
            info.AddValue("Times", this.Timeloaded);
        }

        public Boolean CheckPin(String a , String Inputpin)
        {
            if(this.AccountNumber.ToLower().Equals(a.ToLower()) == false)
            {
                return false;
            }

            if(this.Pin.ToLower() == Inputpin.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean withdrawl(int amount)
        {
            if (ValidTran(amount) == false)
            {
                return false;
            }
                this.Balance -= amount;
                this.LastTransaction = $"Opening Balance : {this.Balance+amount} \n Withdraw Amount : {amount}\n Closing Balance : {this.Balance}";
                return true;
        }

        public String getTransaction()
        {
            if (!this.LastTransaction.Equals(""))
            {
                return this.LastTransaction;
            }
            else
            {
                return "no Transaction";
            }
        }

        public virtual bool ValidTran(int amount)
        {
            if(this.Balance > amount)
            {
                return true;
            }
            return false;
        }

        public virtual void getInterest()
        {

        }
        

        public virtual string queryType()
        {
            return "Account";
        }

    }
}
