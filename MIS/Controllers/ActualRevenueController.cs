using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models;
using MIS.ViewModels;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace MIS.Controllers
{
    public class ActualRevenueController : Controller
    {
        private ApplicationDbContext _context;

        public ActualRevenueController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: ActualRevenue

        public ActionResult Index()
        {

            var branch = _context.Branches.ToList();
            var ActualRevenue = _context.ActualRevenues.Where(c => c.BranchId == 4).ToList();

            var viewModel = new AtualRevenueBranchViewModel
            {

                Branches = branch,
                ActualRevenue = ActualRevenue
            };

            return View(viewModel);

        }


        public ActionResult GraphView()
        {

            var branch = _context.Branches.ToList();
            var ActualRevenue = _context.ActualRevenues.Where(c => c.BranchId == 4).ToList();

            var viewModel = new AtualRevenueBranchViewModel
            {

                Branches = branch,
                ActualRevenue = ActualRevenue
            };

            return View("GraphView",viewModel);

        }

        [HttpPost]
        public ActionResult FilterBranch(ActualRevenue Actual)
        {

           
            var branch = _context.Branches.ToList();
            var ActualRevenue = _context.ActualRevenues.Where(c => c.BranchId == Actual.BranchId).ToList();

            var viewModel = new AtualRevenueBranchViewModel
            {

                Branches = branch,
                ActualRevenue = ActualRevenue
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateActualRevenue(FormCollection Fc)
        {
            foreach (var _key in Fc.Keys)
            {
                var id = Convert.ToInt32(Regex.Replace(_key.ToString(), "Amount_", ""));
                var _value = Fc[_key.ToString()];
                var ActualReveneInDb = _context.ActualRevenues.Single(c => c.Id == id);
                ActualReveneInDb.Amount = Convert.ToInt32(_value);
                

                
            }
            _context.SaveChanges();

            return Redirect("Index");
        }








    }
}