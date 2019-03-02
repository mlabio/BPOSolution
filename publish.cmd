@echo off
cls

set APP="BPOSolution"
set DIST_PATH="D:\dev\Gitlab-ci\BPOSolution"
set FULL_PATH="D:\dev\Gitlab-ci\BPOSolution\BPOSolution.sln"

IF EXIST %DIST_PATH% (
    sc stop %APP%
) ELSE (
    mkdir %DIST_PATH%
    sc create %APP% binPath=%FULL_PATH%
)

dotnet publish -c release -o %DIST_PATH%
sc start %APP%