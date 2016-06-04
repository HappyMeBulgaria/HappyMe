var HappyMe = HappyMe || {};

HappyMe.Questions = (function () {
    'use strict';

    var sendUserAnswer = function (data) {
        HttpRequester.postJson('/Questions/Answer', data)
            .then(function (data) {
                console.log(data);

                if (data.isAnswerCorrect) {
                    $("#success-message").dialog({
                        modal: true,
                        draggable: false,
                        resizable: false,
                        show: 'blind',
                        hide: 'blind',
                        width: $(window).width() * 0.7,
                        dialogClass: 'result-popup success-message',
                        buttons: {
                            "I've read and understand this": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                    window.location = '/questions/answer/' + data.sessionId;
                } else {
                    $("#error-message").dialog({
                        modal: true,
                        draggable: false,
                        resizable: false,
                        show: 'blind',
                        closeOnEscape: false,
                        hide: 'blind',
                        width: $(window).width() * 0.6,
                        dialogClass: 'result-popup error-message',
                        buttons: {
                            "Продължи": function () {
                                $(this).dialog("close");
                            }
                        },
                        open: function (event, ui) {
                            $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                        },
                    });
                }
            });
    };

    var loadAnswerClickEvent = function (questionId, sessionId) {
        $('.answers-wrapper').on('click',
        '.answer',
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