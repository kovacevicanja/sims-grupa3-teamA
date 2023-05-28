using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class ForumCommentService : IForumCommentService
    {
        private IForumCommentRepository _commentRepository;
        public ForumCommentService() { }
        public void Initialize()
        {
            _commentRepository = Injector.CreateInstance<IForumCommentRepository>();
        }
        public void Create(ForumComment image)
        {
            _commentRepository.Create(image);
        }
        public void Save(List<ForumComment> comments)
        {
            _commentRepository.Save(comments);
        }
        public void SaveComment()
        {
            _commentRepository.SaveComment();
        }
        public List<ForumComment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public ForumComment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }

        public List<ForumComment> GetAllForForum(int forumId)
        {
            List<ForumComment> comm = new List<ForumComment>();
            foreach(ForumComment c in GetAll())
            {
                if (c.Forum.Id == forumId)
                {
                    comm.Add(c);
                }
            }
            return comm;
        }


    }
}
