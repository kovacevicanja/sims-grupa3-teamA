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
    public class ForumCommentRepository : IForumCommentRepository
    {
        private const string FilePath = "../../Resources/Data/forumComments.csv";

        private Serializer<ForumComment> _serializer;

        public List<ForumComment> _comments;

        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _comments = Load();
        }

        public void Initialize() { CommentUserBind(); }

        public List<ForumComment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ForumComment> comments)
        {
            _serializer.ToCSV(FilePath, comments);
        }
        public void SaveComment()
        {
            _serializer.ToCSV(FilePath, _comments);
        }
        public List<ForumComment> GetAll()
        {
            return _comments;
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (ForumComment forum in _comments)
            {
                if (forum.Id > maxId)
                {
                    maxId = forum.Id;
                }
            }
            return maxId + 1;
        }

        public void CommentUserBind()
        {
            foreach (ForumComment comment in _comments)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetById(comment.User.Id);
                comment.User = user;
            }
        }
        public void Create(ForumComment forum)
        {
            forum.Id = GenerateId();
            _comments.Add(forum);
            Save(_comments);
        }
        public ForumComment GetById(int id)
        {
            return _comments.Find(forum => forum.Id == id);
        }
    }
}
