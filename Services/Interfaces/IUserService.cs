using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IUserService
    {
        User GetByUsername(string username);
       
        User GetLoggedUser();
    }
}
