cd accountcore
xcopy /E /I /Y "C:\Program Files (x86)\NUnit.org\nunit-console" .\tools\nunit-console
RMDIR reports\dotCover /S /Q
del reports\dotCover.html
dotnet sonarscanner begin /k:account /name:account-core /version:1.0 /d:sonar.cs.dotcover.reportsPaths="reports\dotCover.html"
dotnet build PCO.AccountCore/PCO.AccountCore.sln
C:\projects\dotCover\dotCover.exe analyse /ReportType=HTML /Output="reports\dotCover.html" "/TargetExecutable=C:\Program Files\dotnet\dotnet.exe" /TargetArguments="test .\PCO.AccountCore.ApplicationServices.Tests\PCO.AccountCore.ApplicationServices.Tests.csproj"
dotnet sonarscanner end
SLEEP 20
ECHO PROCESS ENDED