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
    public class ForumService : IForumService
    {
        private IForumRepository _forumRepository;
        public ForumService() { }
        public void Initialize()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
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
        

        
    }
}
