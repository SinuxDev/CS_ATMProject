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
    public class SeralizedClass : Account, ISerializable
    {
        private String name;
        private String address;
        private String email;

        public string Name { get;set; }
        public string Address { get;set; }
        public string Email { get;set; }  
        
        public SeralizedClass(string a , string p, int b, string n, string ad , string e):base(a,p,b)
        {
            this.Name = n;
            this.Address = ad;
            this.Email = e;
        }

        public SeralizedClass(SerializationInfo info, StreamingContext context):base(info, context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.Address = (string)info.GetValue("Address", typeof(string));
            this.Email = (string)info.GetValue("Email",typeof(string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Name", this.Name);
            info.AddValue("Address", this.Address);
            info.AddValue ("Email", this.Email);
        }

        public override string queryType()
        {
            return "Seralized Type";
        }

    }
}
