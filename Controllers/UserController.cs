using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_App_Clothing_Ecommerce.Models;

namespace Web_App_Clothing_Ecommerce.Controllers
{
    public class UserController : Controller
    {
        ClothingDBEntities db = new ClothingDBEntities();

        public ActionResult MenProduct()
        {
            Session["User_Id"] = 1;
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["Total"] = x;
            }
            TempData.Keep();
            var res = db.tb_men.ToList();
            return View(res);
        }
        public ActionResult WomenProduct()
        {
            Session["User_Id"] = 1;
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["Total"] = x;
            }
            TempData.Keep();
            var res = db.tb_women.ToList();
            return View(res);
        }
        public ActionResult KidProduct()
        {
            Session["User_Id"] = 1;
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["Total"] = x;
            }
            TempData.Keep();
            var res = db.tb_kid.ToList();
            return View(res);
        }
        public ActionResult Men_Cart(int id)
        {
            var res = db.tb_men.Where(x=>x.men_id==id).SingleOrDefault();
            return View(res);
        }

        List<cart> li = new List<cart>();
        [HttpPost]
        public ActionResult Men_Cart(tb_men value, int qty, int id)
        {
            tb_men p = db.tb_men.Where(x => x.men_id == id).SingleOrDefault();
            cart c = new cart();
            c.poductid = p.men_id;
            c.productName = p.name;
            c.price = (int)p.price;
            c.qty = qty;
            c.bill = c.price * c.qty;
           if (TempData["cart"] ==null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<cart> li2 = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.poductid == c.poductid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                
                TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("MenProduct");
        }
        public ActionResult Women_Cart(int id)
        {
            var res = db.tb_women.Where(x => x.women_id == id).SingleOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Women_Cart(tb_women value, int qty, int id)
        {
            tb_women p = db.tb_women.Where(x => x.women_id == id).SingleOrDefault();
            cart c = new cart();
            c.poductid = p.women_id;
            c.productName = p.name;
            c.price = (int)p.price;
            c.qty = qty;
            c.bill = c.price * c.qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<cart> li2 = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.poductid == c.poductid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }

                TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("WomenProduct");
        }
        public ActionResult Kid_Cart(int id)
        {
            var res = db.tb_kid.Where(x => x.kid_id == id).SingleOrDefault();
            return View(res);
        }

        [HttpPost]
        public ActionResult Kid_Cart(tb_kid value, int qty, int id)
        {
            tb_kid p = db.tb_kid.Where(x => x.kid_id == id).SingleOrDefault();
            cart c = new cart();
            c.poductid = p.kid_id;
            c.productName = p.name;
            c.price = (int)p.price;
            c.qty = qty;
            c.bill = c.price * c.qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<cart> li2 = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.poductid == c.poductid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }

                TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("WomenProduct");
        }
        public ActionResult cart()
        {
            TempData.Keep();
            return View();
        }
        /*[HttpPost]
        public ActionResult cart(tb_order o)
        {
            List<cart> li = TempData["cart"] as List<cart>;
            tb_invoice iv = new tb_invoice();
            iv.User_Id = Convert.ToInt32(Session["User_Id"].ToString());
            iv.in_totalbill = (int)TempData["Total"];
            db.tb_invoice.Add(iv);
            db.SaveChanges();
            foreach (var item in li)
            {
              //  tb_order od = new tb_order();
                od.men_id = item.poductid;
                od.in_id = iv.in_id;
                od.o_date = DateTime.Now;
                od.o_qty = item.qty;
                od.o_bill = item.bill;
              //  db.tb_order.Add(od);
                db.SaveChanges();
            }
            TempData["msg"] = "Transaction Complete.....";
            TempData.Keep();
            return RedirectToAction("MenProduct");
        }*/
        public ActionResult feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult feedback(tb_feedback value)
        {
            db.tb_feedback.Add(value);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("MenProduct");
        }
        public ActionResult remove(int id)
        {
            List<cart> li2 = TempData["cart"] as List<cart>;
            cart c = li2.Where(x => x.poductid == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.bill;
            }
            TempData["Total"] = h;
            return RedirectToAction("cart");
        }
        public ActionResult com_men(string searching)
        {
            return View(db.tb_men.Where(x=>x.name.Contains(searching)).ToList());
        }
        public ActionResult com_women(string searching)
        {
            return View(db.tb_women.Where(x => x.name.Contains(searching)).ToList());
        }
        public ActionResult com_kid(string searching)
        {
            return View(db.tb_kid.Where(x => x.name.Contains(searching)).ToList());
        }
    }
}