using System;
using System.Collections.Generic;
using System.Transactions;
using System.IO;
using static Program;



class Program
{

    int W = 0;
    int D = 0;






    static void AppendTextToFile(string filePath, string newText)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
            
                sw.WriteLine(newText);
            }
        }
        catch (Exception ex)
        {
           
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    public void AddText(string temp)
    {
        string filePath = "C:\\Users\\Anish\\Desktop\\houses.txt";
       
        AppendTextToFile(filePath, temp);

    }
    private void ShowTransaction(ref List<Transaction>Transactions,ref List<Customer> Customers)
    {
        Console.WriteLine("Tansaction ID  T_Date  Customer Name  Customer_ID ");
        Console.WriteLine("Total transactions : "+Transactions.Count);
        Console.WriteLine("Total WithDrawals : " + W);
        Console.WriteLine("Total Deposits : " + D);
        string Temp="";
        foreach (var item in Transactions)
        {
            Temp = item.Transaction_Code + " " + item.T_Date + " " + item.Customer_Name + " " + item.Customer_ID + " " + item.Amount;
            //Console.WriteLine(Temp);
            Console.WriteLine(item.Transaction_Code + " " + item.T_Date + " " + item.Customer_Name + " " + item.Customer_ID + " " + item.Amount);
        }
        AddText(Temp);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        FirstPhase(ref Customers, ref Transactions);

    }


    public void TransactionPhase( ref List<Transaction> Transactions,string Customer_Name,string Customer_ID,int Amt,Boolean ok)
    {

        Transaction newData = new Transaction
        {
            Transaction_Code = GenerateString(ok),
            T_Date = DateGenerate(),
            Customer_Name = Customer_Name,
            Customer_ID = Customer_ID,
            Amount = Amt
        };
        Transactions.Add(newData);
     
    }
    public string DateGenerate()
    {
        DateTime today = DateTime.Today;
        
        String CurrentDate = today.ToString("yyyy-MM-dd");
       
        return CurrentDate;
    }

    public string GenerateString(Boolean ok)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        if(ok)
        {
            return "D" + finalString;
        }
        return "W"+finalString;
    }

    private void WithDraw(ref List<Customer> Customer, string UserName, string ID,ref List<Transaction> Transactions)
    {
        Console.WriteLine("Enter the Amount You Would Like to WithDraw");
        String val = Console.ReadLine();
        int amt = Convert.ToInt32(val);
        Boolean ok = false;
        try
        {
            foreach (var item in Customer)
            {
                if (item.Customer_Name == UserName && item.Customer_ID == ID)
                {                  
                    if(amt<=item.Amount) 
                    {
                        item.Amount = item.Amount - amt;
                        W++;
                       
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Balance");
                        
                    }                
                    Console.WriteLine();
                    Console.Clear();
                    Console.WriteLine("The Balance is  " + item.Amount);
                    TransactionPhase(ref Transactions, UserName, ID, item.Amount,ok);
                    UserPage(ref Customer, UserName, ID, ref Transactions);
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
    }

    private void Deposits(ref List<Customer> Customer, string UserName, string ID, ref List<Transaction> Transactions) {
        Console.WriteLine("Enter the Amount You Would Like to Deposit");
        String val = Console.ReadLine(); 
        int amt = Convert.ToInt32(val);
        Boolean ok = true;
        try
        {
            foreach (var item in Customer)
            {
                if (item.Customer_Name == UserName && item.Customer_ID == ID)
                {
                    
                    item.Amount = item.Amount + amt;
                    Console.Clear();
                    Console.WriteLine("Amount Added SuccessFully");
                    Console.WriteLine("The Balance is  " + item.Amount);
                    D++;
                    Console.WriteLine();
                    Console.WriteLine();
                    //Console.Clear();
                    TransactionPhase(ref Transactions,UserName, ID, item.Amount, ok);
                    UserPage(ref Customer, UserName, ID,ref Transactions);
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
     }
        
        private Boolean AdminVerify()
        {
        
        Console.WriteLine("Enter UserName");
        String UserName = Console.ReadLine();
        Console.WriteLine("Enter Password");
        String Password = Console.ReadLine();
        if(UserName=="ADMIN" && Password == "ADMIN")
        {
            return true;
        }
        return false;
    }

       public void AdminPage(ref List<Transaction> Transactions, ref List<Customer> Customers)
        {
        Console.WriteLine("Welcome Admin");
        Console.WriteLine("1: Show All Transaction");
        Console.WriteLine("2: Return To Home Page");
        String val = Console.ReadLine();
        switch (val)
        {
            case "1":
                ShowTransaction(ref Transactions,ref Customers);
                break;
            case "2":
                FirstPhase(ref Customers,ref Transactions);
                break;
        }
        }

    public void UserPage(ref List<Customer> Customer,string UserName,string ID,ref List<Transaction> Transactions)
    {
        Console.WriteLine("Hi "+UserName +" What would You like to Do");
        Console.WriteLine();
        
        Console.WriteLine("1 : Deposit");
        Console.WriteLine("2 : WithDraw");
        Console.WriteLine("3 : Return To HomePage");
        String val = Console.ReadLine();
        switch(val)
        {
            case "1":
                Deposits(ref Customer, UserName, ID,ref Transactions);
                break;
            case "2":
                WithDraw(ref Customer, UserName, ID,ref Transactions); 
                break;
            case "3":
                Console.Clear();
                FirstPhase(ref Customer,ref Transactions);
                
                break;

        }
    }


    public Boolean Validate(string UserName,string ID, List<Customer> Customer, string Password, List<Transaction> Transactions)
    {
           foreach(var item in Customer)
        {
            if(item.Customer_Name == UserName  && item.Customer_ID == ID && item.Password== Password) {
                Console.WriteLine("Validation SuccessFull");
                Console.Clear();
                return true;
                

            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void InsertData(ref List<Customer> Customers,string UserName,string ID,string PassWord)
    {
        Customer newData = new Customer
        {
            Customer_Name = UserName,
            Customer_ID = ID,
            Password = PassWord,
            Amount = 0
            
        };
        Customers.Add(newData);
        Console.WriteLine("User Added SuccessFully");
        //Console.WriteLine(Customers.Count);
        Console.Clear();
    }
    
    public void AccountCreate(ref List<Customer> Customers,ref  List<Transaction> Transactions)
    {
        Console.WriteLine("/////////////////////");
        Console.WriteLine("CREATE A ACCOUNT ");
        Console.WriteLine("/////////////////////");
        Console.WriteLine("Set a UserName");
        String Username = Console.ReadLine();
        Console.WriteLine("Set a ID");
        String ID = Console.ReadLine();
        Console.WriteLine("Set a Password");
        String Password = Console.ReadLine();
        if(Username.Length<6 || Password.Length<6 || Username==Password)
        {
            Console.Clear();
            Console.WriteLine("Please Set Justified UserName and Password");
            Console.WriteLine("Make Sure PassWord Length is Minimum 6 Length and UserName and Password is Not Same");
            Console.WriteLine();
            Console.WriteLine();
            AccountCreate(ref Customers, ref Transactions);
        }
        else
        {
            InsertData(ref Customers,Username,ID,Password);
            Console.Clear();
            Login(ref Customers, ref Transactions);
        }

    }

    public void Login(ref List<Customer>Customer, ref  List<Transaction> Transactions) 
    {
        Console.WriteLine(Customer.Count());
        Console.WriteLine("/////////////////////");
        Console.WriteLine("WELCOME TO LOGIN PAGE");
        Console.WriteLine("/////////////////////");
        Console.WriteLine("Enter UserName");
        String Username = Console.ReadLine();
        Console.WriteLine("Enter ID");
        String ID = Console.ReadLine();
        Console.WriteLine("Enter Password");
        String Password = Console.ReadLine();

        if(Validate(Username, ID, Customer, Password, Transactions))
        {
            UserPage(ref Customer, Username, ID,ref Transactions);
        }
        else
        {
            Console.Clear();
            Console.WriteLine(Customer.Count());
            Console.WriteLine("Wrong Details");
            Login(ref Customer,ref Transactions);
        }

       //try{ ; }
        //catch(Exception e) 
        //{ Console.WriteLine("Wrong Details Or user Does Not Exist"); Login(Customer, Transactions); }
       
         
    }
    public class Customer
    {
        public string Customer_Name { get; set; }
        public string Customer_ID { get; set; }
        public string Password { get; set; }
        public int Amount { get; set; }
    }



    public class Transaction
    {
        public string Transaction_Code { get; set; }
        public string T_Date { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_ID { get; set; }
        public int Amount { get; set; }
    }

  
    
    
    
    
    public void FirstPhase(ref List<Customer> Customers, ref List<Transaction> Transactions)
    {
        
        Console.WriteLine("/////////////////////");
        Console.WriteLine("1 : User Login ");
        Console.WriteLine("2 : Create Account");
        Console.WriteLine("3 : Admin Login"); Console.WriteLine();
        Console.WriteLine("/////////////////////");
        String val = Console.ReadLine();
        switch(val)
        {
            case "1":
                Console.Clear();
                Login(ref Customers, ref Transactions);
                
                break;
            case "2":
                Console.Clear();
                AccountCreate(ref Customers, ref Transactions);
                break;
            case "3":
                Console.Clear();
                if(AdminVerify())
                {
                    Console.Clear();
                    AdminPage(ref Transactions, ref Customers);
                }
                else
                {
                    FirstPhase(ref Customers, ref Transactions);
                }
                break;
            
        }
    }




    static void Main(string[] args)
    {
        List<Customer> Customers = new List<Customer>();
        List<Transaction> Transactions = new List<Transaction>();
        Program program = new Program();
        Console.WriteLine("Welcome To Banking App");
        program.FirstPhase(ref Customers,ref Transactions);

    }


}


