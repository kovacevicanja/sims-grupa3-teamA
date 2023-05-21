using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class SuperGuestRepository : ISuperGuestRepository
    {
        private const string FilePath = "../../Resources/Data/superguests.csv";

        private Serializer<SuperGuest> _serializer;

        public List<SuperGuest> _guests;
        public SuperGuestRepository()
        {
            _serializer = new Serializer<SuperGuest>();
            _guests = Load();
        }
        public void Initialize()
        {
            SuperGuestBind();
        }
        public List<SuperGuest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<SuperGuest> guests)
        {
            _serializer.ToCSV(FilePath, guests);
        }
        public List<SuperGuest> GetAll()
        {
            return _guests.ToList();
        }
        public SuperGuest GetById(int id)
        {
            return _guests.Find(g => g.Guest.Id == id);
        }
        public void Create(SuperGuest guest)
        {
            _guests.Add(guest);
            Save(_guests);
        }
        public void Delete(SuperGuest guest)
        {
            List<SuperGuest> _superGuestCopy = new List<SuperGuest>(_guests);

            _guests.Clear();
            foreach (var g in _superGuestCopy)
            {
                if (g.Guest.Id != guest.Guest.Id)
                {
                    _guests.Add(guest);
                }
            }
            _serializer.ToCSV(FilePath, _guests);
        }
        public void Update(SuperGuest guest)
        {
            int index = _guests.FindIndex(u => u.Guest.Id == guest.Guest.Id);
            _guests[index] = guest;
            _serializer.ToCSV(FilePath, _guests);
        }
        public void SuperGuestBind()
        {
            foreach (SuperGuest superGuest in _guests)
            {
                User guest = Injector.CreateInstance<IUserRepository>().GetById(superGuest.Guest.Id);
                superGuest.Guest = guest;
            }
        }
        
    }
}
