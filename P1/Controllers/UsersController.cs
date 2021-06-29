using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModels;
using LogicModels;
using Microsoft.AspNetCore.Http;

namespace P1.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        private readonly ICheckThings _icheckthings;

        public UsersController(Context context, ICheckThings icheckthings)
        {
            _context = context;
            _icheckthings = icheckthings;
        }

        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Users.ToListAsync());
        //}

        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Userid == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, string confirm)
        {
            if (ModelState.IsValid)
            {
                if (_icheckthings.CheckUser(user.Email) == false && user.Password == confirm)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), nameof(CategoryController).Replace("Controller",""));
                }
                else if(user.Password != confirm)
                {
                    ModelState.AddModelError(nameof(user.Password), "The Passwords don't match.");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError(nameof(user.Email), "There is already an account registered with that email. Log in instead?");
                    return View(user);
                }
            }
            return View(user);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (_icheckthings.CheckUser(user.Email) == true)
                {
                    HttpContext.Session.SetInt32("UserID",user.Userid);
                    //using HandlingSessionInAsp.NetMVCCore.Models;
                    //something = HttpContext.Session.GetString("UserID");
                    return RedirectToAction(nameof(Index), nameof(CategoryController).Replace("Controller", ""));
                }
                else
                {
                    ModelState.AddModelError(nameof(user.Password), "Incorrect user name or password.");
                    return View();
                }
            }
            return View();
        }

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Userid,Firstname,Lastname,Password,Defaultstore,Location")] User user)
        //{
        //    if (id != user.Userid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Userid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Userid == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Userid == id);
        //}
    }
}
