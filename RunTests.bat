@ECHO OFF
SETLOCAL
REM You might prefer specific scripts for each tool. Makes the usage a bit easier. This is an example for NUnit.
 
FOR /R %~dp0\src\packages %%G IN (Fixie.Console.exe) DO (
IF EXIST %%G (
SET TOOLPATH=%%G
GOTO FOUND
)
)
IF '%TOOLPATH%'=='' GOTO NOTFOUND
 
:FOUND
%TOOLPATH% %* --NUnitXml TestResult.xml src\ForWebforms.Tests\bin\ForWebforms.Tests.dll
GOTO :EOF
 
:NOTFOUND
ECHO Fixie.Console not found.
EXIT /B 1 