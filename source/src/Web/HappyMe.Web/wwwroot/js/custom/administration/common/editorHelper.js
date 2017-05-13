var editor = editor || {};

editor = (function () {
    'use strict';

    var checkForUnsavedText = function (text) {
        document.body.onbeforeunload = function () {
            for (var editorName in CKEDITOR.instances) {
                if (CKEDITOR.instances.hasOwnProperty(editorName)) {
                    if (CKEDITOR.instances[editorName].checkDirty()) {
                        return text;
                    }
                }
            }
        };
    };

    var loadCkEditor = function (idSelector, height) {
        CKEDITOR.replace(idSelector);
        CKEDITOR.config.height = height || 300;
    };

    var unloadCkEditor = function (idSelector) {
        CKEDITOR.remove(idSelector);
    };

    return {
        loadCKEditor: loadCkEditor,
        unloadCKEditor: unloadCkEditor,
        checkForUnsavedText: checkForUnsavedText
    };
})();
