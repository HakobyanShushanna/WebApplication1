using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly WebApplication1Context _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly PasswordHasher<UserModel> _passwordHasher;

        public UserController(WebApplication1Context context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, PasswordHasher<UserModel> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }


        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserModel.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var model = await _context.UserModel.FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or Password.");
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found.");
            }

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Address,Id,Firstname,Lastname,Email,Password")] UserModel userModel, string password)
        {
            userModel.LockoutEnabled = false;
            userModel.NormalizedEmail = _userManager.NormalizeEmail(userModel.Email);
            userModel.NormalizedUserName = userModel.Email;
            userModel.PasswordHash = _passwordHasher.HashPassword(userModel, password);



            if (ModelState.IsValid)
            {
                var user = new UserModel
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                    Firstname = userModel.Firstname,
                    Lastname = userModel.Lastname,
                    Age = userModel.Age,
                    Address = userModel.Address,
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Age,Address,Id,Firstname,Lastname,Email,Password")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.UserModel.FindAsync(id);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.Firstname = userModel.Firstname;
                    existingUser.Lastname = userModel.Lastname;
                    existingUser.Email = userModel.Email;
                    existingUser.Age = userModel.Age;
                    existingUser.Address = userModel.Address;

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(id))
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

            return View(userModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel != null)
            {
                _context.UserModel.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(string id)
        {
            return _context.UserModel.Any(e => e.Id == id);
        }

        private UserModel? SearchByEmail(string email)
        {
            return _context.UserModel.FirstOrDefault(e => e.Email == email);
        }
    }
}
