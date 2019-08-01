using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CollectionJoy.Models;

namespace CollectionJoy.Controllers
{
    public class ProductsController : Controller
    {
        private ClothesJoyEntities db = new ClothesJoyEntities();


        //GET: Product
        public ActionResult Index(int? Category)
        {
            var products = db.Products.Include(p => p.Category1);
            if ( Category != null)
            {
                products = products.Where(p => p.Category1.ID == Category);
            }
            ViewBag.Category = new SelectList(db.Categories, "ID", "Name");
            return View(products.ToList());
        }

        ////GET: Product
        //public ActionResult Index(string id)
        //{
        //    var products = db.Products.Include(p => p.Category1);
        //    if (id != null)
        //    {
        //        products = products.Where(p => p.Category1.Name == id);
        //    }
        //    ViewBag.Category = new SelectList(db.Categories, "ID", "Name");
        //    return View(products.ToList());
        //}

        //// GET: Products/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Category,Image,ImageMimeType")] Product product)
        {
            HttpPostedFileBase filebase = Request.Files[0];

            if (filebase.ContentLength == 0)
            {
                ModelState.AddModelError("Image", "Required field. Please, select file");
            }
            else
            {
                if (filebase.FileName.EndsWith(".jpg"))
                {
                    WebImage imagen = new WebImage(filebase.InputStream);
                    product.Image = imagen.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("Image", "Invalid value");
                }
            }


            var obj = db.Products.Where(o => o.Name == product.Name).FirstOrDefault();
            if (obj == null)
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "This product already exists");
                }
                
            }
            ViewBag.Category = new SelectList(db.Categories, "ID", "Name", product.Category);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Categories, "ID", "Name", product.Category);
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Category,Image,ImageMimeType")] Product product)
        {
            //byte[] imagenActual = null;
            Product _product = new Product();

            HttpPostedFileBase filebase = Request.Files[0];

            if (filebase.ContentLength == 0)
            {
                _product = db.Products.Find(product.ID);
                product.Image = _product.Image;
            }
            else
            {
                if (filebase.FileName.EndsWith(".jpg"))
                {
                    WebImage imagen = new WebImage(filebase.InputStream);
                    product.Image = imagen.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("Image", "Invalid value");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(_product).State = EntityState.Detached;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Categories, "ID", "Name", product.Category);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult getImage(int id)
        {
            Product productsk = db.Products.Find(id);
            byte[] byteImage = productsk.Image;
            MemoryStream memoryStream = new MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);
            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);

            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
                 
               
        }


    }
}
