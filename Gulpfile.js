var gulp = require('gulp');
var fs = require('fs');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
var nuget = require('nuget-runner')({});

gulp.task('default', ['nuget-restore', 'create-folders', 'build', 'test', 'nuget-pack']);

gulp.task('create-folders', function(){
	return fs.mkdir('./artifacts');
});

gulp.task('nuget-restore', function(){
	return nuget.restore({packages: './src/ExitStrategy.sln'});
});

gulp.task('build', ['nuget-restore'], function() {
	return gulp
        .src('./src/ExitStrategy.sln')
        .pipe(msbuild({
            toolsVersion: 12.0,
            //msbuildPath: 'C:/Program Files (x86)/MSBuild/12.0/Bin/MSBuild.exe', //Fix for AppVeyor
            targets: ['Clean', 'Build'],
            errorOnFail: true,
            stdout: true
        }));
});

gulp.task('test', ['create-folders', 'build'], function (cb) {
	var finder = require('findit')('./src/packages/');	
	var fixieName = 'Fixie.Console.exe';

	finder.on('file', function (file, stat) {
	    if(file.indexOf(fixieName, file.length - fixieName.length) !== -1){	    	
	    	finder.stop();
	    	var command = file + ' --NUnitXml artifacts/TestResult.xml src/ForWebforms.Tests/bin/ExitStrategy.ForWebforms.Tests.dll';

	    	exec(command, function (err, stdout, stderr) {
		    	console.log(stdout);
		    	console.log(stderr);
		    	cb(err);
		  	});
	    }	    
	});  	
});

gulp.task('nuget-pack', ['create-folders', 'test'], function(){
	return nuget.pack({
		spec: './src/ForWebforms/ForWebforms.csproj',
		outputDirectory: './artifacts/'
	});
});