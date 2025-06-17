using System;

namespace SimCore
{
    public class OneWindow
    {
        public OneWindow(IntPtr hWnd, string clazz, string title, uint procId, uint threadId, IntPtr? parent)
        {
            Handle = hWnd;
            Class = clazz;
            Text = title;
            ProcId = procId;
            ThreadId = threadId;
            Parent = parent;
        }

        public IntPtr Handle { get; init; }
        public string Class { get; init; }
        public string Text { get; init; }
        public uint ProcId { get; init; }
        public uint ThreadId { get; init; }
        public IntPtr? Parent { get; init; }
    }
}