const fs = require('fs');
const path = require('path');
const { exec } = require('child_process');
const inquirer = require('inquirer');
const util = require('util');

const execPromise = util.promisify(exec);

// Copyright Information
const copyrightInfo = `
===============================
      System Cleanup Menu
===============================
Script Name: SystemCleanup.js
Owner: Mahmoud Assaf
Email: mahmoud-assaf@post.com
Phone: +20114176847
Version: 0.1
Copyright 2024
`;

const desktopFolder = path.join(process.env.USERPROFILE, 'Desktop', 'cmn_mahmoud_assaf');
const logFile = path.join(desktopFolder, 'cleanup_log.txt');
const backupFolder = path.join(desktopFolder, 'backup');

// Create necessary folders if they don't exist
if (!fs.existsSync(desktopFolder)) fs.mkdirSync(desktopFolder);
if (!fs.existsSync(backupFolder)) fs.mkdirSync(backupFolder);

function log(message) {
    fs.appendFileSync(logFile, `${new Date().toISOString()}: ${message}\n`);
}

async function clearScreen() {
    console.clear();
}

async function mainMenu() {
    await clearScreen();
    console.log(copyrightInfo);
    console.log('1. Delete log files');
    console.log('2. Delete user temp files');
    console.log('3. Delete system temp files');
    console.log('4. Delete prefetch files');
    console.log('5. Empty Recycle Bin');
    console.log('6. Clear memory cache');
    console.log('7. Run Disk Cleanup');
    console.log('8. Clean old Windows Update files');
    console.log('9. Clear event logs');
    console.log('10. Backup and Delete Specific Files');
    console.log('11. Execute All');
    console.log('12. Exit');
    
    const { choice } = await inquirer.prompt({
        type: 'input',
        name: 'choice',
        message: 'Select an option: '
    });

    switch (choice) {
        case '1': await deleteLogFiles(); break;
        case '2': await deleteUserTempFiles(); break;
        case '3': await deleteSystemTempFiles(); break;
        case '4': await deletePrefetchFiles(); break;
        case '5': await emptyRecycleBin(); break;
        case '6': await clearMemoryCache(); break;
        case '7': await runDiskCleanup(); break;
        case '8': await cleanOldWindowsUpdateFiles(); break;
        case '9': await clearEventLogs(); break;
        case '10': await backupAndDeleteSpecificFiles(); break;
        case '11': await executeAll(); break;
        case '12': await exitScript(); break;
        default: console.log('Invalid option. Please try again.'); await mainMenu();
    }
}

async function confirmAction(message) {
    const { confirm } = await inquirer.prompt({
        type: 'confirm',
        name: 'confirm',
        message: message,
        default: false
    });
    return confirm;
}

async function executeCommand(command, successMessage, errorMessage) {
    try {
        await execPromise(command);
        log(successMessage);
    } catch (err) {
        log(`${errorMessage}: ${err.message}`);
    }
}

