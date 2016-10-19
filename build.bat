@echo Off

REM build
"%programfiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe" BingRestServices.sln /p:Configuration=Release /verbosity:minimal /p:BuildInParallel=true /p:RestorePackages=true /t:Rebuild

REM Package Folders Setup
rd build /s /q

if not exist build mkdir build
if not exist build\package mkdir build\package
if not exist build\package\bing-rest-services mkdir build\package\bing-rest-services

if not exist build\package\bing-rest-services\lib mkdir build\package\bing-rest-services\lib
if not exist build\package\bing-rest-services\lib\4.5 mkdir build\package\bing-rest-services\lib\4.5

copy BingRestServices\bin\Release\BingRestServices.* build\package\bing-rest-services\lib
copy BingRestServices\bin\Release\BingRestServices.* build\package\bing-rest-services\lib\4.5

REM Create Packages
cmd /c tools\nuget.exe pack "BingRestServices.nuspec" -BasePath build\package\bing-rest-services -o build