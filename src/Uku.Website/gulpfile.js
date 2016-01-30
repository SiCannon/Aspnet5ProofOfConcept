/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less");

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.less = paths.webroot + "css/**/*.less";
paths.lessOutput = paths.webroot + "css/";
paths.lessCss = paths.lessOutput + "**/*.css";
paths.concatJsDest = paths.webroot + "js/all.min.js";
paths.concatCssDest = paths.webroot + "css/all.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean:less", function (cb) {
    rimraf(paths.lessCss, cb);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:less"]);

gulp.task("less", function () {
    return gulp.src(paths.less)
        .pipe(less())
        .pipe(gulp.dest(paths.lessOutput));
});

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", ["less"], function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);
