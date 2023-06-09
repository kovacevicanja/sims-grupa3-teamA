using BookingProject.Domain;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IForumCommentService
    {
        void Initialize();
        void Create(ForumComment comment);
        List<ForumComment> GetAll();
        ForumComment GetById(int id);
        void Save(List<ForumComment> comments);
        void SaveComment();
        List<ForumComment> GetAllForForum(int forumId);
        void Update(ForumComment comment);
    }
}
