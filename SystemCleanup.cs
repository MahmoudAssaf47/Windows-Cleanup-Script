using System;
using System.Diagnostics;
using System.IO;

namespace SystemCleanup
{
    class Program
    {
        // Copyright Information
        // Script Name: SystemCleanup.cs
        // Owner: Mahmoud Assaf
        // Email: mahmoud-assaf@post.com
        // Phone: +20114176847
        // Version: 0.1
        // Copyright 2024

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("       System Cleanup Menu");
                Console.WriteLine("===============================");
                Console.WriteLine("1. Delete log files");
                Console.WriteLine("2. Delete user temp files");
                Console.WriteLine("3. Delete system temp files");
                Console.WriteLine("4. Delete prefetch files");
                Console.WriteLine("5. Empty Recycle Bin");
                Console.WriteLine("6. Clear memory cache");
                Console.WriteLine("7. Run Disk Cleanup");
                Console.WriteLine("8. Clean old Windows Update files");
                Console.WriteLine("9. Clear event logs");
                Console.WriteLine("10. Backup and Delete Specific Files");
                Console.WriteLine("11. Execute All");
                Console.WriteLine("12. Exit");
                Console.WriteLine("===============================");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DeleteLogFiles();
                        break;
                    case "2":
                        DeleteUserTempFiles();
                        break;
                    case "3":
                        DeleteSystemTempFiles();
                        break;
                    case "4":
                        DeletePrefetchFiles();
                        break;
                    case "5":
                        EmptyRecycleBin();
                        break;
                    case "6":
                        ClearMemoryCache();
                        break;
                    case "7":
                        RunDiskCleanup();
                        break;
                    case "8":
                        CleanOldWindowsUpdateFiles();
                        break;
                    case "9":
                        ClearEventLogs();
                        break;
                    case "10":
                        BackupAndDeleteSpecificFiles();
                        break;
                    case "11":
                        ExecuteAll();
                        break;
                    case "12":
                        ExitScript();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        static void DeleteLogFiles()
        {
            Console.WriteLine("Deleting log files...");
            Console.Write("Are you sure you want to delete log files? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Deleting log files...\n");
                foreach (string file in Directory.GetFiles("C:\\", "*.log", SearchOption.AllDirectories))
                {
                    File.Delete(file);
                }
                File.AppendAllText(logFile, "Log files deleted.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void DeleteUserTempFiles()
        {
            Console.WriteLine("Deleting user temp files...");
            Console.Write("Are you sure you want to delete user temp files? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string tempFolder = Path.GetTempPath();
                string backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "backup");
                string backupPath = Path.Combine(backupFolder, "user_temp_backup");
                Directory.CreateDirectory(backupPath);
                Directory.Move(tempFolder, backupPath);
                Directory.CreateDirectory(tempFolder);
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: User temp files deleted and backed up.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void DeleteSystemTempFiles()
        {
            Console.WriteLine("Deleting system temp files...");
            Console.Write("Are you sure you want to delete system temp files? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string tempFolder = @"C:\Windows\Temp";
                string backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "backup");
                string backupPath = Path.Combine(backupFolder, "system_temp_backup");
                Directory.CreateDirectory(backupPath);
                Directory.Move(tempFolder, backupPath);
                Directory.CreateDirectory(tempFolder);
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: System temp files deleted and backed up.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void DeletePrefetchFiles()
        {
            Console.WriteLine("Deleting prefetch files...");
            Console.Write("Are you sure you want to delete prefetch files? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string prefetchFolder = @"C:\Windows\Prefetch";
                string backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "backup");
                string backupPath = Path.Combine(backupFolder, "prefetch_backup");
                Directory.CreateDirectory(backupPath);
                Directory.Move(prefetchFolder, backupPath);
                Directory.CreateDirectory(prefetchFolder);
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Prefetch files deleted and backed up.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void EmptyRecycleBin()
        {
            Console.WriteLine("Emptying Recycle Bin...");
            Console.Write("Are you sure you want to empty the Recycle Bin? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Emptying Recycle Bin...\n");
                // Using PowerShell to empty Recycle Bin
                Process.Start("powershell.exe", "-NoProfile -Command \"Clear-RecycleBin -Confirm:$false\"");
                File.AppendAllText(logFile, "Recycle Bin emptied.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void ClearMemoryCache()
        {
            Console.WriteLine("Clearing memory cache...");
            Console.Write("Are you sure you want to clear memory cache? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Clearing memory cache...\n");
                // Using PowerShell to clear memory cache
                Process.Start("powershell.exe", "-NoProfile -Command \"[System.Runtime.GC]::Collect()\"");
                File.AppendAllText(logFile, "Memory cache cleared.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void RunDiskCleanup()
        {
            Console.WriteLine("Running Disk Cleanup...");
            Console.Write("Are you sure you want to run Disk Cleanup? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Running Disk Cleanup...\n");
                Process.Start("cleanmgr.exe", "/sagerun:1");
                File.AppendAllText(logFile, "Disk Cleanup completed.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void CleanOldWindowsUpdateFiles()
        {
            Console.WriteLine("Cleaning up old Windows Update files...");
            Console.Write("Are you sure you want to clean old Windows Update files? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Cleaning up old Windows Update files...\n");
                Process.Start("dism.exe", "/Online /Cleanup-Image /StartComponentCleanup");
                File.AppendAllText(logFile, "Old Windows Update files cleaned.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void ClearEventLogs()
        {
            Console.WriteLine("Clearing event logs...");
            Console.Write("Are you sure you want to clear event logs? (Y/N): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Clearing event logs...\n");
                Process.Start("wevtutil.exe", "clog Application");
                Process.Start("wevtutil.exe", "clog System");
                File.AppendAllText(logFile, "Event logs cleared.\n");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void BackupAndDeleteSpecificFiles()
        {
            Console.WriteLine("Backing up and deleting specific files...");
            Console.Write("Enter the path of the files or folders to backup and delete: ");
            string sourcePath = Console.ReadLine();
            string backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "backup");
            string backupPath = Path.Combine(backupFolder, "specific_files_backup");
            Directory.CreateDirectory(backupPath);

            if (Directory.Exists(sourcePath) || File.Exists(sourcePath))
            {
                string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cmn_mahmoud_assaf", "cleanup_log.txt");
                File.AppendAllText(logFile, $"{DateTime.Now}: Backing up and deleting specific files from {sourcePath}...\n");

                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(backupPath, Path.GetFileName(sourcePath)));
                }
                else if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(backupPath, Path.GetFileName(sourcePath)));
                }

                File.AppendAllText(logFile, "Specific files backed up and deleted.\n");
            }
            else
            {
                Console.WriteLine("The specified path does not exist.");
            }
        }

        static void ExecuteAll()
        {
            DeleteLogFiles();
            DeleteUserTempFiles();
            DeleteSystemTempFiles();
            DeletePrefetchFiles();
            EmptyRecycleBin();
            ClearMemoryCache();
            RunDiskCleanup();
            CleanOldWindowsUpdateFiles();
            ClearEventLogs();
            BackupAndDeleteSpecificFiles();
        }

        static void ExitScript()
        {
            Console.WriteLine("Exiting script...");
        }
    }
}
