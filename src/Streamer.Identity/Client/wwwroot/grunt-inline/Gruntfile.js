module.exports =  function (grunt) {

    grunt.loadNpmTasks('grunt-inline');

    grunt.initConfig({
        inline: {
            dist: {
                options: {
                    tag: ''
                },
                src: 'src/index.html',
                dest: 'dist/index.html'
            }
        }
    });


    // grunt.registerTask('default', ['jshint']);

}
