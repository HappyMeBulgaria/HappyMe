var HappyMe = HappyMe || {};

HappyMe.Questions = (function () {

    var loadAnswerClickEvent = function (questionId, sessionId) {
        $('.color-asnwers-wrapper').on('click',
        '.color-answer',
        function (event) {
            var answerId = event.originalEvent.target.dataset.answerId;

            var postData = {
                answerId: answerId,
                questionId: questionId,
                sessionId: sessionId
            };

            HttpRequester.postJson('/Questions/Answer', postData)
                .then(function (data) {
                    console.log(data);

                    if (data.isAnswerCorrect) {
                        alert('Correct answer!');
                        window.location = '/questions/answer/' + sessionId;
                    } else {
                        alert('Wrong answer!');
                    }
                });

        });
    };

    return {
        loadAnswerClickEvent: loadAnswerClickEvent
    };
})();