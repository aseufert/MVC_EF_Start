using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
  public class DatabaseExampleController : Controller
  {
        // instantiation of the entity framework
    public ApplicationDbContext dbContext;

        // this is a constructor. has same name as class
        // sets connection of entity framework to dbcontext
    public DatabaseExampleController(ApplicationDbContext context)
    {
      dbContext = context;
    }

    public IActionResult Index()
        {
            Customer customer1 = new Customer();
            customer1.Name = "Alex Seufert";

            Customer customer2 = new Customer();
            customer2.Name = "Bruce Wayne";

            Item item1 = new Item();
            item1.ItemName = "Frisbee";

            Item item2 = new Item();
            item2.ItemName = "Batarang";

            Order order1 = new Order();
            order1.Customer = customer1;
            order1.Item = item1;
            order1.TotalPaid = 4.50;

            Order order2 = new Order();
            order2.Customer = customer2;
            order2.Item = item2;
            order2.TotalPaid = 8.75;

            Order order3 = new Order();
            order3.Customer = customer2;
            order3.Item = item1;
            order3.TotalPaid = 5.34;

            dbContext.Customers.Add(customer1);
            dbContext.Customers.Add(customer2);

            dbContext.Items.Add(item1);
            dbContext.Items.Add(item2);

            dbContext.Orders.Add(order1);
            dbContext.Orders.Add(order2);
            dbContext.Orders.Add(order3);

            dbContext.SaveChanges();
            return View();
    }

    public async Task<ViewResult> DatabaseOperations()
    {

        // Features include which includes Orders associated with each Customer
        Customer CustomerRead1 = dbContext.Customers
                              .Include(c => c.Orders)
                              .ThenInclude(o => o.Item)
                              .Where(c => c.Name == "Bruce Wayne")
                              .First();

      return View(CustomerRead1);
    }
  }
}