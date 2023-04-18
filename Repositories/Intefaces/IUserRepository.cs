using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IUserRepository
    {
        void Create(User user);
        List<User> GetAll();
        User GetByID(int id);
        void Initialize();
        void Save();
    }
}