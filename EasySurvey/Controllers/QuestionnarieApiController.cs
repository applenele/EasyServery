using EasySurvey.Models;
using EasySurvey.Models.DataModel;
using EasySurvey.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySurvey.Controllers
{

    public class QuestionnarieApiController : BaseController
    {
        // GET: Questionnarie
        public ActionResult Index()
        {
            return View();
        }

        #region 创建问卷  以及添加问卷的问题 +Create
        /// <summary>
        /// 创建问卷  以及添加问卷的问题
        /// </summary>
        /// <param name="questions"></param>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(string questions, int userId, string title)
        {
            List<QuestionModel> qmodels = JsonConvert.DeserializeObject<List<QuestionModel>>(questions);
            Questionnaire Questionnaire = new Questionnaire { Title = title, UserID = userId, Time = DateTime.Now };
            db.Questionnaires.Add(Questionnaire);
            int result = 0;
            foreach (var model in qmodels)
            {
                Question question = new Question { QuestionnaireID = Questionnaire.ID, Title = model.Title, Time = DateTime.Now, IsBlank = false, IsMulSelect = false };
                db.Questions.Add(question);

                SysAnswer sysAnswer1 = new SysAnswer();
                sysAnswer1.Content = model.Q1;
                sysAnswer1.QuestionID = question.ID;

                SysAnswer sysAnswer2 = new SysAnswer();
                sysAnswer2.Content = model.Q2;
                sysAnswer2.QuestionID = question.ID;

                SysAnswer sysAnswer3 = new SysAnswer();
                sysAnswer3.Content = model.Q3;
                sysAnswer3.QuestionID = question.ID;

                SysAnswer sysAnswer4 = new SysAnswer();
                sysAnswer4.Content = model.Q4;
                sysAnswer4.QuestionID = question.ID;


                db.SysAnswers.Add(sysAnswer1);
                db.SysAnswers.Add(sysAnswer2);
                db.SysAnswers.Add(sysAnswer3);
                db.SysAnswers.Add(sysAnswer4);

                result= db.SaveChanges();
            }
            
            if (result > 0)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<QuestionnarieHub>();//得到Signalr context 
                context.Clients.All.GetNew(Questionnaire);  //将新上传的资源广播到全部客户端
                return Content("ok");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion


        #region 根据问卷的ID生成问卷
        /// <summary>
        /// 根据问卷的ID生成问卷
        /// </summary>
        /// <param name="qid"></param>
        /// <returns></returns>
        public ActionResult GetQuestionnarie(int qid)
        {
            Questionnaire Questionnaire = new Questionnaire();
            Questionnaire = db.Questionnaires.Find(qid);
            if (Questionnaire != null)
            {
                List<Question> questions = new List<Question>();
                List<QuestionReturnModel> questonModels = new List<QuestionReturnModel>();
                questions = db.Questions.Where(q => q.QuestionnaireID == qid).ToList();
                foreach (var model in questions)
                {
                    List<SysAnswer> answers = new List<SysAnswer>();
                    QuestionReturnModel questionModel = new QuestionReturnModel();
                    answers = db.SysAnswers.Where(sa => sa.QuestionID == model.ID).ToList();

                    questionModel.Title = model.Title;
                    questionModel.ID = model.ID;
                    questionModel.Q1 = answers[0].Content;
                    questionModel.Q2 = answers[1].Content;
                    questionModel.Q3 = answers[2].Content;
                    questionModel.Q4 = answers[3].Content;
                    questonModels.Add(questionModel);
                }
                QuestionnarieReturnModel Qrm = new QuestionnarieReturnModel();
                Qrm.ID = Questionnaire.ID;
                Qrm.UserID = Questionnaire.UserID;
                Qrm.Time = Questionnaire.Time.ToString();
                Qrm.Title = Questionnaire.Title;
                Qrm.Questions = questonModels;
                return Json(Qrm);
            }
            else
            {
                return Content("noexist");
            }
           
        }
        
        #endregion


        #region 分页获取问卷 + GetQuestionnaries
        /// <summary>
        /// 分页获取问卷
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult GetQuestionnaries(int page, string key)
        {
            List<Questionnaire> Questionnaires = new List<Questionnaire>();
            List<QuestionnarieListModel> _Questionnaires = new List<QuestionnarieListModel>();
            int index = (page - 1) * 10;
            if (!string.IsNullOrEmpty(key))
            {
                Questionnaires = db.Questionnaires.Where(q => q.Title.Contains(key)).OrderByDescending(p => p.Time).Skip(index).Take(10).ToList();
            }
            else
            {
                Questionnaires = db.Questionnaires.OrderByDescending(p => p.Time).Skip(index).Take(10).ToList();
            }
            foreach (var Questionnaire in Questionnaires)
            {
                QuestionnarieListModel model = new QuestionnarieListModel(Questionnaire);
                _Questionnaires.Add(model);
            }
            return Json(_Questionnaires);
        } 
        #endregion


        #region 用户提交调查
        /// <summary>
        /// 用户提交调查
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="qid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ActionResult UserSubmit(int uid, int qid, string content)
        {
            UserAnswer Answer = new UserAnswer { UserID = uid, QuestionnaireID = qid, Content = content, Time = DateTime.Now };
            db.UserAnswers.Add(Answer);
            db.SaveChanges();
            return Content("ok");
        } 
        #endregion


        #region 通过问卷id得到问卷的统计
        /// <summary>
        ///   通过问卷id得到问卷的统计
        /// </summary>
        /// <param name="qid"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult TongJi(int qid)
        {
            List<TjQuestion> questions = new List<TjQuestion>();
            List<TJSubmitQuestion> tjquestions = new List<TJSubmitQuestion>();
            Questionnaire questionnarie = new Questionnaire();
            questionnarie = db.Questionnaires.Find(qid);
            var uas = db.UserAnswers.Where(ua => ua.QuestionnaireID == qid).ToList();
            foreach (var ua in uas)
            {
                tjquestions = JsonConvert.DeserializeObject<List<TJSubmitQuestion>>(ua.Content);
                foreach (var tq in tjquestions)
                {
                    var question = questions.Where(q => q.Id == tq.Id).FirstOrDefault();
                    if (question == null)
                    {
                        TjQuestion _question = new TjQuestion();
                        _question.Id = tq.Id;
                        _question.Title = db.Questions.Where(q => q.QuestionnaireID == qid).ToList()[tq.Id - 1].Title;
                        if (tq.Answer == 1)
                        {
                            _question.A1++;
                        }
                        if (tq.Answer == 2)
                        {
                            _question.A2++;
                        }
                        if (tq.Answer == 3)
                        {
                            _question.A3++;
                        }
                        if (tq.Answer == 4)
                        {
                            _question.A4++;
                        }
                        questions.Add(_question);
                    }
                    else
                    {
                        if (tq.Answer == 1)
                        {
                            question.A1++;
                        }
                        if (tq.Answer == 2)
                        {
                            question.A2++;
                        }
                        if (tq.Answer == 3)
                        {
                            question.A3++;
                        }
                        if (tq.Answer == 4)
                        {
                            question.A4++;
                        }
                    }
                }
            }
            TjQuestionnarie TjQuestionnarie = new TjQuestionnarie();
            TjQuestionnarie.ID = qid;
            TjQuestionnarie.Title = questionnarie.Title;
            TjQuestionnarie.Questions = questions;
            return Json(TjQuestionnarie);
        } 
        #endregion
    }
}