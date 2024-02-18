using Business_Layer;
using ServiceLayer;

namespace PresentationLayer
{
    class Program
    {
        static CustomerManager customerManager;
        static OrderManager orderManager;
        static ClothesManager clothesManager;
        static CouponManager couponManager;
        static ShippingManager shippingManager;

        static async Task Main(string[] args)
        {
            bool exitFlag = false;
            do
            {
                try
                {
                    customerManager = new CustomerManager(ContextHelper.GetCustomerContext());
                    orderManager = new OrderManager(ContextHelper.GetOrderContext());
                    clothesManager = new ClothesManager(ContextHelper.GetClothesContext());
                    couponManager = new CouponManager(ContextHelper.GetCouponContext());
                    shippingManager = new ShippingManager(ContextHelper.GetShippingContext());

                    ShowMenu();
                    int choice = MakeChoice();

                    switch (choice)
                    {
                        case 1: await ProcessCustomerOperation(); break;
                        case 2: await ProcessOrderOperation(); break;
                        //case 3: await ProcessClothesOperation(); break;
                        //case 4: await ProcessCouponOperation(); break;
                        //case 5: await ProcessShippingOperation(); break;
                        case 0: exitFlag = true; break;
                        default:
                            Console.WriteLine("Choose between 0 and 5!");
                            ClearMenu();
                            continue;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ClearMenu();
                    continue;
                }

                ClearMenu(true);

            } while (!exitFlag);
        }

        #region Menus

        static void ShowMenu()
        {
            Console.WriteLine("1) Customers");
            Console.WriteLine("2) Orders");
            Console.WriteLine("3) Clothes");
            Console.WriteLine("4) Coupons");
            Console.WriteLine("5) Shipping");
            Console.WriteLine("0) Exit");
            Console.Write("Choose your option: ");
        }

        static void ShowSubMenu(string data)
        {
            Console.WriteLine(data);
            Console.WriteLine("1) Create");
            Console.WriteLine("2) Read");
            Console.WriteLine("3) Read all");
            Console.WriteLine("4) Update");
            Console.WriteLine("5) Delete");
            Console.WriteLine("0) Back");

            Console.WriteLine("Choose your option: ");
        }

        static void ClearMenu(bool backToMenu = false)
        {
            if (!backToMenu)
            {
                Console.WriteLine("Press any key to clear the console!");
                Console.ReadKey();
            }

            Console.Clear();
        }

        static int MakeChoice()
        {
            return int.Parse(Console.ReadLine());
        }

        #endregion

        #region Customers

        static async Task ProcessCustomerOperation()
        {
            bool exitCustomerOperationFlag = false;
            do
            {
                try
                {
                    ShowSubMenu("Customers");
                    int choice = MakeChoice();

                    switch (choice)
                    {
                        case 1: await CreateCustomer(); break;
                        case 2: await ReadCustomer(); break;
                        case 3: await ReadAllCustomers(); break;
                        case 4: await UpdateCustomer(); break;
                        case 5: await DeleteCustomer(); break;
                        case 0: exitCustomerOperationFlag = true; break;
                        default:
                            Console.WriteLine("Enter choice between 0 and 5!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                ClearMenu(exitCustomerOperationFlag);

            } while (!exitCustomerOperationFlag);
        }

        static async Task CreateCustomer()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Customer customer = new Customer(name);
            await customerManager.CreateAsync(customer);
            Console.WriteLine("Customer created successfully!");
        }

        static async Task ReadCustomer()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Customer customer = await customerManager.ReadAsync(customerId);
            Console.WriteLine($"Customer details:\n Name: {customer.Name}");
        }

        static async Task ReadAllCustomers()
        {
            var customers = await customerManager.ReadAllAsync();
            Console.WriteLine("All customers:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Name: {customer.Name}");
            }
        }

        static async Task UpdateCustomer()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Customer customer = await customerManager.ReadAsync(customerId);

            Console.Write("New name: ");
            string newName = Console.ReadLine();

            customer.Name = newName;

            await customerManager.UpdateAsync(customer);
            Console.WriteLine("Customer updated successfully!");
        }

        static async Task DeleteCustomer()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            await customerManager.DeleteAsync(customerId);
            Console.WriteLine("Customer deleted successfully!");
        }

        #endregion

        #region Orders

        static async Task ProcessOrderOperation()
        {
        }


    }
}
#endregion