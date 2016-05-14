/// <binding AfterBuild='concurrent:lintingAndTesting' />
module.exports = function (grunt) {

    grunt.initConfig({
        eslint: {
            te4FestWeb: {
                src: ["Web/Te4Fest.Web/Scripts/custom/*.js"]
            },
            te4FestWebApi: {
                src: ["Web/Te4Fest.Web.Api/Scripts/custom/*.js"]
            },
        },
        jasmine: {
            te4FestWeb: {
                src: ['Web/Te4Fest.Web/Scripts/custom/*.js'],
                options: {
                    specs: 'Web/Te4Fest.Web/Scripts/specs/*.js',
                    vendor: ['Web/Te4Fest.Web/Scripts/vendor/jquery/jquery-2.2.3.js'],
                    outfile: '_SpecRunnerTe4FestWeb.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/Te4FestWeb/coverage.json',
                        report: 'bin/coverage/Te4FestWeb',
                        //thresholds: {
                        //    lines: 80,
                        //    statements: 80,
                        //    branches: 80,
                        //    functions: 80
                        //}
                    }
                }
            },
            te4FestWebApi: {
                src: ['Web/Te4Fest.Web.Api/Scripts/custom/*.js'],
                options: {
                    specs: 'Web/Te4Fest.Web.Api/Scripts/specs/*.js',
                    vendor: ['Web/Te4Fest.Web.Api/Scripts/vendor/jquery/jquery-2.2.3.js'],
                    outfile: '_SpecRunnerTe4FestWebApi.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/Te4FestWebApi/coverage.json',
                        report: 'bin/coverage/Te4FestWebApi',
                        //thresholds: {
                        //    lines: 80,
                        //    statements: 80,
                        //    branches: 80,
                        //    functions: 80
                        //}
                    }
                }
            }
        },
        concurrent: {
            lintingAndTesting: [
                'jasmine:te4FestWeb',
                'eslint:te4FestWeb',
                'jasmine:te4FestWebApi',
                'eslint:te4FestWebApi']
        }
    });

    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks("gruntify-eslint");
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-notify');
    grunt.registerTask("default", ["concurrent:lintingAndTesting"]);
};