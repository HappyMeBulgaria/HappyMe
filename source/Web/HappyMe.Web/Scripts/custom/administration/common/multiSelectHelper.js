var HappyMe = HappyMe || {};

HappyMe.MultiSelectHelper = (function () {

    var loadMultiSelect = function (selector, options) {
        $(selector).chosen(options);
    };

    return {
        loadMultiSelect: loadMultiSelect
    };
})();