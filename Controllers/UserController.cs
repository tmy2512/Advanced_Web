using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public UserController(IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _httpcontextAccessor = contextAccessor;
        }
        [HttpGet]
        public IActionResult CreateAccountForAdmin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterAccountForUser()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LoginViewModel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WelcomeView()
        {
            ViewBag.Username = _httpcontextAccessor.HttpContext.Session.GetString("Username");
            return View();
        }
        // get user with role= user 
        [HttpGet]
        public async Task<IActionResult> GetUserByRole()
        {
            var users = await _userRepository.GetUsersByRoleAsync();
            return PartialView("Partials/_UserSelectPartial", users);
        }
        [HttpPost]
        public IActionResult RegisterForUser(RegisterAccountForUserViewModel userViewModel)
        {
            _userRepository.RegisterAccountForUser(userViewModel);
            TempData["Username"] = userViewModel.Username;
            return RedirectToAction("Welcome", "User"); 
        }
        [HttpPost]
        public IActionResult CreateForAdmin(CreateAccountForAdminViewModel userViewModel)
        {
            _userRepository.CreateAccountForAdmin(userViewModel);
            TempData["Username"] = userViewModel.Username;
            return RedirectToAction("WelcomeView", "User");
        }
        // Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginViewModel( LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                var user = _userRepository.FindUser(loginModel.Email.Trim(), loginModel.Password.Trim());
                if (user != null)
                {
                    _httpcontextAccessor.HttpContext.Session.SetString("Username", user.Username);
                    // Vi day la TempData nen no chi duoc luu temp, sau khi login ok, ta redirect 
                    // sang WelcomeView nên nó khong được lưu lại nữa.
                    //TempData["Username"] = user.Email; 
                    // Su dụng session de luu
                    return RedirectToAction("WelcomeView", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
                }
            }
            //ViewBag.ErrorMessage = " Sai ten dang nhap hoac password";
            return View();
        }

        // get username from session
        [HttpGet]
        public IActionResult getUserNameSession()
        {
            var username = _httpcontextAccessor.HttpContext.Session.GetString("Username");
            if(username != null)
            {
                return Ok(username);
            }
            return NotFound();
        }
        // handle logout
        [HttpPost]
        public IActionResult Logout()
        {
            _httpcontextAccessor.HttpContext.Session.Remove("Username");
            return Ok(new { mesasge = "Logout successful" });
        }
        public IActionResult SecurePage()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // Kiểm tra trạng thái đăng nhập
            if (string.IsNullOrEmpty(_httpcontextAccessor.HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        // get user với role = user để hiển thị trong modal add task

    }
}
