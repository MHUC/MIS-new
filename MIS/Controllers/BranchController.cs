using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models;
using MIS.ViewModels;

namespace MIS.Controllers
{
    public class BranchController : Controller
    {

        private ApplicationDbContext _context;

        public BranchController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

      
        //public ActionResult Index(ActualRevenue Actual)
        //{

        //    return View();
        //}


        [HttpPost]
        public ActionResult BranchFilter(ActualRevenue Actual)
        {


            var branch = _context.Branches.ToList();
            var ActualRevenue = _context.ActualRevenues.Where(c => c.BranchId == Actual.BranchId).ToList();

            var viewModel = new AtualRevenueBranchViewModel
            {

                Branches = branch,
                ActualRevenue = ActualRevenue
            };

         //   return View("~/ActualRevenue/GraphView", viewModel);

            return RedirectToAction("GraphView", "ActualRevenue", viewModel);
        }



    }
}