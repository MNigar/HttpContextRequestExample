using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AboutHttpContextRequest.Models;
using AboutHttpContextRequest.Models.Db;
using Microsoft.AspNetCore.Http;

namespace AboutHttpContextRequest.Controllers
{
    public class UsersController : Controller
    {
        private RequestExample db = new RequestExample();

        // GET: Users
        public ActionResult Index()
        {
            var path = Request.FilePath;
            var method = Request.HttpMethod;
            var count = Request.Headers.Count;
            string userAgent = HttpContext.Request.UserAgent;
            string browser = HttpContext.Request.Browser.Browser;
            string userHostAddress = HttpContext.Request.UserHostAddress;
            string rawUrl = HttpContext.Request.RawUrl;
            HttpCookie cookie = new HttpCookie("username");
            HttpContext.Response.Cookies.Add(cookie);
            cookie.Value = "Nigar";
            var userId = HttpContext.Request.Cookies[cookie.Name];


            return Content(
                "<p>Path: " + path + "</p>" +
                "<p>Method: " + method + "</p>" +
                "<p>Count: " + count + "</p>" +
               
                "<p>UserAgent: " + userAgent + "</p>" +
                "<p>Url : " + rawUrl +
                "</p>" +
               
                "<p>IpAdress: " + userHostAddress + "</p>"+
                "<p>Browser: " + browser + "</p>" +
                "<p>Username: " + userId.Value + "</p>");
           
        }
        
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
