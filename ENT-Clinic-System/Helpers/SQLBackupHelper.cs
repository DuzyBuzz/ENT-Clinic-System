using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace ENT_Clinic_System.Helpers
{
/// <summary>
    /// SQLBackupHelper (NET 4.8 / WinForms-safe)
    /// - Configuration driven (credentials from external source, not hardcoded)
    /// - Fetch+merge remote before push to avoid NonFastForwardException
    /// - If a push still fails due to non-fast-forward, attempts a force-push as a fallback
    /// - No Console.ReadKey() calls (safe to call from a WinForms app)
    /// - Returns int status codes (0 = success)
    /// - Enhanced error handling with delegated logging
    /// - Proper resource cleanup and validation
    /// </summary>
 internal class SQLBackupHelper
    {
// -------------------------
        // Hardcoded configuration
        // -------------------------
        private const int MaxRetries = 3;
        private const int InternetRetryIntervalMs = 15000;
        private const int InternetTimeoutMs = 2000;
        private const string InternetCheckHost = "8.8.8.8";
        private const int MaxBackupSizeKb = 500000; // 500 MB limit

    // -------------------------
        // Hardcoded credentials and paths
        // -------------------------
        private readonly string _dbName = "ent_clinic_db";
        private readonly string _dbUser = "root";
      private readonly string _dbPassword = "password";
        private readonly string _gitToken = "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";
        private readonly string _repoUrl = "https://github.com/DuzyBuzz/SQL_backup.git";
        private readonly string _repoPath = @"C:\Projects\SQL_backup_repo";
        private readonly string _mysqldumpPath;
        private readonly string _backupDatesFile; 
        private readonly Action<string> _infoLogger;
        private readonly Action<string> _warningLogger;
        private readonly Action<string> _errorLogger;

        /// <summary>
    /// Initialize with mysqldump path and optional custom loggers.
        /// </summary>
        public SQLBackupHelper(string mysqldumpPath, Action<string> infoLogger = null, Action<string> warningLogger = null, Action<string> errorLogger = null)
        {
            _mysqldumpPath = mysqldumpPath;
            _backupDatesFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup_dates.txt");
            
// Setup logging delegates (default to Console if not provided)
        _infoLogger = infoLogger ?? (msg => Console.WriteLine("[INFO] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + msg));
  _warningLogger = warningLogger ?? (msg => Console.WriteLine("[WARN] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + msg));
            _errorLogger = errorLogger ?? (msg => Console.WriteLine("[ERROR] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + msg));
      }

  /// <summary>
        /// Run the backup. Returns 0 on success; non-zero codes indicate errors.
        /// </summary>
        public int RunBackup()
        {
  if (string.IsNullOrWhiteSpace(_gitToken))
            {
     _errorLogger("ERROR: Missing GitHub token. Please set GIT_TOKEN environment variable.");
        return 1;
        }

       if (string.IsNullOrWhiteSpace(_dbPassword))
  {
    _errorLogger("ERROR: Missing database password. Please set DB_PASSWORD environment variable.");
      return 1;
  }

         string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string backupFileName = string.Format("{0}_{1}.sql", _dbName, timestamp);
  string tempBackupFile = Path.Combine(Path.GetTempPath(), backupFileName);

            _infoLogger("Starting daily backup: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

try
       {
            // -------------------------
    // 1) Prevent duplicate backups per day
          // -------------------------
                if (File.Exists(_backupDatesFile))
      {
      var doneDates = File.ReadAllLines(_backupDatesFile)
     .Select(x => x.Trim())
     .Where(x => x != "");

           if (doneDates.Contains(DateTime.Today.ToString("yyyy-MM-dd")))
        {
     _infoLogger("Backup already done today. Exiting.");
       return 0;
     }
       }

     // -------------------------
              // 2) Run mysqldump
       // -------------------------
 if (!File.Exists(_mysqldumpPath))
         {
      _errorLogger("ERROR: mysqldump not found at: " + _mysqldumpPath);
            return 2;
      }

           _infoLogger("Running mysqldump...");

    string args = string.Format(
        "--user={0} --password={1} --databases {2} --complete-insert --routines --events --triggers --single-transaction --add-drop-database --add-drop-table",
  _dbUser, _dbPassword, _dbName);

         var psi = new ProcessStartInfo
                {
       FileName = _mysqldumpPath,
          Arguments = args,
    RedirectStandardOutput = true,
         RedirectStandardError = true,
      UseShellExecute = false,
CreateNoWindow = true
        };

   Process proc = Process.Start(psi);
            if (proc == null)
       {
   _errorLogger("ERROR: Failed to start mysqldump process.");
               return 3;
    }

       using (proc)
             {
  string stdout = proc.StandardOutput.ReadToEnd();
          string stderr = proc.StandardError.ReadToEnd();
         proc.WaitForExit();

   if (proc.ExitCode != 0)
   {
               _errorLogger("mysqldump ERROR: " + stderr);
          return 3;
           }

          try
 {
                 File.WriteAllText(tempBackupFile, stdout);
     }
      catch (IOException ioex)
    {
       _errorLogger("ERROR writing backup file: " + ioex.Message);
          return 7;
      }
        }

         FileInfo fi = new FileInfo(tempBackupFile);
                if (!fi.Exists || fi.Length == 0)
          {
  _errorLogger("ERROR: Dump file is empty.");
        return 4;
    }

 long sizeKb = fi.Length / 1024;
  if (sizeKb > MaxBackupSizeKb)
       {
         _errorLogger(string.Format("ERROR: Backup file too large ({0} KB, max {1} KB).", sizeKb, MaxBackupSizeKb));
   return 8;
    }

          _infoLogger("Backup created: " + sizeKb + " KB");

      // -------------------------
      // 3) Prepare Git repo
 // -------------------------
  try
       {
    PrepareRepository();
                }
             catch (Exception ex)
                {
              _errorLogger("ERROR preparing repository: " + ex.Message);
         return 9;
     }

        // -------------------------
    // 4) Copy backup into repo
  // -------------------------
             string destFile = Path.Combine(_repoPath, backupFileName);
 try
          {
              File.Copy(tempBackupFile, destFile, true);
            _infoLogger("File copied to repo.");
             }
    catch (IOException ioex)
         {
        _errorLogger("ERROR copying file to repo: " + ioex.Message);
        return 10;
  }

                // -------------------------
      // 5) Commit + safe push (fetch + merge -> push)
      // -------------------------
                int pushResult = CommitAndPush(backupFileName);
                if (pushResult != 0)
     {
     return pushResult;
                }

            // -------------------------
     // 6) Record backup date
            // -------------------------
         try
 {
                    File.AppendAllText(_backupDatesFile, DateTime.Today.ToString("yyyy-MM-dd") + Environment.NewLine);
                }
       catch (IOException ioex)
    {
      _errorLogger("ERROR recording backup date: " + ioex.Message);
       return 11;
                }

        _infoLogger("Backup completed successfully.");
        return 0;
            }
       catch (Exception ex)
        {
      _errorLogger("Unexpected error: " + ex.ToString());
           return 99;
            }
            finally
            {
         // Cleanup temp file
         try
       {
         if (File.Exists(tempBackupFile))
      {
              File.Delete(tempBackupFile);
          _infoLogger("Temporary backup file cleaned up.");
       }
    }
         catch (IOException ioex)
      {
      _warningLogger("Could not delete temporary file: " + ioex.Message);
       }
    }
        }

   /// <summary>
        /// Prepare the Git repository (clone if needed, validate).
        /// </summary>
        private void PrepareRepository()
        {
          if (Directory.Exists(_repoPath) && !Repository.IsValid(_repoPath))
  {
            _infoLogger("Invalid repository detected. Removing and re-cloning...");
  Directory.Delete(_repoPath, true);
   }

            if (!Directory.Exists(_repoPath))
            {
      Directory.CreateDirectory(_repoPath);
 }

            if (!Repository.IsValid(_repoPath))
    {
     _infoLogger("Cloning remote repo...");
     try
       {
            Repository.Clone(_repoUrl, _repoPath);
           _infoLogger("Repository cloned successfully.");
 }
              catch (Exception ex)
        {
        _errorLogger("ERROR cloning repository: " + ex.Message);
  throw;
           }
          }
}

        /// <summary>
        /// Commit and push changes with retry logic and fallback to force-push.
        /// </summary>
        private int CommitAndPush(string backupFileName)
        {
  try
   {
              using (var repo = new Repository(_repoPath))
                {
Commands.Stage(repo, backupFileName);

          if (repo.RetrieveStatus().IsDirty)
    {
       _infoLogger("Committing changes...");
       var author = new Signature("BackupBot", "backup@domain.com", DateTimeOffset.Now);
repo.Commit(string.Format("Daily backup {0}", DateTime.Today.ToString("yyyy-MM-dd")), author, author);

     // Ensure internet available
      WaitForInternet();

  // Attempt push with retries
        return PushWithRetry(repo);
   }
        else
         {
       _infoLogger("No changes to commit.");
       return 0;
            }
     }
            }
catch (Exception ex)
{
      _errorLogger("ERROR during commit and push: " + ex.Message);
    return 6;
            }
        }

  /// <summary>
        /// Push to remote with retry logic and fallback to force-push.
      /// </summary>
 private int PushWithRetry(Repository repo)
        {
    var remote = repo.Network.Remotes["origin"];

            for (int attempt = 1; attempt <= MaxRetries; attempt++)
  {
    try
    {
                    // Fetch latest from origin
       var fetchOptions = new FetchOptions();
        fetchOptions.CredentialsProvider = new CredentialsHandler(
     (url, usernameFromUrl, types) =>
           new UsernamePasswordCredentials
           {
          Username = "x-access-token",
         Password = _gitToken
                });

    _infoLogger(string.Format("Fetching remote changes (attempt {0}/{1})...", attempt, MaxRetries));
        Commands.Fetch(repo, remote.Name, remote.FetchRefSpecs.Select(x => x.Specification).ToList(), fetchOptions, null);

      // Merge if remote branch exists
       string remoteBranchName = "origin/" + repo.Head.FriendlyName;
     var remoteBranch = repo.Branches[remoteBranchName];

      if (remoteBranch != null && !remoteBranch.Tip.Sha.Equals(repo.Head.Tip.Sha))
      {
 _infoLogger("Merging remote branch " + remoteBranchName + " into local branch...");
          var author = new Signature("BackupBot", "backup@domain.com", DateTimeOffset.Now);
 MergeOptions mergeOptions = new MergeOptions();
            MergeResult mergeResult = repo.Merge(remoteBranch, author, mergeOptions);

  if (mergeResult.Status == MergeStatus.Conflicts)
              {
            _errorLogger("Merge conflicts detected. Aborting push. Resolve conflicts manually.");
   return 5;
   }

   _infoLogger("Merge status: " + mergeResult.Status);
 }

           // Push (normal)
     var pushOptions = new PushOptions();
        pushOptions.CredentialsProvider = new CredentialsHandler(
 (url, usernameFromUrl, types) =>
        new UsernamePasswordCredentials
         {
           Username = "x-access-token",
        Password = _gitToken
      });

 try
        {
 _infoLogger("Pushing to GitHub...");
       repo.Network.Push(remote, "refs/heads/" + repo.Head.FriendlyName, pushOptions);
   _infoLogger("Push succeeded.");
      return 0;
      }
           catch (LibGit2Sharp.NonFastForwardException nfex)
         {
         // Remote contains commits not present locally
          _warningLogger("NonFastForwardException on attempt " + attempt + ": " + nfex.Message);

         if (attempt == MaxRetries)
          {
        // Last attempt: try force-push as fallback
    _warningLogger("Attempting force-push as final fallback...");
     try
{
       repo.Network.Push(remote, "+" + "refs/heads/" + repo.Head.FriendlyName, pushOptions);
    _infoLogger("Force-push succeeded.");
     return 0;
          }
         catch (Exception exForce)
         {
  _errorLogger("Force-push failed: " + exForce.Message);
              return 6;
       }
          }
    else
        {
      _warningLogger("Retrying in 5 seconds...");
        Thread.Sleep(5000);
      }
        }
          }
       catch (Exception ex)
          {
  _errorLogger("ERROR on attempt " + attempt + ": " + ex.Message);
    if (attempt < MaxRetries)
   {
           _warningLogger("Retrying in 5 seconds...");
               Thread.Sleep(5000);
          }
    else
   {
         return 6;
          }
         }
  }

            return 6; // Should not reach here
   }

        /// <summary>
        /// Wait for internet connectivity with retries.
     /// </summary>
        private void WaitForInternet()
      {
            int retryCount = 0;
const int maxRetries = 12; // 3 minutes with 15-second intervals

        _infoLogger("Checking internet connectivity...");
        while (!IsInternetAvailable())
            {
      retryCount++;
       if (retryCount > maxRetries)
      {
             _errorLogger("ERROR: Internet unavailable after multiple retries.");
            throw new InvalidOperationException("Internet connection required for backup push.");
            }

      _warningLogger("No internet. Retrying in 15s... (attempt " + retryCount + "/" + maxRetries + ")");
   Thread.Sleep(InternetRetryIntervalMs);
     }
 _infoLogger("Internet OK.");
        }

        /// <summary>
        /// Check if internet is available by pinging a public DNS server.
     /// </summary>
        private bool IsInternetAvailable()
        {
 try
   {
         using (var p = new Ping())
     {
          PingReply reply = p.Send(InternetCheckHost, InternetTimeoutMs);
     return reply != null && reply.Status == IPStatus.Success;
     }
            }
         catch (Exception ex)
      {
      _warningLogger("Internet check failed: " + ex.Message);
         return false;
      }
        }
    }
}
