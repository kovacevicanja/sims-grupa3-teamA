using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class ForumService : IForumService
    {
        private IForumRepository _forumRepository;
        private IForumCommentService _forumCommentService;
        public ForumService() { }
        public void Initialize()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
        }
        public void Create(Forum forum)
        {
            _forumRepository.Create(forum);
        }
        public void Save(List<Forum> forums)
        {
            _forumRepository.Save(forums);
        }
        public void SaveForum()
        {
            _forumRepository.SaveForum();
        }
        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }

        public Forum GetById(int id)
        {
            return _forumRepository.GetById(id);
        }

        public void UpdateForum(Forum forum)
        {
            List<Forum> forums = _forumRepository.GetAll();
            int index = forums.FindIndex(f => f.Id == forum.Id);
            if (index != -1)
            {
                forums[index] = forum;
                _forumRepository.Save(forums);
            }
        }

        public List<ForumComment> GetCommentsForForum(Forum forum)
		{
            List<ForumComment> comments = new List<ForumComment>();
            List<ForumComment> allComments = _forumCommentService.GetAll();
            foreach(ForumComment comment in allComments)
			{
                if(comment.Forum.Id == forum.Id)
				{
                    comments.Add(comment);
				}
			}
            return comments;            
        }

        public void SetVeryHelpful(Forum forum)
		{
            bool isVeryHelpful = false;
            int ownerComments = 0;
            int guestComments = 0;
            foreach (var com in _forumCommentService.GetAll())
            {
                // com.User = _userRepository.GetById(com.User.Id);
                if (com.Forum.Id == forum.Id && com.User.UserType == Model.Enums.UserType.OWNER) //vlasnik ima smestaj na toj lokaciji
                {
                    ownerComments++;
                }
                else if (com.Forum.Id == forum.Id && com.User.UserType == Model.Enums.UserType.GUEST1 && com.IsInvalid == true) //gost mora da je posetio lokaciju
                {
                    guestComments++;
                }
            }
            if (ownerComments >= 10 && guestComments >= 20)
            {
                isVeryHelpful = true;
                forum.IsUseful = true;
            }
            if (isVeryHelpful)
            {
                UpdateForum(forum);
            }
        }


        public void Update(Forum forum)
        {
            Forum oldForum = GetById(forum.Id);
            if (oldForum == null)
            {
                return;
            }
            oldForum.IsUseful = forum.IsUseful;
        }

    }
}
