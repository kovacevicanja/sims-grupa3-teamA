using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../Resources/Data/users.csv";

        private Serializer<User> _serializer;

        public List<User> _users;

        public UserRepository() { }
        public void Initialize()
        {
            _serializer = new Serializer<User>();
            _users = Load();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _users);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (User user in _users)
            {
                if (user.Id > maxId)
                {
                    maxId = user.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(User user)
        {
            user.Id = GenerateId();
            _users.Add(user);
            Save();
        }
        public List<User> GetAll()
        {
            return _users.ToList();
        }
        public User GetByID(int id)
        {
            return _users.Find(user => user.Id == id);
        } 
    }
}
