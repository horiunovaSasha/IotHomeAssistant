using System.Collections.Generic;
using System.Threading.Tasks;
using IoTHomeAssistant.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Services
{
    public interface IUserService
    {
        public List<UserViewModel> GetUsersWithRoles();
        public  Task Invite(string email, IUrlHelper url);
    }
}