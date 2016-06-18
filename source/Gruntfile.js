/// <binding AfterBuild='concurrent:lintingAndTesting' />
module.exports = function (grunt) {

    grunt.initConfig({
        eslint: {
            te4FestWeb: {
                src: ['Web/HappyMe.Web/Scripts/custom/**/*.js']
            }
        },
        jasmine: {
            te4FestWeb: {
                src: ['Web/HappyMe.Web/Scripts/custom/*.js'],
                options: {
                    specs: 'Web/HappyMe.Web/Scripts/specs/*.js',
                    vendor: ['Web/HappyMe.Web/Scripts/vendor/jquery/jquery-2.2.3.js'],
                    outfile: '_SpecRunnerTe4FestWeb.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/Te4FestWeb/coverage.json',
                        report: 'bin/coverage/Te4FestWeb'
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
            te4FestWeb: {
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
                'jasmine:te4FestWeb',
                'eslint:te4FestWeb',
                'csslint:te4FestWeb']
        }
    });

    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks('gruntify-eslint');
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-notify');
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.registerTask('default', ['concurrent:lintingAndTesting']);
};