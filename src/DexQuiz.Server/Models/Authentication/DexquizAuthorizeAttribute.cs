using DexQuiz.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Authentication
{
    public class DexquizAuthorizeAttribute : AuthorizeAttribute
    {
        private UserType roleEnum;
        public UserType RoleEnum
        {
            get { return roleEnum; }
            set { roleEnum = value; Roles = value.ToString(); }
        }

    }
}
