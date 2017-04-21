using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wall.Models;
using wall.DbConnectors;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace wall.Controllers
{
    public class UsersController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Regerror = new List<string>();
            
            return View();
        }
        
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User newuser)   //(User new User)
        {
            if(ModelState.IsValid){
            string newguy = $"INSERT INTO users(first_name, last_name, email, password, created_at, updated_at) VALUES ('{newuser.FirstName}', '{newuser.LastName}', '{newuser.Email}', '{newuser.Password}',NOW(), NOW()); SELECT * FROM users WHERE email = '{newuser.Email}'";
            Dictionary<string, object> person = DbConnector.Query(newguy).SingleOrDefault();
            
            HttpContext.Session.SetInt32("id",(int)person["userid"]);
            HttpContext.Session.SetString("name",(string)person["first_name"]);
            ViewBag.id = HttpContext.Session.GetString("name");
            
                return RedirectToAction("wall", "Messages");
                 }
             else
             {
                ViewBag.Regerror = ModelState.Values;
             }
            return View("index");
      
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
           string findit = $"SELECT * FROM users WHERE email = '{email}'";
           Dictionary<string, object> person = DbConnector.Query(findit).SingleOrDefault();

     if(person == null)
           {
               ViewBag.Regerror = new List<string>();
               ViewBag.message = "Please enter a valid email/password combo";
                    return View("index");
           }
               
            else if(password == (string)person["password"]){
                    HttpContext.Session.SetInt32("id",(int)person["userid"]);
                    HttpContext.Session.SetString("name",(string)person["first_name"]);
                   
                    ViewBag.id = HttpContext.Session.GetString("name");
                    return RedirectToAction("wall","Messages");
                }
             else{
                 ViewBag.Regerror = new List<string>();
                 ViewBag.message = "Please enter a valid password";
                return View("index");

                   
                }
        }
           [HttpPost]
           [Route("logout")]
           public IActionResult logout(){
                HttpContext.Session.Clear();
           return RedirectToAction("Index");
           }
    }
}
