var HappyMe = HappyMe || {};

HappyMe.MultiSelectHelper = (function () {
    'use strict';

    var loadMultiSelect = function (selector, options) {
        $(selector).chosen(options);
    };

    return {
        loadMultiSelect: loadMultiSelect
    };
})();
