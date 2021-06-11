using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ssbbr.Data;

namespace ssbbr.Controllers
{
    public class RegisterFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterFormsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        // GET: RegisterForms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegisterForm.ToListAsync());
        }

        // GET: RegisterForms/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerForm = await _context.RegisterForm
                .FirstOrDefaultAsync(m => m.id == id);
            if (registerForm == null)
            {
                return NotFound();
            }

            return View(registerForm);
        }

        // GET: RegisterForms/Create
        public IActionResult Create(string? res, string? code)
        {
            if (code != null)
                res = res + " کد رهگیری : " + code;
            ViewBag.res = res;

            return View();
        }

        // POST: RegisterForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,Lastname,NationalCode,Mobile,Phone,Fax,Email,Province,City,Resume,Responsibility,Password,Password2")] RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                if (registerForm.Password != registerForm.Password2)
                {
                    return RedirectToAction("Create", new { res = "کلمه رمز را مجددا وارد کنید - همخوانی ندارد" });
                }
                _context.Add(registerForm);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { res = "درخواست شما با موفقیت ثبت گردید.", code = registerForm.id.ToString() });

            }
            return RedirectToAction("Create", new { res = "خطا - مجددا تلاش نمایید." });

        }

        // GET: RegisterForms/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerForm = await _context.RegisterForm.FindAsync(id);
            if (registerForm == null)
            {
                return NotFound();
            }
            return View(registerForm);
        }

        // POST: RegisterForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,Lastname,Email,Province,City,Phone")] RegisterForm registerForm)
        {
            if (id != registerForm.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterFormExists(registerForm.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(registerForm);
        }

        // GET: RegisterForms/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerForm = await _context.RegisterForm
                .FirstOrDefaultAsync(m => m.id == id);
            if (registerForm == null)
            {
                return NotFound();
            }

            return View(registerForm);
        }

        // POST: RegisterForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //RegisterModel rm = new RegisterModel();
            RegisterForm a = _context.RegisterForm.FirstOrDefault(X => X.id == id);

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = a.Email, Email = a.Email };
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, a.Password);
                var createdUser = await _userManager.FindByEmailAsync(user.Email);
                await _userManager.AddToRoleAsync(createdUser, "Government");
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //await _signInManager.SignOutAsync();
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //// If we got this far, something failed, redisplay form
            //return Page();






            var registerForm = await _context.RegisterForm.FindAsync(id);
            _context.RegisterForm.Remove(registerForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterFormExists(int id)
        {
            return _context.RegisterForm.Any(e => e.id == id);
        }
    }
}
