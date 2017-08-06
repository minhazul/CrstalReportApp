using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalReportApp.Models;
using CrystalReportApp.Reports;

namespace CrystalReportApp.Controllers
{
    public class ProductController : Controller
    {
       private ModelMyDemoEntities mde=new ModelMyDemoEntities();
        public ActionResult Index()
        {
            ViewBag.ListProducts = mde.Products.ToList();
            return View();
        }

        public ActionResult Export()
        {
            ReportDocument rd=new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportProduct.rpt")));
            rd.SetDataSource(mde.Products.Select(p=>new
            {
                Id=p.Id,
                Name=p.Name,
                Price=p.Price.Value,
                Quantity=p.Quantity.Value
            }).ToList());

            Response.Buffer = false;            
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "ListProducts.pdf");

        }
	}
}