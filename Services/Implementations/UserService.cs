using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repository;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class UserService : IUserService
    {
        private List<User> _users;
        private UserRepository _userRepository;
        public UserService() 
        { 
            _users = new List<User>();
            _userRepository = new UserRepository();
            Load();
        }
        public void Load()
        {
            _users = _userRepository.Load();
        }

        public User GetByUsername(string username)
        {
            _users = _userRepository.Load(); 
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public User GetLoggedUser()
        {
            foreach (User user in _users)
            {
                if (user.IsLoggedIn == true)
                {
                    return user;
                }
            }
            return _users.FirstOrDefault(u => u.IsLoggedIn == true);
        }
    }
}