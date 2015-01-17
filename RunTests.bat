@ECHO OFF
SETLOCAL
 
FOR /R %~dp0\src\packages %%G IN (Fixie.Console.exe) DO (
    IF EXIST %%G (
        SET TOOLPATH=%%G
        GOTO FOUND
    )
)
IF '%TOOLPATH%'=='' GOTO NOTFOUND
 
:FOUND
%TOOLPATH% %* --NUnitXml TestResult.xml src\ForWebforms.Tests\bin\ExitStrategy.ForWebforms.Tests.dll
GOTO :EOF
 
:NOTFOUND
ECHO Fixie.Console not found.
EXIT /B 1 
