var HappyMe = HappyMe || {};

HappyMe.Questions = (function () {
    'use strict';

    var sendUserAnswer = function (data) {
        HttpRequester.postJson('/Questions/Answer', data)
            .then(function (data) {
                console.log(data);

                if (data.isAnswerCorrect) {
                    alert('Correct answer!');
                    window.location = '/questions/answer/' + data.sessionId;
                } else {
                    alert('Wrong answer!');
                }
            });
    };

    var loadAnswerClickEvent = function (questionId, sessionId) {
        $('.one-answer-question-answers-wrapper').on('click',
        '.one-answer-question-answer',
        function (event) {
            var answerId = event.originalEvent.target.dataset.answerId;

            var data = {
                answerId: answerId,
                questionId: questionId,
                sessionId: sessionId
            };

            sendUserAnswer(data);
        });
    };

    var loadAnswerDragAndDropEvents = function (questionId, sessionId) {
        $('.drag-and-drop-asnwers-wrapper')
            .on('dragstart',
                '.drag-and-drop-answer',
                function (event) {
                    console.log(event.originalEvent.target.dataset);
                    event.originalEvent.dataTransfer.setData('answerId', event.originalEvent.target.dataset.answerId);
                });

        $('#question-answer-area')
            .on('dragover',
                function (event) {
                    event.originalEvent.preventDefault();
                });

        $('#question-answer-area')
            .on('drop',
                function (event) {
                    var answerId = event.originalEvent.dataTransfer.getData('answerId');

                    var data = {
                        answerId: answerId,
                        questionId: questionId,
                        sessionId: sessionId
                    };

                    sendUserAnswer(data);
                });
    };

    return {
        loadAnswerClickEvent: loadAnswerClickEvent,
        loadAnswerDragAndDropEvents: loadAnswerDragAndDropEvents
    };
})();