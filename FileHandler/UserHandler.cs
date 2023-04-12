using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class UserHandler
    {
        private const string FilePath = "../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        public List<User> _users;

        public UserHandler()
        {
            _serializer = new Serializer<User>();
            _users = Load();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}