using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Utils.CSharp;

/// <summary>
/// Allows processes to be automatically killed if this parent process unexpectedly quits.
/// This feature requires Windows 8 or greater. On Windows 7, nothing is done.</summary>
/// <remarks>References: https://stackoverflow.com/questions/3342941/kill-child-process-when-parent-process-is-killed/4657392#37034966</remarks>
public static class ProcessTracker
{
    /// <summary>
    /// Add the process to be tracked. If our current process is killed, the child processes
    /// that we are tracking will be automatically killed, too. If the child process terminates
    /// first, that's fine, too.</summary>
    /// <param name="process"></param>
    public static void AddProcess(Process process)
    {
        if (s_jobHandle != IntPtr.Zero)
        {
            bool success = AssignProcessToJobObject(s_jobHandle, process.Handle);
            if (!success && !process.HasExited)
                throw new Win32Exception();
        }
    }

    static ProcessTracker()
    {
        // This feature requires Windows 8 or later. To support Windows 7 requires registry settings to be added if you are using Visual Studio plus an app.manifest change.
        if (Environment.OSVersion.Version < new Version(6, 2))
            return;

        // The job name is optional (and can be null) but it helps with diagnostics. If it's not null, it has to be unique. Use SysInternals' Handle command-line utility: handle -a ProcessTracker
#if NETSTANDARD2_0
        string jobName = "ProcessTracker" + Process.GetCurrentProcess().Id;
#else
        string jobName = "ProcessTracker" + Environment.ProcessId;
#endif
        s_jobHandle = CreateJobObject(IntPtr.Zero, jobName);

        var info = new JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            // This is the key flag. When our process is killed, Windows will automatically close the job handle, and when that happens, we want the child processes to be killed, too.
            LimitFlags = JOBOBJECTLIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
        };

        var extendedInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            BasicLimitInformation = info
        };

        int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
        IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);
        try
        {
            Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);

            if (!SetInformationJobObject(s_jobHandle, JobObjectInfoType.ExtendedLimitInformation,
                extendedInfoPtr, (uint)length))
            {
                throw new Win32Exception();
            }
        }
        finally
        {
            Marshal.FreeHGlobal(extendedInfoPtr);
        }
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string name);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetInformationJobObject(IntPtr job, JobObjectInfoType infoType, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

    // Windows will automatically close any open job handles when our process terminates. This can be verified by using SysInternals' Handle utility. When the job handle is closed, the child processes will be killed.
    private static readonly IntPtr s_jobHandle;
}

public enum JobObjectInfoType
{
    AssociateCompletionPortInformation = 7,
    BasicLimitInformation = 2,
    BasicUIRestrictions = 4,
    EndOfJobTimeInformation = 6,
    ExtendedLimitInformation = 9,
    SecurityLimitInformation = 5,
    GroupInformation = 11
}

[StructLayout(LayoutKind.Sequential)]
public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
{
    public long PerProcessUserTimeLimit;
    public long PerJobUserTimeLimit;
    public JOBOBJECTLIMIT LimitFlags;
    public UIntPtr MinimumWorkingSetSize;
    public UIntPtr MaximumWorkingSetSize;
    public uint ActiveProcessLimit;
    public long Affinity;
    public uint PriorityClass;
    public uint SchedulingClass;
}

[Flags]
public enum JOBOBJECTLIMIT : uint
{
    JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000
}

[StructLayout(LayoutKind.Sequential)]
public struct IO_COUNTERS
{
    public ulong ReadOperationCount;
    public ulong WriteOperationCount;
    public ulong OtherOperationCount;
    public ulong ReadTransferCount;
    public ulong WriteTransferCount;
    public ulong OtherTransferCount;
}

[StructLayout(LayoutKind.Sequential)]
public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
{
    public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
    public IO_COUNTERS IoInfo;
    public UIntPtr ProcessMemoryLimit;
    public UIntPtr JobMemoryLimit;
    public UIntPtr PeakProcessMemoryUsed;
    public UIntPtr PeakJobMemoryUsed;
}
