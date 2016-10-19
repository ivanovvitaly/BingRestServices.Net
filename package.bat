tools\nuget.exe update -self

rd build /s /q

if not exist build mkdir build
if not exist build\package mkdir build\package
if not exist build\package\bing-rest-services mkdir build\package\bing-rest-services

if not exist build\package\bing-rest-services\lib mkdir build\package\bing-rest-services\lib
if not exist build\package\bing-rest-services\lib\4.5 mkdir build\package\bing-rest-services\lib\4.5

copy BingRestServices\bin\Release\BingRestServices.* build\package\bing-rest-services\lib
copy BingRestServices\bin\Release\BingRestServices.* build\package\bing-rest-services\lib\4.5

tools\nuget.exe pack BingRestServices.nuspec -BasePath build\package\bing-rest-services -o build