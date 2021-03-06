﻿module.exports = function (config) {
    config.set({

        basePath: '',

        plugins: [
            'karma-chrome-launcher',
            'karma-jasmine'
        ],

        frameworks: ['jasmine'],

        files: [
            '../Uku.Website/Gulp/*.js',
            'Gulp/*-spec.js'
        ],

        exclude: [
        ],

        //reporters: ['progress'],

        preprocessors: {

        },

        // web server port
        port: 44300,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: true,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        browsers: ['Chrome'],

        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        //singleRun: true,

        // Concurrency level
        // how many browser should be started simultanous
        concurrency: Infinity

    })
}
