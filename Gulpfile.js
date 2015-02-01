var gulp = require('gulp');
var msbuild = require('gulp-msbuild');
var run = require('gulp-run');
var nuget = require('nuget-runner')({});

gulp.task('default', ['build', 'test']);

gulp.task('ci', []);

gulp.task('build', function() {
	//todo: pipe this? nuget.restore({packages: 'src/ExitStrategy.sln'});

    return gulp
        .src('src/*.sln')
        .pipe(msbuild({
            toolsVersion: 12.0,
            targets: ['Clean', 'Build'],
            errorOnFail: true,
            stdout: true
        }));
});

gulp.task('test', ['build'], function(){
	return gulp
		.src(['src/**/bin/**/*Tests.dll'], { read: false })
		.pipe(run('RunTests.bat'));
});