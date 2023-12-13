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
    public class BaseAccount : Account, ISerializable
    {
        private int overdraft;
        private int timeloaded;

        public int OverDraft { get; set; }

        public BaseAccount(string a, string p , int b) :base(a, p, b)
        {
            this.OverDraft = -100;
        }
        public BaseAccount(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.OverDraft = (int)info.GetValue("Overdraft",typeof(int));
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Overdraft", this.OverDraft);
        }
        public override string queryType()
        {
            return "Basic";
        }

        public override bool ValidTran(int amount)
        {
            if(this.Balance-amount>OverDraft)
            {
                return true;
            }
            return false;
        }

    }
}
