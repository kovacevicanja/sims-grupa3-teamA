using BookingProject.Domain;
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
        void Create(User user);
        List<User> GetAll();
        User GetById(int id);
        void Initialize();
        void Save();
        User IsUserSuperUser(User user);
        void Update(User user);

        void Update2(User user);
    }
}
