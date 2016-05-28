using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyMe.Common.Mapping;
using HappyMe.Common.Models;
using HappyMe.Data.Models;

namespace HappyMe.Web.Areas.Administration.InputModels.Questions
{
    public class QuestionCreateInputModel : IMapTo<Question>, IMapFrom<Question>
    {
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Text { get; set; }

        [UIHint("EnumDropDownList")]
        public QuestionType Type { get; set; }

        public bool IsPublic { get; set; }

        [UIHint("DropDownList")]
        public int ModuleId { get; set; }

        // add image -> kendo upload image

        // module -> dropdown

        // answers -> optional ?? from dropdown
    }
}