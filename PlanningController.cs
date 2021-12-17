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
    [Route("weddings")]
    public class PlanningController : Controller
    {
        private MyContext _context {get;}
        public PlanningController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            foreach(Wedding thisWed in _context.Weddings)
            {
                if(DateTime.Compare(DateTime.Now, thisWed.Date) > 0)
                {
                    _context.Weddings.Remove(thisWed);
                }
            }
            _context.SaveChanges();
            DashboardView ViewModel = new DashboardView
            {
                LoggedInUser = (int)UserId,
                UserName = _context.Users.FirstOrDefault(u => u.UserId == (int)UserId).FirstName,
                AllWeddings = _context.Weddings
                    .Include(w => w.PeopleAttending)
                        .ThenInclude(u => u.RSVPedBy)
                    .ToList()
            };

            return View(ViewModel);
        }
        
        [HttpGet("new")]
        public IActionResult NewWedding()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            return View("NewWedding");
        }

        [HttpPost("create")]
        public IActionResult CreateWedding(Wedding fromForm)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
            {
                fromForm.HostId = (int)UserId;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return NewWedding();
        }

        [HttpGet("{WeddingId}")]
        public IActionResult ViewWedding(int WeddingId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            OneWeddingView ViewModel = new OneWeddingView
            {
                ScheduledWedding = _context.Weddings
                                        .Include(r => r.PeopleAttending)
                                            .ThenInclude(r => r.RSVPedBy)
                                        .FirstOrDefault(w => w.WeddingId == WeddingId)
            };
            if(ViewModel == null){
                return RedirectToAction("Dashboard");
            }
            return View("WeddingInfo", ViewModel);
        }

        [HttpGet("RSVP/{WeddingId}")]
        public IActionResult RSVPToWedding(int WeddingId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            RSVP Attendee = _context.RSVPs
                    .Where(w => w.WeddingId == WeddingId)
                    .FirstOrDefault(u => u.UserId == UserId);
            if(Attendee == null)
            {
                _context.RSVPs.Add(new RSVP{
                    WeddingId = WeddingId,
                    UserId = (int)UserId
                });
            }
            else
            {
                _context.RSVPs.Remove(Attendee);
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("{WeddingId}/delete")]
        public IActionResult Delete(int WeddingId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                RedirectToAction("Index", "Home");
            }
            Wedding wedding = _context.Weddings
                                .FirstOrDefault(w => w.WeddingId == WeddingId);
            _context.Weddings.Remove(wedding);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
    
}