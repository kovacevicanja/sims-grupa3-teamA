using BookingProject.Domain;
using BookingProject.Domain.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IForumRepository
    {
        void Initialize();
        List<Forum> GetAll();
        int GenerateId();
        void Create(Forum forum);
        Forum GetById(int id);
        void Save(List<Forum> forums);
        void SaveForum();
    }
}
