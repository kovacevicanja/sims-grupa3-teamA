using BookingProject.Domain;
using BookingProject.Domain.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IForumCommentRepository
    {
        void Initialize();
        List<ForumComment> GetAll();
        int GenerateId();
        void Create(ForumComment comment);
        ForumComment GetById(int id);
        void Save(List<ForumComment> comments);
        void SaveComment();
    }
}
