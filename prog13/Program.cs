// See https://aka.ms/new-console-template for more information
using prog13;
using System.Security.Cryptography.X509Certificates;

namespace user
{
    public class PlaceOrder
    {
        public int userid { get; set; }
        float totalvalue = 0;

        //LIST OF LINE ITEMS 
       List<Item> items = new List<Item>();

        //DISPLAY LIST OF ALL ITEMS
        public void displayUnitPrice(List<Item> Ilist)
        {
            Console.WriteLine("\nAVAILABLE ITEMS\n");
            foreach (var item in Ilist)
            {
                Console.WriteLine($"ITEM NAME: {item.Name}\t UNIT PRICE: {item.unitPrice}\tDISCOUNT: {item.discount}");
            }
        }

        //ADD TO CART
        public void Buy(string itemname, List<Item> Ilist, int number)
        {
            foreach(var it in items)
            {
                if(it.Name== itemname)
                {
                    Console.WriteLine("\nITEM ALREADY ADDED TO LIST\n");
                    return;
                }
            }
            foreach (var item in Ilist)
            {
                if (item.Name == itemname)
                {
                    item.Quantity = number;
                    items.Add(item);
                }
            }
        }

        //CART TOTAL
        public void totalValue(int vipdiscount)
        {
            totalvalue = 0;
            foreach (var item in items)
            {
                totalvalue += ((item.Quantity * item.unitPrice) - (item.discount * item.Quantity));
            }
            Console.WriteLine("ORDER DETAILS");
            Console.WriteLine("_____________________");
            Console.WriteLine("\nITEM NAME\tQUANTITY\tUNIT PRICE\tDISCOUNT\tTOTAL\n");
            foreach (var item in items)
            {
               float tot= ((item.Quantity * item.unitPrice) - (item.discount * item.Quantity));
                Console.WriteLine($"{item.Name}\t\t{item.Quantity}\t\t{item.unitPrice}\t\t{item.discount}\t\t{tot}");
            }
            
           
            if(vipdiscount > 0 && totalvalue>20)
            {
                totalvalue -= vipdiscount;
                Console.WriteLine("\nVIP DISCOUNT APPLIED(20)\n");
            }

            Console.WriteLine($"\nOrder Total is :{totalvalue}\n");
        }

        //GENERATE PDF
        public void pdfInvoice()
        {
            
            string path = $"{Environment.CurrentDirectory}\\{userid}";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("\nITEM NAME\tQUANTITY\tUNIT PRICE\tDISCOUNT\tTOTAL\n");
                foreach (var item in items)
                {
                    float tot = ((item.Quantity * item.unitPrice) - (item.discount * item.Quantity));
                    writer.WriteLine($"{item.Name}\t\t{item.Quantity}\t\t{item.unitPrice}\t\t{item.discount}\t\t{tot}");
                }
            }
            Console.WriteLine($"PDF INVOICE GENERATED at {path}");

        }

        //MAKE PAYMENT
        public void makePayment()
        {
            Console.WriteLine("Payment Succesfull");
            Console.WriteLine("Email Send to customer");
            pdfInvoice();

        }



    }
    public class User
    {
       
        public static void Main(string[] args)
        {
            int y,vipDiscount;

            //LIST OF ITEMS
            List<Item> Itemlist = new List<Item> 
            {
             new Item { Name = "Noodle", unitPrice = 40, discount = 10 },new Item { Name = "Chips", unitPrice = 30, discount = 5 }
            };
           
            //LIST OF VIP CUSTOMERS
            List<int> VIPCustomers = new List<int> {1,2,3,4 };
            do
            {
                Console.WriteLine("Enter User ID");
                int uID = Convert.ToInt32(Console.ReadLine());
                vipDiscount = 0;
                foreach (var item in VIPCustomers)
                {
                    if (item == uID)   //CHECKING IF VIP CUSTOMER
                    {
                        Console.WriteLine("\nVIP DISCOUNT APPLICABLE\n");
                        vipDiscount = 20;
                    }
                }

                var user = new PlaceOrder();
                user.userid = uID;

                do
                {
                    
                    user.displayUnitPrice(Itemlist);
                    Console.WriteLine("Enter item name to buy");
                    string itemname = Console.ReadLine();
                    Console.WriteLine("Enter quantity");
                    int qty = Convert.ToInt32(Console.ReadLine());
                    user.Buy(itemname, Itemlist, qty);

                    Console.WriteLine("Do you want to ADD more items(1-yes||0-no)");
                    y = Convert.ToInt32(Console.ReadLine());
                } while (y == 1);
                Console.WriteLine("Available Items are");
                    user.displayUnitPrice(Itemlist);                                                 
                    user.totalValue(vipDiscount);
                    Console.WriteLine("ENTER PAYMENT DETAILS\n");
                    string pay=Console.ReadLine();
                    user.makePayment();                                                                    
                    
                Console.WriteLine("Do you want to enter another user(1-yes||0-no)");
                y=Convert.ToInt32(Console.ReadLine());
            }while(y==1); 
        }
    }
}

