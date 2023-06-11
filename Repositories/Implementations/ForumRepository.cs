using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class ForumRepository : IForumRepository
    {
        private const string FilePath = "../../Resources/Data/forums.csv";

        private Serializer<Forum> _serializer;

        public List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = Load();
        }

        public void Initialize() { 
            ForumLocationBind();
            ForumUserBind();
        }

        public List<Forum> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Forum> forums)
        {
            _serializer.ToCSV(FilePath, forums);
        }
        public void SaveForum()
        {
            _serializer.ToCSV(FilePath, _forums);
        }
        public List<Forum> GetAll()
        {
            return _forums;
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (Forum forum in _forums)
            {
                if (forum.Id > maxId)
                {
                    maxId = forum.Id;
                }
            }
            return maxId + 1;
        }
       

        public void Create(Forum forum)
        {
            forum.Id = GenerateId();
            _forums.Add(forum);
            Save(_forums);
        }
        public Forum GetById(int id)
        {
            return _forums.Find(forum => forum.Id == id);
        }
        public void ForumLocationBind()
        {
            foreach (Forum forum in _forums)
            {
                Location location = Injector.CreateInstance<IAccommodationLocationRepository>().GetById(forum.Location.Id);
                forum.Location = location;
            }
        }
        public void AccommodationImagesBind()
        {
            IForumCommentRepository commentRepository = Injector.CreateInstance<IForumCommentRepository>();
            foreach (ForumComment comment in commentRepository.GetAll())
            {
                Forum f = GetById(comment.Forum.Id);
                f.Comments.Add(comment);
            }
        }
        public void ForumUserBind()
        {
            foreach (Forum forum in _forums)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetById(forum.User.Id);
                forum.User = user;
            }
        }
    }
}
