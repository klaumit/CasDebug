@echo off

set PATH=c:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin;%PATH%

dotnet build Complex.sln

msbuild Complex.sln /nologo /clp:ErrorsOnly

dotnet build Complex.sln

