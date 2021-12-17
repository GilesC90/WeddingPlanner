using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using System.Linq;
using System;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        public User UserInDB()
        {
            return _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }
        [HttpGet("")]

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost("create")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.Email == user.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                
                    // You may consider returning to the View at this point
                    return View("Index");
                }
                // Initializing a PasswordHasher object, providing our User class as its type
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                //Save your user object to the database
                _context.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", (int)user.UserId);

                return RedirectToAction("Dashboard", "Planning");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");

                    return View("Index");
                }
                else
                {
                    // Initialize hasher object
                    var hasher = new PasswordHasher<LoginUser>();
                
                    // verify provided password against hash stored in db
                    var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);
                
                    // result can be compared to 0 for failure
                    if(result == 0)
                    {
                        // handle failure (this should be similar to how "existing email" is handled)
                        ModelState.AddModelError("LogPassword", "Invalid Email/Password");

                        return View("Index");
                    }
                }
                HttpContext.Session.SetInt32("UserId", (int)userInDb.UserId);
                return RedirectToAction("Dashboard", "Planning");
            }
            return View("Index");
        }
        [HttpGet("logout")]
        public RedirectToActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}