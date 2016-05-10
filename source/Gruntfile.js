/// <binding AfterBuild='concurrent:lintingAndTesting' />
module.exports = function (grunt) {

    grunt.initConfig({
        eslint: {
            te4Fest: {
                src: ["Web/Scripts/*.js"]
            },
        },
        jasmine: {
            te4Fest: {
                src: [''],
                options: {
                    specs: '',
                    vendor: [''],
                    outfile: '_SpecRunnerTe4Fest.html',
                    template: require('grunt-template-jasmine-istanbul'),
                    templateOptions: {
                        coverage: 'bin/coverage/Te4Fest/coverage.json',
                        report: 'bin/coverage/Te4Fest',
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
                'jasmine:te4Fest',
                'eslint:te4Fest']
        }
    });

    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks("gruntify-eslint");
    grunt.loadNpmTasks('grunt-contrib-jasmine');
    grunt.loadNpmTasks('grunt-notify');
    grunt.registerTask("default", ["concurrent:lintingAndTesting"]);
};