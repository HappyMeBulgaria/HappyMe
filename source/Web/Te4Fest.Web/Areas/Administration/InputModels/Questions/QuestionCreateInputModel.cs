using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Te4Fest.Web.Areas.Administration.InputModels.Questions
{
    public class QuestionCreateInputModel
    {
        [AllowHtml]
        [UIHint("CKeditor")]
        public string Text { get; set; }

        // question type -> dropdown

        // add image -> kendo upload image

        // module -> dropdown

        // answers -> optional ??
    }
}