using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class UserController
    {
        private const string FilePath = "../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        public List<User> _users;

        public UserController()
        {
            _serializer = new Serializer<User>();
            _users = Load();
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _users);
        }

        public User GetByID(int id)
        {
            return _users.Find(u => u.Id == id);
        }

        public User GetLoggedUser()
        {
            foreach(User user in _users)
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
