using System.Collections.Generic;
using System.Diagnostics;

namespace SimCore
{
    public interface ISimExeItem
    {
        SimExeKind Kind { get; }
        string File { get; }
        string Dir { get; }
        List<string> Projects { get; }
        Process Proc { get; }
        bool IsRunning { get; }
        OneWindow Main { get; }
        OneLoad Loaded { get; }
        void Start();
        void Stop();
    }
}