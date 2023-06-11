using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Services.Implementations;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingProject.Controllers
{
    public class ForumController
    {
        private readonly IForumService _forumService;
        public ForumController()
        {
            _forumService = Injector.CreateInstance<IForumService>();
        }
        public void Create(Forum forum)
        {
            _forumService.Create(forum);
        }
        public List<Forum> GetAll()
        {
            return _forumService.GetAll();
        }
        public void SaveImage()
        {
            _forumService.SaveForum();
        }
        public Forum GetById(int id)
        {
            return _forumService.GetById(id);
        }
       
        public void Save(List<Forum> forums)
        {
            _forumService.Save(forums);
        }

        public void UpdateForum(Forum forum)
		{
            _forumService.UpdateForum(forum);
		}

        public List<ForumComment> GetCommentsForForum(Forum forum)
		{
            return _forumService.GetCommentsForForum(forum);

        }

        public void SetVeryHelpful(Forum forum)
		{
            _forumService.SetVeryHelpful(forum);
		}
        
        public void Update(Forum forum)
        {
            _forumService.Update(forum);
        }

    }
}
