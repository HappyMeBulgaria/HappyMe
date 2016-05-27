using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Te4Fest.Common.Mapping;
using Te4Fest.Common.Models;
using Te4Fest.Data.Models;

namespace Te4Fest.Web.Areas.Administration.InputModels.Questions
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

        // add image -> kendo upload image

        // module -> dropdown

        // answers -> optional ?? from dropdown
    }
}