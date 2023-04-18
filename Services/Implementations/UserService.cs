using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
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
        private IUserRepository _userRepository;
        public UserService() { }
        public void Initialize()
        {
            _userRepository = Injector.CreateInstance<UserRepository>();
        }
        public User GetByUsername(string username)
        {
            return _userRepository.GetAll().FirstOrDefault(u => u.Username == username);
        }
        public User GetLoggedUser()
        {
            foreach (User user in _userRepository.GetAll())
            {
                if (user.IsLoggedIn == true)
                {
                    return user;
                }
            }
            return _userRepository.GetAll().FirstOrDefault(u => u.IsLoggedIn == true);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByID(int id)
        {
            return _userRepository.GetByID(id);
        }
    }
}