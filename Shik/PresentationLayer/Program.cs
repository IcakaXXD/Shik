using Business_Layer;
using DataLayer;
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
        static Dictionary<int, IManager> dic = new Dictionary<int, IManager>();
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

                   
                    dic.Add(1, customerManager);
                    dic.Add(2, orderManager);
                    dic.Add(3, clothesManager);
                    dic.Add(4, couponManager);
                    dic.Add(5, shippingManager);

                    ShowMenu();
                    int choice = MakeChoice();

                    switch (choice)
                    {
                        case 1: await ProcessCustomerOperation(); break;
                        case 2: await ProcessOrderOperation(); break;
                        // case 3: await ProcessClothesOperation(); break;
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

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Customer customer = new Customer(name,password);
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
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Password: {customer.Password}");
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
            bool exitOrderOperationFlag = false;
            do
            {
                try
                {
                    ShowSubMenu("Orders");
                    int choice = MakeChoice();

                    switch (choice)
                    {
                        case 1: await CreateOrder(); break;
                        case 2: await ReadOrder(); break;
                        case 3: await ReadAllOrders(); break;
                        case 4: await UpdateOrder(); break;
                        case 5: await DeleteOrder(); break;
                        case 0: exitOrderOperationFlag = true; break;
                        default:
                            Console.WriteLine("Enter choice between 0 and 5!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                ClearMenu(exitOrderOperationFlag);

            } while (!exitOrderOperationFlag);
        }

        static async Task CreateOrder()
        {
            Console.Write("Customer ID: ");
            int CustomerID = int.Parse(Console.ReadLine());

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Shipping ID: ");
            int ShippingId = int.Parse(Console.ReadLine());

            Order Order = new Order(address,await customerManager.ReadAsync(CustomerID), await shippingManager.ReadAsync(ShippingId));
            await orderManager.CreateAsync(Order);
            Console.WriteLine("Order created successfully!");
        }

        static async Task ReadOrder()
        {
            Console.Write("Enter Order ID: ");
            int OrderId = int.Parse(Console.ReadLine());

            Order Order = await orderManager.ReadAsync(OrderId);
            Console.WriteLine($"Order details:\n Address: {Order.Address}");
        }

        static async Task ReadAllOrders()
        {
            var Orders = await orderManager.ReadAllAsync();
            Console.WriteLine("All Orders:");
            foreach (var Order in Orders)
            {
                Console.WriteLine($"ID: {Order.Id}, Address: {Order.Address}, Customer name: {(await customerManager.ReadAsync(Order.CustomerId)).Name}");
            }
        }

        static async Task UpdateOrder()
        {
            Console.Write("Enter Order ID: ");
            int OrderId = int.Parse(Console.ReadLine());

            Order Order = await orderManager.ReadAsync(OrderId);

            Console.Write("New address: ");
            string newAddress = Console.ReadLine();

            Order.Address = newAddress;

            await orderManager.UpdateAsync(Order);
            Console.WriteLine("Order updated successfully!");
        }

        static async Task DeleteOrder()
        {
            Console.Write("Enter Order ID: ");
            int OrderId = int.Parse(Console.ReadLine());

            await orderManager.DeleteAsync(OrderId);
            Console.WriteLine("Order deleted successfully!");
        }

        static async Task ProcessClothesOperation()
        {
            bool exitClothesOperationFlag = false;
            do
            {
                try
                {
                    ShowSubMenu("Clothess");
                    int choice = MakeChoice();

                    switch (choice)
                    {
                        case 1: await CreateClothes(); break;
                        case 2: await ReadClothes(); break;
                        case 3: await ReadAllClothess(); break;
                        case 4: await UpdateClothes(); break;
                        case 5: await DeleteClothes(); break;
                        case 0: exitClothesOperationFlag = true; break;
                        default:
                            Console.WriteLine("Enter choice between 0 and 5!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                ClearMenu(exitClothesOperationFlag);

            } while (!exitClothesOperationFlag);
        }

        static async Task CreateClothes()
        {

            Console.Write("Address: ");
            string colour = Console.ReadLine();
            Console.Write("Address: ");
            string name = Console.ReadLine();
            Console.Write("Address: ");
            string description = Console.ReadLine();
            Console.Write("Address: ");
            string price = Console.ReadLine();
            Clothes Clothes = new Clothes();
            await clothesManager.CreateAsync(Clothes);
            Console.WriteLine("Clothes created successfully!");
        }

        static async Task ReadClothes()
        {
            Console.Write("Enter Clothes ID: ");
            int ClothesId = int.Parse(Console.ReadLine());

            Clothes Clothes = await clothesManager.ReadAsync(ClothesId);
            Console.WriteLine($"Clothes details:\n Address: {Clothes.Address}");
        }

        static async Task ReadAllClothess()
        {
            var Clothess = await clothesManager.ReadAllAsync();
            Console.WriteLine("All Clothess:");
            foreach (var Clothes in Clothess)
            {
                Console.WriteLine($"ID: {Clothes.Id}, Address: {Clothes.Address}, Customer name: {(await customerManager.ReadAsync(Clothes.CustomerId)).Name}");
            }
        }

        static async Task UpdateClothes()
        {
            Console.Write("Enter Clothes ID: ");
            int ClothesId = int.Parse(Console.ReadLine());

            Clothes Clothes = await clothesManager.ReadAsync(ClothesId);

            Console.Write("New address: ");
            string newAddress = Console.ReadLine();

            Clothes.Address = newAddress;

            await clothesManager.UpdateAsync(Clothes);
            Console.WriteLine("Clothes updated successfully!");
        }

        static async Task DeleteClothes()
        {
            Console.Write("Enter Clothes ID: ");
            int ClothesId = int.Parse(Console.ReadLine());

            await clothesManager.DeleteAsync(ClothesId);
            Console.WriteLine("Clothes deleted successfully!");
        }

    }

}



#endregion