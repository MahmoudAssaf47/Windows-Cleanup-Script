@echo off
:: Copyright Information
:: Script Name: SystemCleanup.bat
:: Owner: Mahmoud Assaf
:: Email: mahmoud-assaf@post.com
:: Phone: +20114176847
:: Version: 0.1
:: Copyright 2024
setlocal enabledelayedexpansion 
:: إعدادات المسارات والملفات
set "desktopFolder=%USERPROFILE%\Desktop\cmn_mahmoud_assaf"
set "logFile=%desktopFolder%\cleanup_log.txt"
set "backupFolder=%desktopFolder%\backup"

:: إنشاء المجلدات إذا لم تكن موجودة
if not exist "%desktopFolder%" mkdir "%desktopFolder%"
if not exist "%backupFolder%" mkdir "%backupFolder%"

:: وظيفة لطباعة القائمة الرئيسية
:MainMenu
cls
echo ===============================
echo       System Cleanup Menu
echo ===============================
echo 1. Delete log files
echo 2. Delete user temp files
echo 3. Delete system temp files
echo 4. Delete prefetch files
echo 5. Empty Recycle Bin
echo 6. Clear memory cache
echo 7. Run Disk Cleanup
echo 8. Clean old Windows Update files
echo 9. Clear event logs
echo 10. Backup and Delete Specific Files
echo 11. Execute All
echo 12. Exit
echo ===============================
set /p choice="Select an option: "

if "%choice%"=="1" goto DeleteLogFiles
if "%choice%"=="2" goto DeleteUserTempFiles
if "%choice%"=="3" goto DeleteSystemTempFiles
if "%choice%"=="4" goto DeletePrefetchFiles
if "%choice%"=="5" goto EmptyRecycleBin
if "%choice%"=="6" goto ClearMemoryCache
if "%choice%"=="7" goto RunDiskCleanup
if "%choice%"=="8" goto CleanOldWindowsUpdateFiles
if "%choice%"=="9" goto ClearEventLogs
if "%choice%"=="10" goto BackupAndDeleteSpecificFiles
if "%choice%"=="11" goto ExecuteAll
if "%choice%"=="12" goto ExitScript

goto MainMenu

:DeleteLogFiles
echo Deleting log files...
set /p confirm="Are you sure you want to delete log files? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Deleting log files... >> %logFile%
    cd /
    del /s /q *.log
    echo Log files deleted. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:DeleteUserTempFiles
echo Deleting user temp files...
set /p confirm="Are you sure you want to delete user temp files? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Deleting user temp files... >> %logFile%
    xcopy /s /e /i /y %temp% "%backupFolder%\user_temp_backup"
    rd /s /q %temp%
    md %temp%
    echo User temp files deleted and backed up. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:DeleteSystemTempFiles
echo Deleting system temp files...
set /p confirm="Are you sure you want to delete system temp files? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Deleting system temp files... >> %logFile%
    xcopy /s /e /i /y C:\Windows\Temp "%backupFolder%\system_temp_backup"
    rd /s /q C:\Windows\Temp
    md C:\Windows\Temp
    echo System temp files deleted and backed up. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:DeletePrefetchFiles
echo Deleting prefetch files...
set /p confirm="Are you sure you want to delete prefetch files? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Deleting prefetch files... >> %logFile%
    xcopy /s /e /i /y C:\Windows\Prefetch "%backupFolder%\prefetch_backup"
    del /s /q C:\Windows\Prefetch\*
    echo Prefetch files deleted and backed up. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:EmptyRecycleBin
echo Emptying Recycle Bin...
set /p confirm="Are you sure you want to empty the Recycle Bin? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Emptying Recycle Bin... >> %logFile%
    rd /s /q C:\$Recycle.Bin
    echo Recycle Bin emptied. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:ClearMemoryCache
echo Clearing memory cache...
set /p confirm="Are you sure you want to clear memory cache? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Clearing memory cache... >> %logFile%
    %windir%\system32\rundll32.exe advapi32.dll,ProcessIdleTasks
    echo Memory cache cleared. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:RunDiskCleanup
echo Running Disk Cleanup...
set /p confirm="Are you sure you want to run Disk Cleanup? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Running Disk Cleanup... >> %logFile%
    cleanmgr /sagerun:1
    echo Disk Cleanup completed. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:CleanOldWindowsUpdateFiles
echo Cleaning up old Windows Update files...
set /p confirm="Are you sure you want to clean old Windows Update files? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Cleaning up old Windows Update files... >> %logFile%
    dism.exe /Online /Cleanup-Image /StartComponentCleanup
    echo Old Windows Update files cleaned. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:ClearEventLogs
echo Clearing event logs...
set /p confirm="Are you sure you want to clear event logs? (Y/N): "
if /i "%confirm%"=="Y" (
    echo %date% %time%: Clearing event logs... >> %logFile%
    wevtutil cl System
    wevtutil cl Application
    echo Event logs cleared. >> %logFile%
) else (
    echo Operation cancelled. >> %logFile%
)
goto MainMenu

:BackupAndDeleteSpecificFiles
echo Backup and delete specific files...
set /p folderPath="Enter the full path of the folder to backup and delete: "
if exist "%folderPath%" (
    set /p confirm="Are you sure you want to backup and delete files in %folderPath%? (Y/N): "
    if /i "%confirm%"=="Y" (
        echo %date% %time%: Backing up and deleting files in %folderPath%... >> %logFile%
        xcopy /s /e /i /y "%folderPath%" "%backupFolder%\specific_backup"
        rd /s /q "%folderPath%"
        echo Files backed up and deleted. >> %logFile%
    ) else (
        echo Operation cancelled. >> %logFile%
    )
) else (
    echo Folder not found. >> %logFile%
)
goto MainMenu

:ExecuteAll
echo Executing all cleanup tasks...
for %%i in (DeleteLogFiles DeleteUserTempFiles DeleteSystemTempFiles DeletePrefetchFiles EmptyRecycleBin ClearMemoryCache RunDiskCleanup CleanOldWindowsUpdateFiles ClearEventLogs) do (
    call :%%i
)
echo %date% %time%: All tasks completed. >> %logFile%
goto MainMenu

:ExitScript
echo Exiting script... >> %logFile%
set /p restart="Do you want to restart the computer now? (Y/N): "
if /i "%restart%"=="Y" (
    echo %date% %time%: Restarting the computer... >> %logFile%
    shutdown /r /t 0
) else (
    echo %date% %time%: Exiting without restart. >> %logFile%
)
exit /b
