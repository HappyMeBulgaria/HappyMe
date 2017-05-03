var HappyMe = HappyMe || {};

HappyMe.Questions = (function (httpRequester) {
    'use strict';

    var sendUserAnswer = function (data) {
        httpRequester.postJson('/Questions/Answer', data)
            .then(function (answerData) {
                if (answerData.isAnswerCorrect) {
                    $('.color-question-image').css('filter', 'none');
                    $('.color-question-image').css('-webkit-filter', 'none');

                    $('#success-message').dialog({
                        modal: true,
                        draggable: false,
                        resizable: false,
                        closeOnEscape: false,
                        show: 'blind',
                        hide: 'blind',
                        width: 'auto',
                        height: 300,
                        maxWidth: 900,
                        responsive: true,
                        dialogClass: 'result-popup success-message',
                        buttons: {
                            'Продължи': function () {
                                window.location = '/questions/answer/' + answerData.sessionId;
                            }
                        },
                        open: function () {
                            $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                        }
                    });
                } else {
                    $('#error-message').dialog({
                        modal: true,
                        draggable: false,
                        resizable: false,
                        closeOnEscape: true,
                        show: 'blind',
                        hide: 'blind',
                        width: 'auto',
                        height: 300,
                        maxWidth: 900,
                        responsive: true,
                        dialogClass: 'result-popup error-message',
                        buttons: {
                            'Продължи': function () {
                                $(this).dialog('close');
                            }
                        },
                        open: function () {
                            $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                        }
                    });
                }
            });
    };

    var loadAnswerClickEvent = function (questionId, sessionId) {
        $('.answers-wrapper').on('click',
        '.answer',
        function (event) {

            // TODO: Check
            var answerId = event.currentTarget.dataset.answerId || event.originalEvent.target.dataset.answerId;

            var data = {
                answerId: answerId,
                questionId: questionId,
                sessionId: sessionId
            };

            sendUserAnswer(data);
        });
    };

    var loadAnswerDragAndDropEvents = function (questionId, sessionId) {
        $('.drag-and-drop-answers-wrapper')
            .on('dragstart',
                '.drag-and-drop-answer',
                function (event) {
                    console.log(event.originalEvent.target.dataset);
                    event.originalEvent.dataTransfer.setData('answerId', event.currentTarget.dataset.answerId);
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
})(HappyMe.HttpRequester);
