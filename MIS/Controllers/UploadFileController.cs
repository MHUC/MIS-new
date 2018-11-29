using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MIS.Models;
using MIS.ViewModels;
using OfficeOpenXml;
using LinqToExcel;
using System.Data.SqlClient;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace MIS.Controllers
{
    public class UploadFileController : Controller
    {

        private ApplicationDbContext _context;

        public UploadFileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: UploadFile
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

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile, FormCollection FC)
        {
         

            string path = Server.MapPath("~/Doc/" + excelfile.FileName);

            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please select a excel file<br>";
                return View("Index");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {

                    try
                    {

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        excelfile.SaveAs(path);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }



                    // Read data from excel file
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<ActualRevenue> actualRevenue = new List<ActualRevenue>();



                    for (int row = 2; row <= range.Rows.Count; row++)
                    {

                        if (Convert.ToInt32(((Excel.Range)range.Cells[row, 1]).Value) != null && ((Excel.Range)range.Cells[row, 2]).Value != null)
                        {
                            ActualRevenue p = new ActualRevenue();
                            p.Amount = Convert.ToInt32(((Excel.Range)range.Cells[row, 1]).Value);
                            p.WeekEndDate = Convert.ToDateTime(((Excel.Range)range.Cells[row, 2]).Value);
                            p.BranchId = Convert.ToByte(FC["Actual.BranchId"]);
                            p.CraetedDate = DateTime.Now;
                            p.CreatedBy = 0;
                            _context.ActualRevenues.Add(p);
                        }

                    }
                    _context.SaveChanges();
                    ViewBag.ListProducts = actualRevenue;
                    return Redirect("Index");
                }
                else
                {
                    ViewBag.Error = "File type is incorrect<br>";
                    return View("Index");
                }

            }
        }



    }
}  
