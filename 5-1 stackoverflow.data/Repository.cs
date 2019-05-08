using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace _5_1_stackoverflow.data
{
    public class Repository
    {
        private string _connString { get; }
        public Repository(string connString)
        {
            _connString = connString;
        }

        public void AddUser(User user, string password)
        {
            user.PasswordHash = PasswordHelper.HashPassword(password);
            using(var context = new StackContext(_connString))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User Login(string email, string password)
        {
            User user;
            using(var ctx = new StackContext(_connString))
            {
                 user = ctx.Users.FirstOrDefault(u => u.Email == email);
            }
            if(user == null)
            {
                return null;
            }
            if(PasswordHelper.PasswordMatch(password, user.PasswordHash))
            {
                return user;
            }
            return null;

        }

        // public User GetByEmail(string email)

        public IEnumerable<Question> GetQuestions()
        {
            using(var ctx = new StackContext(_connString))
            {
                //return ctx.Questions.Include(q => q.Likes).Include(q => q.Tags).ToList();
                return  ctx.Questions.OrderByDescending(q => q.DatePosted).Include(q => q.Likes).Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).ToList();
            }
        }

        public void AddQuestion(Question question, IEnumerable<string> tags)
        {
            using(var context = new StackContext(_connString))
            {
                context.Questions.Add(question);
                foreach(string tag in tags)
                {
                    Tag t = GetTag(tag);
                    int tagId;
                    if(t == null)
                    {
                        tagId = AddTag(tag);
                    }
                    else
                    {
                        tagId = t.Id;
                    }
                    context.QuestionTags.Add(new QuestionTags
                    {
                        QuestionId = question.Id,
                        TagId = tagId
                    });
                }
                context.SaveChanges();
            }
        }

        public Question GetQuestion(int id)
        {
            using(var ctx = new StackContext(_connString))
            {
                #region using own sql select
                //using (var command = ctx.Database.GetDbConnection().CreateCommand())
                //{
                //    command.CommandText = @"SELECT Top 1 * from Questions q
                //join QuestionTags qt
                //on qt.QuestionId = q.Id
                //join tags t
                //on qt.TagId = t.Id  Where q.id = @id"; new SqlParameter("@id", id);
                //    ctx.Database.OpenConnection();
                //    using (var result = command.ExecuteReader())
                //    {
                //        Question question = new Question
                //        {
                //            Text = (string)result["kdfjk"]
                //        };
                //        return question;
                //    }
                //}
                #endregion
                //not sure why this doesnt work maybe cause of version or that it not return it in right format
                //var qe = ctx.Questions.FromSql("Select * from Questions").ToList();

                return ctx.Questions.Include(q => q.Answers).Include(q => q.Likes).Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).FirstOrDefault(q => q.Id == id);
            }
        }

        private Tag GetTag(string name)
        {
            using(var context = new StackContext(_connString))
            {
                return context.Tags.FirstOrDefault(t => t.Name == name);
            }
        }

        private int AddTag(string name)
        {
            using(var context = new StackContext(_connString))
            {
                var tag = new Tag { Name = name };
                context.Tags.Add(tag);
                context.SaveChanges();
                return tag.Id;
            }
        }

        public void AnswerQuestion(Answer answer)
        {
            using(var ctx = new StackContext(_connString))
            {
                //ctx.Answers.Add(answer);
                //ctx.SaveChanges();
                ctx.Database.ExecuteSqlCommand("INSERT INTO Answers VALUES(@text, @qid)",
                    new SqlParameter("@text", answer.Text), new SqlParameter("@qid", answer.Question.Id));
            }
        }

        public User GetUserByEmail(string email)
        {
            using(var ctx = new StackContext(_connString))
            {
                return ctx.Users.Include(u => u.Likes).FirstOrDefault(u => u.Email == email);
            }
        }

        public int LikeQuestion(int id, string email)
        {
            User user = GetUserByEmail(email);
            if(user == null)
            {
                return 0;
            }
            Likes like = new Likes
            {
                QuestionId = id,
                UserId = user.Id 
            };
            using(var ctx = new StackContext(_connString))
            {
                ctx.Likes.Add(like);
                ctx.SaveChanges();
                var x =  ctx.Likes.Where(l => l.QuestionId == id);
                var c = x.Count();
                return x.Count();
            }
        }
    }

   
}
