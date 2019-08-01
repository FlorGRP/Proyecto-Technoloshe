using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollectionJoy.Models;

namespace CollectionJoy.Controllers
{
    public class StocksController : Controller
    {
        private ClothesJoyEntities db = new ClothesJoyEntities();

        //// GET: Stocks
        //public ActionResult Index()
        //{
        //    var stocks = db.Stocks.Include(s => s.Product1).Include(s => s.Size1);
        //    return View(stocks.ToList());
        //}

        public ActionResult Index(string prod)
        {
            var stocks = db.Stocks.Include(s => s.Product1).Include(s => s.Size1);
            if (prod != null)
            {
                stocks = db.Stocks.Where(s => s.Product1.Name.Contains(prod));
            }
            return View(stocks.ToList());
        }

        //// GET: Stocks/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Stock stock = db.Stocks.Find(id);
        //    if (stock == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stock);
        //}

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.Product = new SelectList(db.Products, "ID", "Name");
            ViewBag.Size = new SelectList(db.Sizes, "ID", "Name");
            return View();
        }

        // POST: Stocks/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Product,Size,Quantity")] Stock stock)
        {
            var newst = db.Stocks.Where(n => n.Product == stock.Product && n.Size == stock.Size).FirstOrDefault();
            if (newst == null)
            {
                if (ModelState.IsValid)
                {
                    db.Stocks.Add(stock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Product", "This register already exists");
                }

            }
                ViewBag.Product = new SelectList(db.Products, "ID", "Name", stock.Product);
                ViewBag.Size = new SelectList(db.Sizes, "ID", "Name", stock.Size);
                return View(stock);
                       
        }
    // GET: Stocks/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product = new SelectList(db.Products, "ID", "Name", stock.Product);
            ViewBag.Size = new SelectList(db.Sizes, "ID", "Name", stock.Size);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Product,Size,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product = new SelectList(db.Products, "ID", "Name", stock.Product);
            ViewBag.Size = new SelectList(db.Sizes, "ID", "Name", stock.Size);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
