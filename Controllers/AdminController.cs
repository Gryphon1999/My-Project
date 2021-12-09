using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_App_Clothing_Ecommerce.Models;

namespace Web_App_Clothing_Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ClothingDBEntities db = new ClothingDBEntities();
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(tb_admin admin)
        {
            var admin_details = db.tb_admin.Where(x=>x.Name==admin.Name && x.Password==admin.Password).FirstOrDefault();
            if (admin_details == null)
            {
/*                admin.LoginError = "Wrong username or Password!!";
*/                return RedirectToAction("AdminLogin", admin);
            }
            else
            {
                Session["Admin_Id"] = admin.Admin_Id;
                return View("AdminDashboard");
            }
                
        }
        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(tb_admin value)
        {
            db.tb_admin.Add(value);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("AdminLogin");
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        //show men product table
        public ActionResult MenTable()
        {
            var res = db.tb_men.ToList();
            return View(res);
        }

        //show women product table
        public ActionResult FemalTable()
        {
            var res = db.tb_women.ToList();
            return View(res);
        }

        //show kid product table
        public ActionResult KidTable()
        {
            var res = db.tb_kid.ToList();
            return View(res);
        }

        //add men product
        public ActionResult add_men_product()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add_men_product(tb_men value)
        {
            string fileName = Path.GetFileNameWithoutExtension(value.imageFile.FileName);
            string extenson = Path.GetExtension(value.imageFile.FileName);
            fileName = fileName + System.DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"),fileName);
            value.image = fileName;
            value.imageFile.SaveAs(file);
            db.tb_men.Add(value);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        //add women product
        public ActionResult add_women_product()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add_women_product(tb_women value)
        {
            string fileName = Path.GetFileNameWithoutExtension(value.imageFile.FileName);
            string extenson = Path.GetExtension(value.imageFile.FileName);
            fileName = fileName + System.DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"), fileName);
            value.image = fileName;
            value.imageFile.SaveAs(file);
            db.tb_women.Add(value);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        //add kid product
        public ActionResult add_kid_product()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add_kid_product(tb_kid value)
        {
            string fileName = Path.GetFileNameWithoutExtension(value.imageFile.FileName);
            string extenson = Path.GetExtension(value.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"), fileName);
            value.image = fileName;
            value.imageFile.SaveAs(file);
            db.tb_kid.Add(value);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        //edit and update men product
        public ActionResult Edit(int id)
        {
            var data = db.tb_men.Where(x => x.men_id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(tb_men obj,HttpPostedFileBase imageName)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageName.FileName);
            string extenson = Path.GetExtension(imageName.FileName);
            fileName = fileName + System.DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"), fileName);
            obj.image = fileName;
            imageName.SaveAs(file);
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            if (obj != null)
            return RedirectToAction("MenTable");
            else
                return View();
        }

        //delete men product
        public ActionResult Delete(int men_id)
        {
            tb_men resp = db.tb_men.Find(men_id);
            db.tb_men.Remove(resp);
            db.SaveChanges();
            var list = db.tb_men.ToList();
            return RedirectToAction("MenTable", list);
        }

        //women product edit and update
        public ActionResult W_Edit(int id)
        {
            var data = db.tb_women.Where(x => x.women_id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult W_Edit(tb_women obj, HttpPostedFileBase imageName)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageName.FileName);
            string extenson = Path.GetExtension(imageName.FileName);
            fileName = fileName + System.DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"), fileName);
            obj.image = fileName;
            imageName.SaveAs(file);
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            if (obj != null)
                return RedirectToAction("FemalTable");
            else
                return View();
        }

        //delete women product
        public ActionResult W_Delete(int women_id)
        {
            tb_women resp = db.tb_women.Find(women_id);
            db.tb_women.Remove(resp);
            db.SaveChanges();
            var list = db.tb_women.ToList();
            return RedirectToAction("FemalTable", list);
        }

        //kid product edit and update
        public ActionResult K_Edit(int id)
        {
            var data = db.tb_kid.Where(x => x.kid_id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult K_Edit(tb_kid obj, HttpPostedFileBase imageName)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageName.FileName);
            string extenson = Path.GetExtension(imageName.FileName);
            fileName = fileName + System.DateTime.Now.ToString("yymmssfff") + extenson;
            string file = Path.Combine(Server.MapPath("~/Image/"), fileName);
            obj.image = fileName;
            imageName.SaveAs(file);
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            if (obj != null)
                return RedirectToAction("KidTable");
            else
                return View();
        }

        //delete women product
        public ActionResult K_Delete(int kid_id)
        {
            tb_kid resp = db.tb_kid.Find(kid_id);
            db.tb_kid.Remove(resp);
            db.SaveChanges();
            var list = db.tb_kid.ToList();
            return RedirectToAction("KidTable", list);
        }
    }
}