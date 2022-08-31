using HiredHunters.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiredHunters.Controllers
{
    public class RecruiterController : Controller
    {
        // GET: Recruiter
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUP()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUP([Bind(Include = "FirstName,LastName,JoiningDate,Email,pass,ConfirmPassword")] Recruiter user)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion
                #region Generate Activation Code 
                //  user.ActivationCode = Guid.NewGuid();
                #endregion

                #region  Password Hashing 
                user.pass = Crypto.Hash(user.pass);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                //user.isEmailVarified = 0;

                #region Save to Database
                using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
                {
                    user.JoiningDate = DateTime.Now;
                    dc.Recruiters.Add(user);
                    dc.SaveChanges();
                    //Send Email to User
                    //SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    //message = "Registration successfully done. Account activation link " +
                    //    " has been sent to your email id:" + user.EmailID;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
            {
                var v = dc.Recruiters.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }

        //Profile Details
        private string p;
        [HttpGet]
        public ActionResult RecruiterProfile()
        {
            if (Session["r_no"] != null)
            {
                using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
                {

                    Recruiter rec=dc.Recruiters.Find(Session["r_no"]);
                    p = rec.pass;
                    if (rec == null)
                    {
                        return HttpNotFound();
                    }
                    return View(rec);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login_Index","Home");
            }
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecruiterProfile([Bind(Include = "r_no,Recruiter_ID,FirstName,LastName,Profilepic,PhoneNumber,Email,JoiningDate,R_address,Rating,Total_job_Posted")] Recruiter recruiter)
        {
            if(Session["r_no"] != null)
            {
                recruiter.pass = p; 
                if (ModelState.IsValid)
                {
                    using(HiredHuntersEntities1 db = new HiredHuntersEntities1())
                    {
                        db.Entry(recruiter).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    
                }
                return View(recruiter);
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
            
        }
        public ActionResult PostAJob()
        {
            if (Session["r_no"] != null)
            {
                return RedirectToAction("Create", "Jobs");
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
                
        }

        public ActionResult MyJob()
        {
            return View();
        }

        public ActionResult BringATalent()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            if (Session["r_no"] != null)
            {
                Session["r_no"] = null;
                return RedirectToAction("Login_Index", "Home");
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
           // return View();
        }

       
    }
}