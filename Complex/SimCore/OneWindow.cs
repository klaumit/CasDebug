using System;

namespace SimCore
{
    public record OneWindow(
        IntPtr Handle,
        string Class,
        string Text,
        uint ProcId,
        uint ThreadId,
        IntPtr? Parent = null
    );
}