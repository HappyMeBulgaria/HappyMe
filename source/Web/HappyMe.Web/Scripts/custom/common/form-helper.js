var HappyMe = HappyMe || {};

HappyMe.FormHelper = (function () {
    'use strict';

    function confirmSubmit (formSelector, message) {
        $(selector).on('submit',
             function () {
                 return confirm(message);
             });
    }

    return {
        confirmSubmit: confirmSubmit
    };

})();
