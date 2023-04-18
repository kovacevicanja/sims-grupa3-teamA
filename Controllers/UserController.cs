using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class UserController 
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = Injector.CreateInstance<IUserService>();
        }
        public User GetByUsername(string username)
        {
            return _userService.GetByUsername(username);
        }
        public User GetLoggedUser()
        {
            return _userService.GetLoggedUser();
        }
        public void Create(User user)
        {
            _userService.Create(user);
        }
        public List<User> GetAll()
        {
            return _userService.GetAll();   
        }
        public User GetByID(int id)
        {
            return _userService.GetByID(id);
        }
        public void Save()
        {
            _userService.Save();
        }
    }
}