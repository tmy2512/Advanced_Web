namespace ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels
{
    // tạo Middleware để kiểm tra trạng thái đăng nhập của người dùng khi họ truy cập vào các trang yc đăng nhập
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public AuthenticationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Session.GetString("Username")) 
                && !context.Request.Path.Value.Contains("/LoginViewModel")
                && !context.Request.Path.Value.Contains("/RegisterForUser"))
            {
                context.Response.Redirect("/User/LoginViewModel");
                return;
            }
            await _requestDelegate(context);
        }
    }
}
