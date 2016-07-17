var HappyMe = HappyMe || {};

HappyMe.ColorUtils = (function () {
    'use strict';

    /**
    * HSV to RGB color conversion
    *
    * H runs from 0 to 360 degrees
    * S and V run from 0 to 100
    * Ported from the excellent java algorithm by Eugene Vishnevsky at:
    * http://www.cs.rit.edu/~ncs/color/t_convert.html
    */
    var hsvToRgb = function (h, s, v) {
        var r;
        var g;
        var b;

        // Make sure our arguments stay in-range
        h = Math.max(0, Math.min(360, h));
        s = Math.max(0, Math.min(100, s));
        v = Math.max(0, Math.min(100, v));

        // We accept saturation and value arguments from 0 to 100 because that's
        // how Photoshop represents those values. Internally, however, the
        // saturation and value are calculated from a range of 0 to 1. We make
        // That conversion here.
        s /= 100;
        v /= 100;

        if (s === 0) {
            // Achromatic (grey)
            r = g = b = v;
            return [Math.round(r * 255), Math.round(g * 255), Math.round(b * 255)];
        }

        h /= 60; // sector 0 to 5
        var i = Math.floor(h);
        var f = h - i; // factorial part of h
        var p = v * (1 - s);
        var q = v * (1 - s * f);
        var t = v * (1 - s * (1 - f));

        if (i === 0) {
            r = v;
            g = t;
            b = p;
        } else if (i === 1) {
            r = q;
            g = v;
            b = p;
        } else if (i === 2) {
            r = p;
            g = v;
            b = t;
        } else if (i === 3) {
            r = p;
            g = q;
            b = v;
        } else if (i === 4) {
            r = t;
            g = p;
            b = v;
        } else {
            r = v;
            g = p;
            b = q;
        }

        return [Math.round(r * 255), Math.round(g * 255), Math.round(b * 255)];
    };

    var getDistinctColorsAsArray = function (total) {
        var i = 360 / (total - 1); // distribute the colors evenly on the hue range
        var r = []; // hold the generated colors
        for (var x = 0; x < total; x++) {
            r.push(hsvToRgb(i * x, 100, 100));

            // you can also alternate the saturation and value for even more contrast between the colors
        }

        return r;
    };

    var getDistinctColorsAsStringArray = function (total) {
        var output = [];
        var data = getDistinctColorsAsArray(total);

        for (var i = 0; i < data.length; i++) {
            var rgbElement = 'rgb(' + data[i][0] + ', ' + data[i][1] + ', ' + data[i][2] + ')';
            output.push(rgbElement);
        }

        return output;
    };

    return {
        getDistinctColors: getDistinctColorsAsStringArray
    };

})();
