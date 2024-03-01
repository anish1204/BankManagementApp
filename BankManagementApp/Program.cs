using System;
using System.Collections.Generic;

class Program
{


    public void ShowDB(List<Customer> Customer)
    {
        
    }

    public void WithDraw(List<Customer> Customer, string UserName, string ID)
    {
        Console.WriteLine("Enter the Amount You Would Like to WithDraw");
        String val = Console.ReadLine();
        int amt = Convert.ToInt32(val);

        try
        {
            foreach (var item in Customer)
            {
                if (item.Customer_Name == UserName && item.Customer_ID == ID)
                {
                    Console.WriteLine("Original Amount Was " + item.Amount);
                    if(amt<=item.Amount) 
                    {
                        item.Amount = item.Amount - amt;
                        Console.WriteLine("Final Amount Present Is " + item.Amount);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Balance");
                    }
                    Console.WriteLine("Final Amount Present Is " + item.Amount);
                    UserPage(Customer, UserName, ID);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
    }

    public void Deposits(List<Customer> Customer, string UserName, string ID) {
        Console.WriteLine("Enter the Amount You Would Like to Deposit");
        String val = Console.ReadLine(); 
        int amt = Convert.ToInt32(val);

        try
        {
            foreach (var item in Customer)
            {
                if (item.Customer_Name == UserName && item.Customer_ID == ID)
                {
                    Console.WriteLine("Original Amount Was " + item.Amount);
                    item.Amount = item.Amount + amt;
                    Console.WriteLine("Amount Added SuccessFully");
                    UserPage(Customer, UserName, ID); 
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
     }

    public void UserPage(List<Customer> Customer,string UserName,string ID)
    {
        Console.WriteLine("Hello + ${UserName}");
        Console.WriteLine("What would You like to Do");
        Console.WriteLine("1 : Deposit");
        Console.WriteLine("2 : WithDraw");
        String val = Console.ReadLine();
        switch(val)
        {
            case "1":
                Deposits(Customer, UserName, ID);
                break;
            case "2":
                WithDraw(Customer, UserName, ID); 
                break;
        }
    }


    public void Validate(string UserName,string ID, List<Customer> Customer)
    {
           foreach(var item in Customer)
        {
            if(item.Customer_Name == UserName  && item.Customer_ID == ID) {
                Console.WriteLine("Validation SuccessFull");
                UserPage(Customer, UserName,ID);

            }
            else
            {
                Console.WriteLine("Wrong Details");
                Login(Customer);
            }
        }
    }

    public void InsertData(List<Customer> Customers,string UserName,string ID)
    {
        Customer newData = new Customer
        {
            Customer_Name = UserName,
            Customer_ID = ID,
            Amount = 0
        };
        Customers.Add(newData);
        Console.WriteLine("Data Inserted SuccessFully");
        Console.WriteLine(Customers.Count);
        
    }
    
    public void AccountCreate(List<Customer> Customers)
    {
        Console.WriteLine("/////////////////////");
        Console.WriteLine("CREATE A ACCOUNT ");
        Console.WriteLine("/////////////////////");
        Console.WriteLine("Set a UserName");
        String Username = Console.ReadLine();
        Console.WriteLine("Set a ID");
        String ID = Console.ReadLine();
        InsertData(Customers,Username,ID);

        Login(Customers);

    }

    public void Login(List<Customer>Customer) 
    {
        Console.WriteLine("/////////////////////");
        Console.WriteLine("WELCOME TO LOGIN PAGE");
        Console.WriteLine("/////////////////////");
        Console.WriteLine("Enter UserName");
        String Username = Console.ReadLine();
        Console.WriteLine("Enter ID");
        String ID = Console.ReadLine();
        Validate(Username ,ID,Customer);
        //Gand lagli ahe
         
    }
    public class Customer
    {
        public string Customer_Name { get; set; }
        public string Customer_ID { get; set; }
        public int Amount { get; set; }
    }



    public class Transactions
    {
        public string Transaction_Code { get; set; }
        public string T_Date { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_ID { get; set; }
        public string Amount { get; set; }
    }

  
    
    
    
    
    public void FirstPhase()
    {
        List<Customer> Customers = new List<Customer>();
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
                Login(Customers);
                
                break;
            case "2":
                Console.Clear();
                AccountCreate(Customers);
                break;
            case "3":
                break;
        }
    }


    static void Main(string[] args)
    {

        Program program = new Program();
        Console.WriteLine("Welcome To Banking App");
        program.FirstPhase();

    }


}


