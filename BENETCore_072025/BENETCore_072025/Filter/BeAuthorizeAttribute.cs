using BENETCore_072025.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security;
using System.Security.Claims;

namespace BENETCore_072025.Filter
{
    public class BeAuthorizeAttribute : TypeFilterAttribute
    {
        public BeAuthorizeAttribute(string functionCode="DEFAULT", string permission="VIEW") : base(typeof(DemoAuthorizeActionFilter))
        {
            Arguments = new object[] { functionCode, permission };
        } 
    }
    public class DemoAuthorizeActionFilter : IAsyncAuthorizationFilter
    { private readonly string _functionCode;
        private readonly string _permission;
        private readonly IAccountService _accountService;
        public DemoAuthorizeActionFilter(string functionCode,string permission,IAccountService accountService)
        {
            _functionCode = functionCode;
            _permission = permission;
            _accountService = accountService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity!=null)
            {
                var userClaims=identity.Claims;
                var userId = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
              //  var userName = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
                if (userId == 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = System.Net.HttpStatusCode.Unauthorized,
                        Message = "Vui lòng đăng nhập để thực hiện chức năng này "
                    });
                    return;

                }
                // check quyền user
                var function = await _accountService.GetFunctionByCode(_functionCode);
                if (function == null|| function.FunctionID<=0) {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = System.Net.HttpStatusCode.Unauthorized,
                        Message = "Chức năng này không hợp lệ"
                    }); ;
                    return;
                }

                var userpermission= await _accountService.GetPermissionByUserID(userId,function.FunctionID);
                if (userpermission == null || userpermission.FunctionID <= 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = System.Net.HttpStatusCode.Unauthorized,
                        Message = "Chức năng này không hợp lệ"
                    }); ;
                    return;
                }

                switch(_permission)
                {
                    case "VIEW":
                        if (userpermission.IsView == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                Code = System.Net.HttpStatusCode.Unauthorized,
                                Message = "Bạn không có quyền thực hiện chức năng này"
                            }); ;
                            return;
                        }


                        break;
                }
            }
        }
    }
}
