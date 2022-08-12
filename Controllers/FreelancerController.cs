using HiredHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiredHunters.Controllers
{
    public class FreelancerController : Controller
    {
        // GET: Freelancer
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
        public ActionResult SignUP([Bind(Include = "FirstName,LastName,Email,username,PhoneNumber,pass,ConfirmPassword")] Freelencer user)
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
                #region //UserName alreaduy exist
                if(IsUserExists(user.username))
                {
                    ModelState.AddModelError("userExist", "This user name is already exist");
                    return View(user);
                }
                #endregion
                #region //Unique phone Number
                if (IsNumberExists(user.PhoneNumber))
                {
                    ModelState.AddModelError("numberExist", "This phoone number is already in use");
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
                user.isEmailVarified = 0;

                #region Save to Database
                using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
                {
                    dc.Freelencers.Add(user);
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
                var v = dc.Freelencers.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }
        public bool IsUserExists(string UserName)
        {
            using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
            {
                var v = dc.Freelencers.Where(a => a.username == UserName).FirstOrDefault();
                return v != null;
            }
        }
        public bool IsNumberExists(string number)
        {
            using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
            {
                var v = dc.Freelencers.Where(a => a.PhoneNumber == number).FirstOrDefault();
                return v != null;
            }
        }
    }
}