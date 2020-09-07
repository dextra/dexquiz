using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static int? GetLoggedUserId(this ControllerBase controller)
        {
            string userIdStr = controller.User?.FindFirst("userId")?.Value;
            if (int.TryParse(userIdStr, out int userId))
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        public static bool IsLoggedUserAdmin(this ControllerBase controller) =>
            controller.User?.IsInRole("admin") ?? false;
    }
}
