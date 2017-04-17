/// <binding AfterBuild='concurrent:lintingAndTesting' />
module.exports = function (grunt) {

    grunt.initConfig({
        eslint: {
            happyMeWeb: {
                src: ['Web/HappyMe.Web/Scripts/custom/**/*.js']
            }
        },
        jasmine: {
            happyMeWeb: {
                src: ['Web/HappyMe.Web/Scripts/custom/**/*.js', 'Web/HappyMe.Web/Scripts/custom/*.js'],
                options: {
                    specs: 'Web/HappyMe.Web/Scripts/specs/*.js',
                    vendor: ['Web/HappyMe.Web/Scripts/vendor/jquery/jquery-3.0.0.min.js'],
                    outfile: '_SpecRunnerHappyMeWeb.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/HappyMeWeb/coverage.json',
                        report: 'bin/coverage/HappyMeWeb'
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
            happyMeWeb: {
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
                'jasmine:happyMeWeb',
                'eslint:happyMeWeb',
                'csslint:happyMeWeb']
        }
    });

    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks('gruntify-eslint');
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-notify');
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.registerTask('default', ['concurrent:lintingAndTesting']);
};