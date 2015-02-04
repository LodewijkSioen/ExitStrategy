var gulp = require('gulp');
var fs = require('fs');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
var nuget = require('nuget-runner')({});

var os = require('os');
var path = require('path');
var childProcess = require('child_process');

gulp.task('default', ['nuget-restore', 'create-folders', 'build', 'test', 'nuget-pack']);

gulp.task('debug-msbuild', function(){
	if (!process.platform.match(/^win/)) {
		console.log('Not running on windows!');
	}

	console.log('os.arch: ' + os.arch());

	var program_files = os.arch() === 'x64' ? 'Program Files (x86)' : 'Program Files';
	var msbuildPath = path.join('C:', program_files, 'MSBuild', '12.0', 'Bin/MSBuild.exe');
  	console.log('MSBuild path: ' + msbuildPath);
  	console.log('Normalized MSBuild path: ' + path.normalize(msbuildPath));

  	console.log('Is MSBuild there: ' + fs.existsSync(msbuildPath));

  	var cp = childProcess.exec('"' + msbuildPath + '"');
  	cp.stdout.pipe(process.stdout);
  	cp.stderr.pipe(process.stderr);
});

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
            toolsVersion: '12.0',
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