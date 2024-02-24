using Business_Layer;
using DataLayer;
using MySqlX.XDevAPI.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{

    [TestFixture]
    public class CustomerContextTest
    {
        private CustomerContext cusContext = new CustomerContext(SetupFixture.dbContext);
        private OrderContext ordContext = new OrderContext(SetupFixture.dbContext);

        private Order order1;
        private Customer customer1;
        private Shipping shipping1;

        [SetUp]
        public async void CreateCustomer()
        {
            customer1 = new Customer("Test","Pass");
            await cusContext.CreateAsync(customer1);
            order1 = new Order("address", customer1, shipping1);
            await ordContext.CreateAsync(order1);
            customer1.Orders.Add(order1);
           
        }

        [TearDown]
        public void DropCustomer()
        {
            foreach (Customer item in SetupFixture.dbContext.Customers)
            {
                SetupFixture.dbContext.Customers.Remove(item);
            }
            SetupFixture.dbContext.SaveChanges();
        }

        [Test]
        public async Task Create()
        {

            // Arrange
            Customer newCustomer = new Customer("test", "password");

            // Act
            int customersBefore = SetupFixture.dbContext.Customers.Count();
            await cusContext.CreateAsync(newCustomer);

            // Assert
            int customersAfter = SetupFixture.dbContext.Customers.Count();
            Assert.IsTrue(customersBefore + 1 == customersAfter, "Create() does not work!");


        }

        [Test]
        public async Task Read()
        {
            var readCustomer= await cusContext.ReadAsync(customer1.Id);

            Assert.That(readCustomer, Is.EqualTo(customer1), "Read does not return the same object!");
        }


        [Test]
        public async Task ReadWithNavigationalProperties()
        {
            Customer readCustomer = await cusContext.ReadAsync(order1.Id, true);

            Assert.That(readCustomer.Orders.Contains(order1), "order1 is not in the orders list!");

        }

        [Test]
        public async Task ReadAll()
        {
            List<Customer> customers = (await cusContext.ReadAllAsync()).ToList();

            Assert.That(customers.Count != 0, "ReadAll() does not return customers!");
        }

        [Test]
        public async Task Delete()
        {
            int customersBefore = SetupFixture.dbContext.Customers.Count();

            await cusContext.DeleteAsync(order1.Id);
            int customersAfter = SetupFixture.dbContext.Customers.Count();

            Assert.IsTrue(customersBefore - 1 == customersAfter, "Delete() does not work! 👎🏻");
        }
        
    }
}
