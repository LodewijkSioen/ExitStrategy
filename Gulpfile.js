var gulp = require('gulp');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
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

gulp.task('test', ['build'], function (cb) {
	var finder = require('findit')('src/packages/');	
	var fixieName = 'Fixie.Console.exe';

	finder.on('file', function (file, stat) {
	    if(file.indexOf(fixieName, file.length - fixieName.length) !== -1){	    	
	    	finder.stop();
	    	var command = file + ' --NUnitXml TestResult.xml src/ForWebforms.Tests/bin/ExitStrategy.ForWebforms.Tests.dll';

	    	exec(command, function (err, stdout, stderr) {
		    	console.log(stdout);
		    	console.log(stderr);
		    	cb(err);
		  	});
	    }	    
	});  	
});