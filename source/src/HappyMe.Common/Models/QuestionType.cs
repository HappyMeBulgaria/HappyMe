namespace HappyMe.Common.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum QuestionType
    {
        [Description("Въпрос с изображение")]
        [Display(Name = "Въпрос с изображение")]
        ImageQuestion = 1,

        [Description("Въпрос с един отговор")]
        [Display(Name = "Въпрос с един отговор")]
        OneAnswerQuestion = 2,

        [Description("Въпрос с цвят")]
        [Display(Name = "Въпрос с цвят")]
        ColorQuestion = 3,

        [Description("Въпрос за азбуката")]
        [Display(Name = "Въпрос за азбуката")]
        AlphabetQuestion = 4,

        [Description("Въпрос за свързване")]
        [Display(Name = "Въпрос за свързване")]
        DragAndDropQuestion = 5
    }
}
