/// <binding />
"use strict";

// patch for autoprefixer "Promise not defined" bug: http://stackoverflow.com/questions/32490328/gulp-autoprefixer-throwing-referenceerror-promise-is-not-defined
require('es6-promise').polyfill();

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    del = require("del"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    autoprefixer = require('gulp-autoprefixer'),
    sourcemaps = require('gulp-sourcemaps'),
    gulpif = require("gulp-if"),
    debug = require("gulp-debug"),
    merge = require('merge-stream'),
    pathbuilder = require("./Gulp/PathBuilder.js"),
    launchSettings = require("./Properties/launchSettings.json");

function getEnv() {
    return launchSettings.profiles["IIS Express"].environmentVariables["Hosting:Environment"];
}
function isProd() {
    return getEnv() === "Production";
}
function isDev() {
    return getEnv() === "Development";
}
function autoprefix() {
    return autoprefixer({ browsers: ['last 2 versions'] });
}

var paths = {};

paths.dist = { base: "./wwwroot/" };
paths.dist.css = paths.dist.base + "css/";
paths.dist.js = paths.dist.base + "js/"; 

paths.less = { base: "./Less/" };
paths.less.all = paths.less.base + "**/*.less";
paths.less.dev_src = ["main.less"];
paths.less.lib = paths.less.base + "lib/";
paths.less.lib_files = paths.less.lib + "**/*.less";
paths.less.uku_bootstrap_filename = "uku-bootstrap.less";
paths.less.uku_fontawesome_filename = "uku-fontawesome.less";
paths.less.uku_bootstrap = paths.less.lib + paths.less.uku_bootstrap_filename;
paths.less.uku_fontawesome = paths.less.lib + paths.less.uku_fontawesome_filename;
paths.less.prod_out = paths.dist.css + "site.css";

paths.bower = { base: "./Bower/" };
paths.bower.fontawesome = paths.bower.base + "Font-Awesome/";

var path = new pathbuilder().build([
    ["dist", "wwwroot", [
        ["css", "css", [
            ["prod", "site.css"]
        ]],
        ["js", "js"],
        ["fonts", "fonts"]
    ]],
    ["less", "Less", [
        ["all", "**/*.less"],
        ["dev_src", ["main.less", "album.less"]],
        ["lib", "lib", [
            ["files", "**/*.less"],
            ["bootstrap", "uku-bootstrap.less"],
            ["fontawesome", "uku-fontawesome.less"]
        ]]
    ]],
    ["bower", "Bower", [
        ["bootstrap", "bootstrap", [
            ["fonts", "dist/fonts/*.*"],
            ["js", "dist/js/bootstrap.js"]
        ]],
        ["fontawesome", "Font-Awesome", [
            ["fonts", "fonts/*.*"]
        ]]
        //["jquery", 
    ]]
]);

gulp.task("[watch]", function () {
    gulp.watch(path.less.$all, ["less:dev"]);
});

gulp.task("less:prod", ["clean:css"], function () {
    var src = [path.less.lib.$files].concat(path.less.$dev_src);
    return gulp.src(src, { base: path.$less })
        .pipe(less())
        .pipe(autoprefix())
        .pipe(cssmin())
        .pipe(concat(path.dist.css.$prod))
        .pipe(gulp.dest("."));
});

gulp.task("less:dev", ["less:app", "less:bootstrap", "less:fontawesome"]);

gulp.task("less:app", function () {
    return gulp.src(path.less.$dev_src, { base: path.$less })
        .pipe(less())
        .pipe(autoprefix())
        .pipe(gulp.dest(path.dist.$css));
});

gulp.task("less:bootstrap", function () {
    return gulp.src(path.less.lib.$bootstrap)
        .pipe(less())
        .pipe(autoprefix())
        .pipe(gulp.dest(path.dist.$css));
});

gulp.task("less:fontawesome", function () {
    return gulp.src(path.less.lib.$fontawesome)
        .pipe(less())
        .pipe(autoprefix())
        .pipe(gulp.dest(path.dist.$css));
});

gulp.task("clean:css", function () {
    del.sync([path.dist.$css]);
});

gulp.task("clean:js", function () {
    del.sync([path.dist.$js]);
});

gulp.task("clean:fonts", function () {
    del.sync([path.dist.$fonts]);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:fonts"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("copy:fonts", ["clean:fonts"], function () {
    return gulp.src([path.bower.fontawesome.$fonts])
        .pipe(gulp.dest(path.dist.$fonts));
});

gulp.task("copy:js", ["clean:js"], function () {
    return gulp.src([path.bower.bootstrap.$js], { base: path.$bower })
        .pipe(gulp.dest(path.dist.$js));
});