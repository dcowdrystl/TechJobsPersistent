using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
       /* TODO 1. Set up a private DbContext variable so you can perform CRUD operations on the database.Pass it into a EmployerController constructor.*/
        private JobDbContext context;
        public EmployerController(JobDbContext dbContext)
        {
            this.context = dbContext;
        }
        // GET: /<controller>/
        /* TODO 2. Complete Index() so that it passes all of the Employer objects in the database to the view.*/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        /* TODO 3. Create an instance of AddEmployerViewModel inside of the Add() method and pass the instance into the View() return method.*/
        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        /* TODO 4. Add the appropriate code to ProcessAddEmployerForm() so that it will process form submissions and make sure that 
            only valid Employer objects are being saved to the database*/
        [HttpPost("/Employer/Add")]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };
                context.Employers.Add(newEmployer);
                context.SaveChanges();
                return Redirect("/Employer");
            }
            return View("Add", addEmployerViewModel);
        }

        /* TODO 5. About() currently returns a view with vital information about each employer such as their name and location.
            Make sure that the method is actually passing an Employer object to the view for display*/
        public IActionResult About(int id)
        {
            Employer theEmployer = context.Employers.Single(x => x.Id == id);
            return View(theEmployer);
        }
    }
}
