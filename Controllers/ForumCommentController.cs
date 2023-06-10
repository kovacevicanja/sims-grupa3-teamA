using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class ForumCommentController
    {
        private readonly IForumCommentService _commentService;
        public ForumCommentController()
        {
            _commentService = Injector.CreateInstance<IForumCommentService>();
        }
        public void Create(ForumComment comment)
        {
            _commentService.Create(comment);
        }
        public List<ForumComment> GetAll()
        {
            return _commentService.GetAll();
        }
        public void SaveComment()
        {
            _commentService.SaveComment();
        }
        public ForumComment GetById(int id)
        {
            return _commentService.GetById(id);
        }
       
        public void Save(List<ForumComment> comments)
        {
            _commentService.Save(comments);
        }
        public List<ForumComment> GetAllForForum(int forumId)
        {
            return _commentService.GetAllForForum(forumId);
        }
        public void Update(ForumComment comment)
        {
            _commentService.Update(comment);
        }


    }
}
