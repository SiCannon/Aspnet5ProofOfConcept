/// <binding AfterBuild='less' Clean='clean' ProjectOpened='[watch]' />
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
        ["css", "css"],
        ["js", "js"]
    ]],
    ["less", "Less", [
        ["all", "**/*.less"],
        ["dev_src", ["main.less"]],
        ["lib", "lib", [
            ["files", "**/*.less"],
            ["bootstrap", "uku-bootstrap.less"],
            ["fontawesome", "uku-fontawesome.less"]
        ]]
    ]]
]);

/*paths.root(".", ".", [
    node("dist", "wwwroot", [
        node("css", "css", [
            node("prod_out", "site.css")
        ]),
        node("js", "js")
    ]),
    node("less", "Less", [
        node("all", "** /*.less"),
        list("dev_src", ["main.less"]),
        node("lib", "lib", [
            node("files", "** /*.less"),
            node("bootstrap", "uku-bootstrap.less"),
            node("fontawesome", "uku-fontawesome.less"),
        ])
    ])
]);*/

gulp.task("[watch]", function () {
    gulp.watch(paths.less.uku_bootstrap, ["less:bootstrap", "less"]);
    gulp.watch(paths.less.uku_fontawesome, ["less:fontawesome", "less"]);
    gulp.watch(paths.less.all, ["less"]);
});

gulp.task("less", function () {
    if (isDev()) {
        return gulp.src(paths.less.dev_src, { cwd: paths.less.base })
            .pipe(less())
            .pipe(autoprefix())
            .pipe(gulp.dest(paths.dist.css));
    }
    else {
        var src = [paths.less.lib_files].concat(paths.less.dev_src.map(function (x) { return paths.less.base + x; }));
        return gulp.src(src, { base: paths.less.base })
            .pipe(debug())
            .pipe(less())
            .pipe(autoprefix())
            .pipe(cssmin())
            .pipe(concat(paths.less.prod_out))
            .pipe(gulp.dest("."));
    }
});

gulp.task("less:bootstrap", function () {
    return gulp.src(paths.less.uku_bootstrap)
        .pipe(less())
        .pipe(autoprefix())
        .pipe(gulp.dest(paths.dist.css));
});

gulp.task("less:fontawesome", function () {
    return gulp.src(paths.less.uku_fontawesome)
        .pipe(less())
        .pipe(autoprefix())
        .pipe(gulp.dest(paths.dist.css));
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.dist.css, cb);
});

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});


gulp.task("test", function () {
    console.log(path.dist.css.$path());
    console.log(path.less.lib.files.$path());
});

/*gulp.task("copy:fontawesome", function () {
    //gulp.src(paths.
});*/