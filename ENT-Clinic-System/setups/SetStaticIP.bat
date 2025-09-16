@echo off
REM Change the network adapter name if needed
SET INTERFACE="Ethernet"

REM Static IP configuration
SET IP=192.168.1.100
SET MASK=255.255.255.0
SET GATEWAY=192.168.1.1
SET DNS=8.8.8.8

REM Set static IP
netsh interface ip set address name=%INTERFACE% static %IP% %MASK% %GATEWAY% 1

REM Set DNS
netsh interface ip set dns name=%INTERFACE% static %DNS%

echo Static IP has been set successfully!
pause
