using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models;
using MIS.ViewModels;
using System.Data.Entity;

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
    }
}