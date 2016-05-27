'use strict';

var editor = editor || {};

editor = (function () {
    function loadCKEditor(idSelector, height) {
        CKEDITOR.replace(idSelector);
        CKEDITOR.config.height = height || 300;
    }
    function unloadCKEditor(idSelector) {
        CKEDITOR.remove(idSelector);
    }
    function checkForUnsavedText(text) {
        document.body.onbeforeunload = function() {
            for (var editorName in CKEDITOR.instances) {
                if (CKEDITOR.instances.hasOwnProperty(editorName)) {
                    if (CKEDITOR.instances[editorName].checkDirty()) {
                        return text;
                    }
                }
            }
        };
    }


    return {
        loadCKEditor: loadCKEditor,
        unloadCKEditor: unloadCKEditor,
        checkForUnsavedText: checkForUnsavedText
    }
})()