using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyMe.Web.Areas.Administration.InputModels.Questions
{
    public class QuestionUpdateInputModel : QuestionCreateInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}