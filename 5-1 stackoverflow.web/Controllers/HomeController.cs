using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _5_1_stackoverflow.web.Models;
using _5_1_stackoverflow.data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace _5_1_stackoverflow.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connString;
        public HomeController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            Repository rep = new Repository(_connString);
            return View(rep.GetQuestions());
        }

        [Authorize]
        public IActionResult QuestionForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuestion(Question question, IEnumerable<string> tags)
        {

            question.DatePosted = DateTime.Now;
            Repository rep = new Repository(_connString);
            rep.AddQuestion(question, tags);
            return Redirect("/");
        }

        public IActionResult Question(int id)
        {
            Repository rep = new Repository(_connString);
            User user = rep.GetUserByEmail(HttpContext.User.Identity.Name);
            var alreadyLiked = false;
            if (user.Likes.FirstOrDefault(l => l.QuestionId == id) != null)
            {
                alreadyLiked = true;
            }
            QuestionViewModel vm = new QuestionViewModel
            {
                Question = rep.GetQuestion(id),
                LoggedIn = User.Identity.IsAuthenticated,
                AlreadyLiked = alreadyLiked
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AnswerQuestion(Answer answer)
        {
            Repository rep = new Repository(_connString);
            rep.AnswerQuestion(answer);
            return Redirect($"/home/question?id={answer.Question.Id}");
        }

        [HttpPost]
        public IActionResult LikeQuestion(int id)
        {
            Repository rep = new Repository(_connString);
            //get user

            string email = HttpContext.User.Identity.Name;
            User user = rep.GetUserByEmail(email);
            //user.Likes.Add()
            int numLikes = rep.LikeQuestion(id, email);
            return Json(numLikes);
        }
    }
}
