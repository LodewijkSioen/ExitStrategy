var gulp = require('gulp');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
var nuget = require('nuget-runner')({});

gulp.task('default', ['nuget-restore', 'build', 'test', 'nuget-pack']);

gulp.task('nuget-restore', function(){
	return nuget.restore({packages: 'src/ExitStrategy.sln'});
});

gulp.task('build', ['nuget-restore'], function() {
	return gulp
        .src('src/ExitStrategy.sln')
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

gulp.task('nuget-pack', ['test'], function(){
	return nuget.pack({
		spec: 'src/ForWebforms/ForWebforms.csproj',
		outputDirectory: '.'
	});
});