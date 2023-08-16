using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services;
using Gamehoax_backend.Services.Interfaces;
using Gamehoax_backend.Viewmodel.Account;
using Gamehoax_backend.Viewmodel.Cart;
using Gamehoax_backend.Viewmodel.Wishlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Gamehoax_backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IEmailService emailService,
                                ICartService cartService,
                                IWishlistService wishlistService,
                                AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _cartService = cartService;
            _wishlistService = wishlistService;
            _context = context;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }

                AppUser user = new()
                {
                    UserName = request.Username,
                    FullName = request.FullName,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(request);
                }


                await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);



                string html = "";
                using (StreamReader reader = new("wwwroot/templates/account.html"))
                {
                    html = reader.ReadToEnd();
                }

                html = html.Replace("{{link}}", link);
                html = html.Replace("{{fullName}}", user.FullName);

                string subject = "Email confirmation";

                _emailService.Send(user.Email, subject, html);

                return RedirectToAction(nameof(VerifyEmail));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(request);
            }
         
        }


        public IActionResult VerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            List<CartVM> cartVMs = new();
            List<WishlistVM> wishlistVMs = new();
            Cart dbCart = await _cartService.GetByUserIdAsync(userId);
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

            if (dbCart is not null)
            {
                List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                foreach (var cartProduct in cartProducts)
                {
                    cartVMs.Add(new CartVM
                    {
                        ProductId = cartProduct.ProductId,
                        Count = cartProduct.Count
                    });
                }

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            }

            if (dbWishlist is not null)
            {
                List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);
                foreach (var wishlistProduct in wishlistProducts)
                {
                    wishlistVMs.Add(new WishlistVM
                    {
                        ProductId = wishlistProduct.ProductId,
                    });
                }

                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);

                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.EmailOrUsername);
                }
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    
                    return View(model);
                }
                else
                {
                    Response.Cookies.Append("user_info", JsonConvert.SerializeObject(user.UserName));
                }
                var res = await _signInManager.PasswordSignInAsync(user, model.Password,false, false);

                if (!res.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }

                List<CartVM> cartVMs = new();
                List<WishlistVM> wishlistVMs = new();


                Cart dbCart = await _cartService.GetByUserIdAsync(user.Id);
                Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(user.Id);


                if (dbCart is not null)
                {
                    List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                    foreach (var cartProduct in cartProducts)
                    {
                        cartVMs.Add(new CartVM
                        {
                            ProductId = cartProduct.ProductId,
                            Count = cartProduct.Count
                        });
                    }

                    Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
                }
                if (dbWishlist is not null)
                {
                    List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);
                    foreach (var wishlistProduct in wishlistProducts)
                    {
                        wishlistVMs.Add(new WishlistVM
                        {
                            ProductId = wishlistProduct.ProductId,
                        });
                    }

                    Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));
                    
                }


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<CartVM> carts = _cartService.GetDatasFromCookie();
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            Cart dbCart = await _cartService.GetByUserIdAsync(userId);
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);
            if (carts.Count > 0)
            {

                if (dbCart == null)
                {
                    dbCart = new()
                    {
                        AppUserId = userId,
                        CartProducts = new List<CartProduct>()
                    };
                    foreach (var cart in carts)
                    {
                        dbCart.CartProducts.Add(new CartProduct()
                        {
                            ProductId = cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    await _context.Carts.AddAsync(dbCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<CartProduct> cartProducts = new List<CartProduct>();
                    foreach (var cart in carts)
                    {
                        cartProducts.Add(new CartProduct()
                        {
                            ProductId = cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    dbCart.CartProducts = cartProducts;

                    //await _context.Carts.AddAsync(dbCart);
                    _context.SaveChanges();

                }
                Response.Cookies.Delete("basket");
            }
            else
            {
                if (dbCart != null)
                    _context.Carts.Remove(dbCart);
            }


            if (wishlists.Count > 0)
            {

                if (dbWishlist == null)
                {
                    dbWishlist = new()
                    {
                        AppUserId = userId,
                        WishlistProducts = new List<WishlistProduct>()
                    };
                    foreach (var wishlist in wishlists)
                    {
                        dbWishlist.WishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = wishlist.ProductId,
                            WishlistId = dbWishlist.Id,
                        });
                    }
                    await _context.Wishlists.AddAsync(dbWishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<WishlistProduct> wishlistProducts = new List<WishlistProduct>();
                    foreach (var wishlist in wishlists)
                    {
                        wishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = wishlist.ProductId,
                            WishlistId = dbWishlist.Id,
                        });
                    }
                    dbWishlist.WishlistProducts = wishlistProducts;
                    //await _context.Wishlists.AddAsync(dbWishlist);
                    _context.SaveChanges();

                }
                Response.Cookies.Delete("wishlist");
            }
            else
            {
                if(dbWishlist != null)
                _context.Wishlists.Remove(dbWishlist);

            }


            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);

            if (existUser == null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme);


            string html = "";
            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{fullName}}", existUser.FullName);

            string subject = "Verify password reset email";

            _emailService.Send(existUser.Email, subject, html);

            return RedirectToAction(nameof(VerifyEmail));
        }


        public IActionResult ResetPassword(string userId, string token)
        {
            var model = new ResetPasswordVM { UserId = userId, Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {

            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }

            AppUser existUser = await _userManager.FindByIdAsync(resetPasswordVM.UserId);

            if (existUser == null)
            {
                return NotFound();
            }
            if (await _userManager.CheckPasswordAsync(existUser, resetPasswordVM.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPasswordVM);
            }


            await _userManager.ResetPasswordAsync(existUser, resetPasswordVM.Token, resetPasswordVM.Password);

            return RedirectToAction(nameof(SignIn));
        }

    }
}
