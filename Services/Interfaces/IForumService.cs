using BookingProject.Domain;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IForumService
    {
        void Initialize();
        void Create(Forum forum);
        List<Forum> GetAll();
        Forum GetById(int id);
        void Save(List<Forum> forums);
        void SaveForum();
        void UpdateForum(Forum forum);
        List<ForumComment> GetCommentsForForum(Forum forum);
        void SetVeryHelpful(Forum forum);
    }
}
