using UnityEngine;
using System.Collections.Generic;

using DarkRift.Logging;

namespace DarkRift.Server.Unity
{
    public sealed class UnityConsoleLogWriter : LogWriter
    {
        public UnityConsoleLogWriter(string name)
            : base (name)
        {

        }

        public override void WriteEvent(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Trace:
                case LogType.Info:
                    Debug.Log(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Error:
                case LogType.Fatal:    
                    Debug.LogError(message);
                    break;
            }
        }
    }
}
