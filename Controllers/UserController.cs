using AutoMapper;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IMapper _autoMapper;
        public UserController(IUserRepository userRepository, IHttpContextAccessor contextAccessor, IMapper autoMapper)
        {
            _userRepository = userRepository;
            _httpcontextAccessor = contextAccessor;
            _autoMapper = autoMapper;
        }
        [HttpGet]
        public IActionResult CreateAccountForAdmin()
        {
            return View();
        }
        // hien thị view đnag kí tài khoản cho nguwoif dùng
        [HttpGet]
        public IActionResult RegisterAccountForUser()
        {
            return View();
        }
        // hien thi view login
        [HttpGet]
        public IActionResult LoginViewModel()
        {
            return View();
        }

        // trang chao mung sau khi login thanh cong
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
        // dang ki tai khoan cho nguoi dung => tu set role = user
        [HttpPost]
       public IActionResult RegisterForUser(RegisterAccountForUserViewModel userViewModel)
        {
            _userRepository.RegisterAccountForUser(userViewModel);
            TempData["Username"] = userViewModel.Username;
            return RedirectToAction("LoginViewModel"); 
         } 
        // 
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
                    _httpcontextAccessor.HttpContext.Session.SetString("Role", user.Role.ToString());
                    _httpcontextAccessor.HttpContext.Session.SetString("UserID", user.UserID.ToString());
					_httpcontextAccessor.HttpContext.Session.SetString("IsLoggedIn", "true"); // Lưu trạng thái đăng nhập
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
            _httpcontextAccessor.HttpContext.Session.SetString("IsLoggedIn", "false"); // Lưu trạng thái đăng nhập
            
            return Ok(new { mesasge = "Logout successful" });
        }

        // hien thi danh sach tat ca ngươi dùng trong hệ thống
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userModels = await _userRepository.GetAllUsers();
            var userDTO = _autoMapper.Map<IEnumerable<UserDTOModel>>(userModels);
            return View(userDTO);
        }
        // hien thị profile người dùng đăng nhập
        public async Task<IActionResult> ProfileUser(int id)
        {
            var user = _userRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _autoMapper.Map<UserDTOModel>(user);
            return View(userDto);
        }
        // thoát khỏi phiên lm việc, kh cho phép back lại
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
