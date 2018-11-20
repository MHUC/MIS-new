using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MIS.Models;
using OfficeOpenXml;
using LinqToExcel;
using System.Data.SqlClient;
using System.Globalization;

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
            return View();
        }

     //   [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var ActualRevenue = new List<ActualRevenue>();

                  


                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
 

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {

                            if (workSheet.Cells[rowIterator, 1].Value != null)
                            {
                                double d = double.Parse(workSheet.Cells[rowIterator, 2].Value.ToString());
                                DateTime conv = DateTime.FromOADate(d);


                                var actualRevenue = new ActualRevenue();
                                actualRevenue.Amount = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                                actualRevenue.WeekEndDate = Convert.ToDateTime(double.Parse(workSheet.Cells[rowIterator, 2].Value.ToString()));


                                _context.ActualRevenues.Add(actualRevenue);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }


       
    }
}  
