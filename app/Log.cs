using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Peco.app
{
    internal static class Log
    {
        private static LogForm _form = null!;

        private static readonly object _sync = new();
        private static readonly Queue<string> _buffer = new();
        private const int MAX_BUFFER_LINES = 300;
        private static int _lastDisplayedLine = 0;

        private static Timer _timer = null!;

        public static void Init()
        {
            _timer = new()
            {
                Interval = 1000
            };

            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private static void Timer_Tick(object? sender, EventArgs e)
        {
            if (_form == null)
                return;

            string[] lines;
            int start;

            lock (_sync)
            {
                lines = _buffer.ToArray();
                start = _lastDisplayedLine;
                _lastDisplayedLine = lines.Length;

                if (start > lines.Length)
                    start = 0;
            }

            if (start >= lines.Length)
                return;

            var newLines = lines[start..];

            _form.Invoke(() =>
            {
                if (_form.logTextBox.Lines.Length > MAX_BUFFER_LINES)
                {
                    Clear();
                }

                foreach (var line in newLines)
                {
                    _form.logTextBox.AppendText(line + Environment.NewLine);
                }

                _form.logTextBox.SelectionStart = _form.logTextBox.Text.Length;
                _form.logTextBox.ScrollToCaret();
            });
        }

        public static string Text
        {
            get
            {
                lock (_sync)
                {
                    return string.Join(Environment.NewLine, _buffer);
                }
            }
        }

        public static void Clear()
        {
            lock (_sync)
            {
                _buffer.Clear();
                _lastDisplayedLine = 0;
            }

            _form?.Invoke(() =>
                {
                    _form.logTextBox.Clear();
                });
        }

        public static void HandleOutputData(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Add(e.Data);
            }
        }

        private static void Add(string log)
        {
            if (log == null)
                return;

            lock (_sync)
            {
                _buffer.Enqueue(log);
                while (_buffer.Count > MAX_BUFFER_LINES)
                {
                    _buffer.Dequeue();
                    if (_lastDisplayedLine > 0)
                        _lastDisplayedLine--;
                }
            }
        }

        public static void BindForm(LogForm form)
        {
            _form = form;
        }
    }
}