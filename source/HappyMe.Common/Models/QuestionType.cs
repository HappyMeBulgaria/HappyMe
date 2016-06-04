namespace HappyMe.Common.Models
{
    using System.ComponentModel;

    public enum QuestionType
    {
        [Description("Въпрос с изображение")]
        ImageQuestion = 1,

        [Description("Въпрос с един отговор")]
        OneAnswerQuestion = 2,

        [Description("Въпрос с цвят")]
        ColorQuestion = 3,

        [Description("Въпрос с цвят")]
        AlphabetQuestion = 4,

        [Description("Drag and Drop въпрос")]
        DragAndDropQuestion = 5
    }
}
