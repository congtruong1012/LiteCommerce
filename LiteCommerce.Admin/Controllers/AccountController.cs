using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Trang quản lý của user
        /// </summary>
        /// <returns></returns>
        /// 
        [Authorize]
        public ActionResult Index()
        {
            WebUserData userData = User.GetUserData();
            Employee data = AccountBLL.GetProfile(userData.UserID);
            return View(data);
        }
        
        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult ChangePwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePwd(string email, string oldpass, string newpass, string repeatpass)
        {
            if (!AccountBLL.Check_Pass(email,EncodeMD5.GetMD5(oldpass)))
            {
                ModelState.AddModelError("errorPass", "Sai mật khẩu");
            }
            if (String.Equals(EncodeMD5.GetMD5(newpass), EncodeMD5.GetMD5(repeatpass)) == false)
            {
                ModelState.AddModelError("newPass", "Mật khẩu mới và nhập lại mật khẩu không khớp");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.oldPass = oldpass;
                ViewBag.newPass = newpass;
                ViewBag.repeatPass = repeatpass;
                return View();
            }
            else
            {
                bool rs = AccountBLL.Change_Pass(email, EncodeMD5.GetMD5(newpass));
                return RedirectToAction("Index", "Dashboard");
            }
            //return Content("OK");
            
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public ActionResult Login(string email = "", string password = "")
        {
            if (Request.HttpMethod == "GET")
            {
                
                return View();
            }
            else
            {
                //bool result = AccountBLL.Login(email, EncodeMD5.GetMD5(password));
                var userAccount = AccountBLL.Athorize(email, EncodeMD5.GetMD5(password), UserAccountTypes.Employee);

                if (userAccount != null)
                {
                    WebUserData cookieData = new Admin.WebUserData()
                    {
                        UserID = userAccount.UserID,
                        FullName = userAccount.FullName,
                        GroupName = userAccount.Role,
                        LoginTime = DateTime.Now,
                        SessionID = Session.SessionID,
                        ClientIP = Request.UserHostAddress,
                        Photo = userAccount.Photo
                    };
                    FormsAuthentication.SetAuthCookie(cookieData.ToCookieString(), false);
                    return RedirectToAction("Index", "Dashboard");

                }
                else
                {
                    ModelState.AddModelError("login_error", "Đăng nhập thất bại");
                    ViewBag.Email = email;
                    ViewBag.Password = password;
                    //ViewBag : Trao đổi dữ liệu
                    return View();
                }

            }

        }

        /// <summary>
        /// Lấy lại mật khẩu
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public ActionResult ForgotPwd()
        {
            return View();
        }
        
        public void SendVerificationLinkEmail(string emailID, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/ResetPassword?email="+emailID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("congtruong1012@gmail.com", "Forgot Password");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "01694958406";
            // link uy quyen https://www.google.com/settings/security/lesssecureapps

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Tai khoan cua ban da duoc tao!";

                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                   " successfully created. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + link + "'>Click here</a> ";
            }
            else
            {
                subject = "Forgot your LiteCommerce password";

                body = "<br/><br/>Click vào link bên dưới để đến trang reset password" +
                   " <br/><br/><a href='" + link + "'>Click here</a> ";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ForgotPwd(string email)
        {
            var emptyEmail = AccountBLL.GetProfile(email);
            if (emptyEmail == null)
            {
                ModelState.AddModelError("checkEmail", "Không tồn tại email");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.email = email;
                return View();
            }
            string resetCode = Guid.NewGuid().ToString();
            SendVerificationLinkEmail(email,  "ResetPassword");

            ViewBag.success = "Đã gửi email. Vui lòng đăng nhập email để thay đổi mật khẩu";
            return View();
            
        }
        public ActionResult ResetPassword(string email="")
        {
            ViewBag.email = email;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(string email = "", string newPassword = "", string confirmPassword = "")
        {
            if(newPassword != confirmPassword)
            {
                ModelState.AddModelError("newPass", "Mật khẩu mới và nhập lại mật khẩu không khớp");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.email = email;
                ViewBag.newPass = newPassword;
                ViewBag.confirmPass = confirmPassword;
                return View();
            }
            var reset = AccountBLL.Change_Pass(email, EncodeMD5.GetMD5(newPassword));
            if (reset)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.email = email;
                ViewBag.newPass = newPassword;
                ViewBag.confirmPass = confirmPassword;
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult ChangeInfo(Employee model, HttpPostedFileBase fileImage = null)
        {
            if (fileImage != null)
            {
                string get = DateTime.Now.ToString("ddMMyyyhhmmss");
                string fileExtension = Path.GetExtension(fileImage.FileName);
                string fileName = get + fileExtension;
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                model.PhotoPath = fileName;
                fileImage.SaveAs(path);
            }
            if (fileImage == null)
            {
                var getEmployee = HumanResourceBLL.Employee_Get(model.EmployeeID);
                model.PhotoPath = getEmployee.PhotoPath;
            }

            bool updateResult = AccountBLL.UpdateProfile(model);
            return RedirectToAction("Index");
            
        }
        public ActionResult CheckEmail(string newEmail = "", string oldEmail = "")
        {
            string rs = "false";
            if (!String.IsNullOrEmpty(newEmail))
            {
                if (HumanResourceBLL.Check_Email(newEmail) && (oldEmail != newEmail))
                {
                    rs = "true";
                }
            }
            return Content(rs);
        }

    }
}