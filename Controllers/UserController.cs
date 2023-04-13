using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class UserController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly UserHandler _userHandler;

        public List<User> _users;

        public UserController()
        {
            _userHandler = new UserHandler();
            _users = new List<User>();
            observers = new List<IObserver>();
            Load();
        }
        public User GetByUsername(string username)
        {
            _users = _userHandler.Load();
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void Load()
        {
            _users = _userHandler.Load();
        }

        public void Save()
        {
            _userHandler.Save(_users);
            NotifyObservers();
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
            NotifyObservers();
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetByID(int id)
        {
            return _users.Find(user => user.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
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
