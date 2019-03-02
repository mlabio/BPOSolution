@echo off
cls

set APP="BPOSolution"
set DIST_PATH="40.65.191.64 cd /C:/inetpub/wwwroot/dummy/"
set FULL_PATH="40.65.191.64 cd /C:/inetpub/wwwroot/dummy/BackEnd"

IF EXIST %DIST_PATH% (
    sc stop %APP%
) ELSE (
    mkdir %DIST_PATH%
    sc create %APP% binPath=%FULL_PATH%
)

dotnet publish -c release -o %DIST_PATH%
sc start %APP%
