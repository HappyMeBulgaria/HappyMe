/// <binding AfterBuild='concurrent:lintingAndTesting' />
module.exports = function (grunt) {

    grunt.initConfig({
        eslint: {
            HappyMe: {
                src: ['Web/HappyMe.Web/Scripts/custom/**/*.js']
            }
        },
        jasmine: {
            HappyMe: {
                src: ['Web/HappyMe.Web/Scripts/custom/*.js'],
                options: {
                    specs: 'Web/HappyMe.Web/Scripts/specs/*.js',
                    vendor: ['Web/HappyMe.Web/Scripts/vendor/jquery/jquery-2.2.3.js'],
                    outfile: '_SpecRunnerHappyMeWeb.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/HappyMe/coverage.json',
                        report: 'bin/coverage/HappyMe'
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
        csslint: {
            options: {
                csslintrc: 'Web/HappyMe.Web/.csslintrc',
                formatters: [{ id: require('csslint-stylish'), dest: 'report/csslint_stylish.xml' }]
            },
            HappyMe: {
                options: {
                    import: 2
                },
                src: [
                    'Web/HappyMe.Web/Content/custom/*.css',
                    'Web/HappyMe.Web/Content/custom/**/*.css']
            }
        },
        concurrent: {
            lintingAndTesting: [
                'jasmine:HappyMe',
                'eslint:HappyMe',
                'csslint:HappyMe']
        }
    });

    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks('gruntify-eslint');
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-notify');
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.registerTask('default', ['concurrent:lintingAndTesting']);
};