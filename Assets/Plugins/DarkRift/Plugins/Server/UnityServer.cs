using UnityEngine;
using System.Collections;

using DarkRift.Server;
using DarkRift;
using System;
using DarkRift.Logging;
using System.IO;

namespace DarkRift.Server.Unity
{
    [AddComponentMenu("DarkRift/Server")]
	public sealed class UnityServer : MonoBehaviour
	{
        #region Basic settings

        [SerializeField]
        [Tooltip("The port the server will listen on.")]
        public ushort Port = 4296;

        [SerializeField]
        [Tooltip("The protocol the server will listening on.")]
        public TransportProtocol Protocol = TransportProtocol.Udp;

        [SerializeField]
        [Tooltip("The IP protocol version the server will listen on.")]
        public IPVersion IPVersion = IPVersion.IPv4;

        [SerializeField]
        [Tooltip("Indicates whether the server will be created in the OnEnable method.")]
        bool createOnEnable = true;

        #endregion

        #region Server settings

        /// <summary>
        ///     The location DarkRift will store persistant data.
        /// </summary>
        [HideInInspector]
        public string DataDirectory = Path.DirectorySeparatorChar + "Data";

        #endregion

        #region Logging settings

        [Header("Logging Settings")]

        [SerializeField]
        [Tooltip("Indicates whether the recommended logging settings will be used.")]
        public bool UseRecommended = true;

        /// <summary>
        ///     The location that log files will be placed when using recommended logging settings.
        /// </summary>
        public string DefaultLogFileLocation = Path.DirectorySeparatorChar + "Logs";

        /// <summary>
        ///     The log writers trace messages will be written to.
        /// </summary>
        [HideInInspector]
        public LogWriter[] TraceWriters = new LogWriter[0];

        /// <summary>
        ///     The log writers trace messages will be written to.
        /// </summary>
        [HideInInspector]
        public LogWriter[] InfoWriters = new LogWriter[1];

        /// <summary>
        ///     The log writers trace messages will be written to.
        /// </summary>
        [HideInInspector]
        public LogWriter[] ErrorWriters = new LogWriter[0];

        /// <summary>
        ///     The log writers trace messages will be written to.
        /// </summary>
        [HideInInspector]
        public LogWriter[] WarningWriters = new LogWriter[0];

        /// <summary>
        ///     The log writers trace messages will be written to.
        /// </summary>
        [HideInInspector]
        public LogWriter[] FatalWriters = new LogWriter[0];

        [SerializeField]
        [Tooltip("The level that extra exception detail will be sent to.")]
        public LogType ExceptionsTo = LogType.Trace;

        #endregion

        #region Plugin settings

        [Header("Plugin Settings")]

        [SerializeField]
        [Tooltip("Whether the server should scan the specified directory for plugins.")]
        public bool LoadFromDirectory = false;

        [SerializeField]
        [Tooltip("The directory that should be searched for plugins.")]
        public string PluginDirectory = "Plugins";

        /// <summary>
        ///     The plugins that should be loaded.
        /// </summary>
        [HideInInspector]
        public Type[] PluginTypes;

        /// <summary>
        ///     The plugin files that should be loaded.
        /// </summary>
        [HideInInspector]
        public string[] PluginFiles;

        #endregion

        #region Database settings

        /// <summary>
        ///     The databases that the server will connect to.
        /// </summary>
        [HideInInspector]
        public ServerSpawnData.DatabaseSettings.DatabaseConnectionData[] Databases;

        #endregion

        /// <summary>
        ///     The actually server.
        /// </summary>
        public DarkRiftServer Server { get; private set; }

        void OnEnable()
        {
            //If createOnEnable is selected create a server
            if (createOnEnable)
                Create();
        }

        void Update()
        {
            //Execute all queued dispatcher tasks
            Server.ExecuteDispatcherTasks();
        }

        /// <summary>
        ///     Creates the server.
        /// </summary>
        public void Create()
        {
            if (Server != null)
                throw new InvalidOperationException("The server has already been created! (Is CreateOnEnable enabled?)");

            ServerSpawnData spawnData = new ServerSpawnData(Port, Protocol, IPVersion);

            spawnData.Data.DataDirectory = DataDirectory;

            if (UseRecommended)
            {
                FileLogWriter fileLogWriter = new FileLogWriter("FileLogWriter", DefaultLogFileLocation);
                UnityConsoleLogWriter unityConsoleLogWriter = new UnityConsoleLogWriter("UnityConsoleLogWriter");

                spawnData.Logging.TraceWriters = new LogWriter[] { fileLogWriter };
                spawnData.Logging.InfoWriters = InfoWriters = new LogWriter[] { unityConsoleLogWriter, fileLogWriter };
                spawnData.Logging.WarningWriters = WarningWriters = new LogWriter[] { unityConsoleLogWriter, fileLogWriter };
                spawnData.Logging.ErrorWriters = ErrorWriters = new LogWriter[] { unityConsoleLogWriter, fileLogWriter };
                spawnData.Logging.FatalWriters = FatalWriters = new LogWriter[] { unityConsoleLogWriter, fileLogWriter };
            }
            else
            {
                spawnData.Logging.TraceWriters = TraceWriters;
                spawnData.Logging.InfoWriters = InfoWriters;
                spawnData.Logging.WarningWriters = WarningWriters;
                spawnData.Logging.ErrorWriters = ErrorWriters;
                spawnData.Logging.FatalWriters = FatalWriters;
            }

            spawnData.Logging.ExceptionsTo = ExceptionsTo;

            spawnData.Plugins.LoadFromDirectory = LoadFromDirectory;
            spawnData.Plugins.PluginDirectory = PluginDirectory;
            spawnData.Plugins.PluginTypes = PluginTypes;
            spawnData.Plugins.PluginFiles = PluginFiles;

            spawnData.Databases.Databases = Databases;

            Server = new DarkRiftServer(spawnData);
            Server.Start();
        }

        private void OnDisable()
        {
            Close();
        }

        /// <summary>
        ///     Closes the server.
        /// </summary>
        public void Close()
        {
            Server.Dispose();
        }
    }
}
