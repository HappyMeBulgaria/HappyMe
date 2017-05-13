const gulp = require('gulp');
const jasmine = require('gulp-jasmine');
const eslint = require('gulp-eslint');
const csslint = require('gulp-csslint');

var paths = {
    webroot: './wwwroot/',
    javaScriptRoot: './wwwroot/js/',
    cssRoot: './wwwroot/css/'
};

gulp.task('jslint', () => {
    return gulp.src([
        paths.javaScriptRoot + '/custom/**/*.js',
        paths.javaScriptRoot + '/custom/*.js',
        '!node_modules/**'])
        .pipe(eslint())
        .pipe(eslint.format())
        .pipe(eslint.failAfterError());
});

// Not configured!
gulp.task('csslint', () => {
    gulp.src([paths.cssRoot + '*.css'])
        .pipe(csslint())
        .pipe(csslint.formatter());
});


gulp.task('default', ['jslint'], () =>
    gulp.src([paths.webroot + 'js/specs/*.js'])
        .pipe(jasmine())
);