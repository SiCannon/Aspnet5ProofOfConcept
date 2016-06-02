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

var path = new pathbuilder().build([
    ["dist", "wwwroot", [
        ["css", "css", [
            ["prod", "site.css"]
        ]],
        ["js", "js", [
            ["prod", "site.js"]
        ]],
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
    ["javascript", "JavaScript", [
        ["src", "**/*.js"]
    ]],
    ["bower", "Bower", [
        ["bootstrap", "bootstrap", [
            ["fonts", "dist/fonts/*.*"],
            ["js", "dist/js/bootstrap.js"]
        ]],
        ["fontawesome", "Font-Awesome", [
            ["fonts", "fonts/*.*"]
        ]],
        ["jquery", "jquery/dist/jquery.js"]
    ]]
]);

path.src = {
    less: [path.less.lib.$files].concat(path.less.$dev_src),
    js: [path.bower.bootstrap.$js, path.bower.$jquery, path.javascript.$src]
};

gulp.task("[watch]", function () {
    gulp.watch(path.less.$all, ["less:dev"]);
});

gulp.task("less:prod", ["clean:css"], function () {
    return gulp.src(path.src.less, { base: path.$less })
        .pipe(less())
        .pipe(autoprefix())
        .pipe(cssmin())
        .pipe(concat(path.dist.css.$prod))
        .pipe(gulp.dest("."));
});

gulp.task("less:dev", ["clean:css"], function () {
    return gulp.src(path.src.less, { base: path.$less })
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

gulp.task("copy:fonts", ["clean:fonts"], function () {
    return gulp.src([path.bower.fontawesome.$fonts])
        .pipe(gulp.dest(path.dist.$fonts));
});

gulp.task("js:dev", ["clean:js"], function () {
    return gulp.src(path.src.js, { cwd: "." })
        .pipe(gulp.dest(path.dist.$js));
});

gulp.task("js:prod", ["clean:js"], function () {
    return gulp.src(path.src.js, { base: "." })
        .pipe(debug())
        .pipe(concat(path.dist.js.$prod))
        .pipe(uglify())
        //.pipe(uglify({ compress: { hoist_funs: false, hoist_vars: false } }))
        .pipe(gulp.dest("."));
});
