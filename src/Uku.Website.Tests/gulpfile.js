var gulp = require('gulp'),
    karma = require("karma").Server;

gulp.task('karma', function (done) {
    new karma({
        configFile: __dirname + '/karma.conf.js',
        singleRun: false
    }, function () { done(); }).start();
});