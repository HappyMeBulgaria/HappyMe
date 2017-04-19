var HappyMe = HappyMe || {};

HappyMe.FormHelper = (function () {
    'use strict';

    function confirmSubmit (formSelector, message) {
        $(formSelector).on('submit',
             function () {
                 return confirm(message);
             });
    }

    return {
        confirmSubmit: confirmSubmit
    };

})();
