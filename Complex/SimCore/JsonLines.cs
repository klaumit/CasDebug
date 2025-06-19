using System;
using System.IO;
using NetfXtended.Core;

namespace SimCore
{
    public sealed class JsonLines<T> : IDisposable
    {
        private readonly StreamWriter _writer;
        private bool _first;

        public JsonLines(string file)
        {
            _first = true;
            _writer = File.CreateText(file);
            _writer.WriteLine("[");
        }

        public void Add(T item)
        {
            if (_first)
                _first = false;
            else
            {
                _writer.Write(",");
                _writer.WriteLine();
            }
            var line = Jsons.WritePlain(item);
            _writer.Write(line);
        }

        public void Dispose()
        {
            _writer.WriteLine();
            _writer.WriteLine("]");
            _writer.Flush();
            _writer.Dispose();
        }
    }
}