async function deleteLogFiles() {
    if (await confirmAction('Are you sure you want to delete log files?')) {
        log('Deleting log files...');
        await executeCommand('del /s /q *.log', 'Log files deleted.', 'Error deleting log files');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function deleteUserTempFiles() {
    if (await confirmAction('Are you sure you want to delete user temp files?')) {
        log('Deleting user temp files...');
        await executeCommand(`xcopy /s /e /i /y %temp% "${backupFolder}\\user_temp_backup"`, 
            'User temp files backed up.', 'Error backing up user temp files');
        await executeCommand('rd /s /q %temp% && md %temp%', 
            'User temp files deleted.', 'Error deleting user temp files');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function deleteSystemTempFiles() {
    if (await confirmAction('Are you sure you want to delete system temp files?')) {
        log('Deleting system temp files...');
        await executeCommand(`xcopy /s /e /i /y C:\\Windows\\Temp "${backupFolder}\\system_temp_backup"`, 
            'System temp files backed up.', 'Error backing up system temp files');
        await executeCommand('rd /s /q C:\\Windows\\Temp && md C:\\Windows\\Temp', 
            'System temp files deleted.', 'Error deleting system temp files');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function deletePrefetchFiles() {
    if (await confirmAction('Are you sure you want to delete prefetch files?')) {
        log('Deleting prefetch files...');
        await executeCommand(`xcopy /s /e /i /y C:\\Windows\\Prefetch "${backupFolder}\\prefetch_backup"`, 
            'Prefetch files backed up.', 'Error backing up prefetch files');
        await executeCommand('del /s /q C:\\Windows\\Prefetch\\*', 
            'Prefetch files deleted.', 'Error deleting prefetch files');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function emptyRecycleBin() {
    if (await confirmAction('Are you sure you want to empty the Recycle Bin?')) {
        log('Emptying Recycle Bin...');
        await executeCommand('rd /s /q C:\\$Recycle.Bin', 
            'Recycle Bin emptied.', 'Error emptying Recycle Bin');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function clearMemoryCache() {
    if (await confirmAction('Are you sure you want to clear memory cache?')) {
        log('Clearing memory cache...');
        await executeCommand('%windir%\\system32\\rundll32.exe advapi32.dll,ProcessIdleTasks', 
            'Memory cache cleared.', 'Error clearing memory cache');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function runDiskCleanup() {
    if (await confirmAction('Are you sure you want to run Disk Cleanup?')) {
        log('Running Disk Cleanup...');
        await executeCommand('cleanmgr /sagerun:1', 
            'Disk Cleanup completed.', 'Error running Disk Cleanup');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function cleanOldWindowsUpdateFiles() {
    if (await confirmAction('Are you sure you want to clean old Windows Update files?')) {
        log('Cleaning up old Windows Update files...');
        await executeCommand('dism.exe /Online /Cleanup-Image /StartComponentCleanup', 
            'Old Windows Update files cleaned.', 'Error cleaning old Windows Update files');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function clearEventLogs() {
    if (await confirmAction('Are you sure you want to clear event logs?')) {
        log('Clearing event logs...');
        await executeCommand('wevtutil cl System', 
            'System event logs cleared.', 'Error clearing System event logs');
        await executeCommand('wevtutil cl Application', 
            'Application event logs cleared.', 'Error clearing Application event logs');
    } else {
        log('Operation cancelled.');
    }
    await mainMenu();
}

async function backupAndDeleteSpecificFiles() {
    const { folderPath } = await inquirer.prompt({
        type: 'input',
        name: 'folderPath',
        message: 'Enter the full path of the folder to backup and delete: '
    });

    if (fs.existsSync(folderPath)) {
        if (await confirmAction(`Are you sure you want to backup and delete files in ${folderPath}?`)) {
            log(`Backing up and deleting files in ${folderPath}...`);
            await executeCommand(`xcopy /s /e /i /y "${folderPath}" "${backupFolder}\\specific_backup"`, 
                'Files backed up.', 'Error backing up files');
            await executeCommand(`rd /s /q "${folderPath}"`, 
                'Files deleted.', 'Error deleting files');
        } else {
            log('Operation cancelled.');
        }
    } else {
        log('Folder not found.');
    }
    await mainMenu();
}

async function executeAll() {
    console.log('Executing all cleanup tasks...');
    await deleteLogFiles();
    await deleteUserTempFiles();
    await deleteSystemTempFiles();
    await deletePrefetchFiles();
    await emptyRecycleBin();
    await clearMemoryCache();
    await runDiskCleanup();
    await cleanOldWindowsUpdateFiles();
    await clearEventLogs();
    log('All tasks completed.');
    await mainMenu();
}

async function exitScript() {
    log('Exiting script...');
    const { restart } = await inquirer.prompt({
        type: 'confirm',
        name: 'restart',
        message: 'Do you want to restart the computer now?',
        default: false
    });
    if (restart) {
        await executeCommand('shutdown /r /t 0', 'Restart command executed.', 'Error executing restart command');
    } else {
        console.log('No restart requested. Exiting script.');
    }
    process.exit();
}

// Start the main menu
mainMenu().catch(err => {
    console.error('An unexpected error occurred:', err.message);
    log(`An unexpected error occurred: ${err.message}`);
    process.exit(1);
}); 
