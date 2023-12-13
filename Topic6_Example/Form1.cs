using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Topic6_Example
{
    public partial class Form1 : Form
    {
        string enterText = "";
        string enterAccNo, enterPin;
        Boolean inputPermitted, withdrawing, receipt, isAccount;
        Account A1, A2, A3, A4, current;
        List<Account> myAccount;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enterText = "";
            enterAccNo = "";
            enterPin = "";
            inputPermitted = true;
            isAccount = false;
            LoadData();
            //A1 = new Account("10","1111",10000);
            //A2 = new Account("20", "2222", 20000);
            //A3 = new Account("30", "3333",30000);
            //A4 = new Account("40", "9999", 40000);

            if(myAccount == null)
            {
                myAccount = new List<Account>();
                addBasicAccount("20", "2222", 20000);
                addExtendedAccount("30", "3333", 30000, 0.8);
                addExtendedAccount("40", "4444", 40000, 0.5);
            }
        }

        public void LoadData()
        {
            Stream output;
            BinaryFormatter bf = new BinaryFormatter();

            output = File.Open("atmdata.atm", FileMode.OpenOrCreate);

            if(output.Length !=0)
            {
                myAccount = (List<Account>)bf.Deserialize(output);
            }

            output.Close();
        }

        public void saveData()
        {
            Stream output;
            BinaryFormatter bf = new BinaryFormatter();

            output = File.Open("atmdata.atm",FileMode.OpenOrCreate);   
            bf.Serialize(output, myAccount);
            output.Close();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            if (inputPermitted == false)
            {
                return;
            }
            enterText += "0";
            label1.Text = enterText;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (!inputPermitted)
            {
                return;
            }
            enterText += "1";
            label1.Text = enterText;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if(inputPermitted == false)
            {
                return;
            }
            enterText += "2";
            label1.Text = enterText;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (!inputPermitted)
            {
                return;
            }
            enterText += "3";
            label1.Text = enterText;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if(!inputPermitted)
            {
                return;
            }
            enterText += "4";
            label1.Text = enterText;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (!inputPermitted)
            {
                return;
            }
            enterText += "5";
            label1.Text = enterText;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (inputPermitted == false)
            {
                return;
            }
            enterText += "6";
            label1.Text = enterText;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if(! inputPermitted)
            {
                return;
            }
            enterText += "7";
            label1.Text = enterText;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if(inputPermitted == false)
            {
                return;
            }
            enterText += "8";
            label1.Text = enterText;
        }
        
        private void btn9_Click(object sender, EventArgs e)
        {
            if (!inputPermitted)
            {
                return;
            }
            enterText += "9";
            label1.Text = enterText;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(!inputPermitted) {
                return;
            }
            enterText = "";
            label1.Text = enterText;
        }

        private void btnDeny_Click(object sender, EventArgs e)
        {
            if(current != null)
            {
                current = null;
                inputPermitted = true;

            }
            accountLogin();
        }

        //private void addNewAccount(string a, string p, int b)
        //{
        //    Account Acc;
        //    Acc = new Account(a, p, b);
        //    myAccount.Add(Acc);
        //}
            
        private void addBasicAccount(string a, string p, int b)
        {
            Account Acc = new BaseAccount(a, p, b);
            myAccount.Add(Acc);
        }

        private void addExtendedAccount(string a, string p, int b, double i)
        {
            Account Acc = new ExtendedAccount(a,p, b,i);
            myAccount.Add(Acc);
        }

        private Account findAccount(string accNum , string p)
        {
           foreach (Account i in myAccount)
           {
                if (i.CheckPin(accNum, p))
                {
                    return i;
                }
                
           }

           //for (int i = 0; i < myAccount.Count; i++)
           //{
           //     if (myAccount[i].CheckPin(accNum, p));
           //     return myAccount[i];
           //}

            return null;
        }

        private void accountLogin()
        {
            label1.Text = "Please Login Again \n Enter PIN";
            enterText = "";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Boolean permitWithdrawl;
            if (current == null)
            {
                
                if(!isAccount){
                    enterAccNo = enterText;
                    if(enterAccNo.Length == 0)
                    {
                        label1.Text = "Please enter Account No";
                        return;
                    }
                    else
                    {
                        isAccount = true;
                        enterText = "";
                        if (isAccount)
                        {
                            label1.Text = "Enter PIN";
                            return;
                        }
                    }
                }

                enterPin = enterText;
                if(enterPin.Length != 0)
                {
                    enterText = "";
                }
                else
                {
                    return;
                }

                if(enterAccNo.Length > 0 && enterPin.Length > 0)
                {
                    current = findAccount(enterAccNo, enterPin);
                }
            }

            if(current == null)
            {
                label1.Text = "Invalid Accout and Pin";
                enterText = "";
                enterAccNo = "";
                enterPin = "";
                isAccount = false;
                inputPermitted = true;
            }
            else
            {
                if (withdrawing)
                {
                    if(enterText == "")
                    {
                        label1.Text = "Enter Amount";
                        return;
                    }
                    permitWithdrawl = current.withdrawl(int.Parse(enterText));
                    if (permitWithdrawl == true)
                    {
                        label1.Text = "Transaction Successful";
                        saveData();
                        if (receipt)
                        {
                            label1.Text = label1.Text + "\n\n Last transaction\n" + current.getTransaction();
                        }
                    }
                    else
                    {
                        label1.Text = "Insufficient funds";
                    }
                }
                else
                {
                    label1.Text = "Logged in - choose transaction\n\nYou have a " + current.queryType()+" Account";
                    if (current.queryType() == "Extended")
                    {
                        if(current.Timeloaded != 0 && current.Timeloaded % 2 == 0)
                        {
                            current.getInterest();
                        }
                    }
                    current.Timeloaded++;
                    saveData();
                    textBox1.Text = current.AccountNumber + " ACC, " + current.Timeloaded + " times loaded";
                }
                enterText = "";
                withdrawing = false;
                inputPermitted = false;
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            inputPermitted = false;
            if(current == null)
            {
                accountLogin();
                return;
            }

            label1.Text = $"Hello {current.AccountNumber} ! \n \n  Your Balance is {current.Balance}";
            enterText = "";
            inputPermitted = false;
        }

        private void btnwidth_Click(object sender, EventArgs e)
        {

            if (current == null)
            {
                accountLogin();
                return;
            }
            label1.Text = "Enter Amount";
            withdrawing = true;
            receipt = false;
            inputPermitted = true;
            enterText = "";
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            if(current == null)
            {
                accountLogin();
                return;
            }
            label1.Text = "Enter Amount";
            enterText = "";
            inputPermitted = true;
            withdrawing = true;
            receipt = true;

            if(receipt == true)
            {
                current.getTransaction();
            }

        }



    }
}
