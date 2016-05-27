using System.ComponentModel;

namespace HappyMe.Common.Models
{
    public enum QuestionType
    {
        [Description("Въпрос с изображение")]
        ImageQuestion = 1,

        [Description("Въпрос с един отговор")]
        OneAnswerQuestion = 2,

        [Description("Въпрос с цвят")]
        ColorQuestion = 3
    }
}
