using DarkRift.Dispatching;
using System;
using UnityEngine;

namespace DarkRift.Client.Unity
{
    [AddComponentMenu("DarkRift/Client")]
	public sealed class UnityClient : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The address of the server to connect to.")]
		public string Address = "127.0.0.1";

		[SerializeField]
		[Tooltip("The port the server is listening on.")]
		public ushort Port = 4296;

		[SerializeField]
		[Tooltip("The protocol the server is listening on.")]
		public TransportProtocol Protocol = TransportProtocol.Udp;

        [SerializeField]
        [Tooltip("The IP protocol version to connect using.")]
        public IPVersion IPVersion = IPVersion.IPv4;

        [SerializeField]
        [Tooltip("Indicates whether the client will connect to the server in the Start method.")]
        bool autoConnect = true;

        [SerializeField]
        [Tooltip("Specifies that DarkRift should take care of multithreading and invoke all events from Unity's main thread.")]
        volatile bool invokeFromDispatcher = true;

        [SerializeField]
        [Tooltip("Specifies whether DarkRift should log all data to the console.")]
        volatile bool sniffData = false;

         /// <summary>
        ///     Event fired when a message is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        ///     The ID the client has been assigned.
        /// </summary>
        public uint ID
        {
            get
            {
                return Client.ID;
            }
        }

        /// <summary>
        ///     Returns whether or not this client is connected to the server.
        /// </summary>
        public bool Connected
        {
            get
            {
                return Client.Connected;
            }
        }

		/// <summary>
		/// 	The actual client connecting to the server.
		/// </summary>
		/// <value>The client.</value>
        public DarkRiftClient Client
        {
            get
            {
                return client;
            }
        }

        DarkRiftClient client = new DarkRiftClient();


        /// <summary>
        ///     The dispatcher for moving work to the main thread.
        /// </summary>
        public Dispatcher Dispatcher
        {
            get
            {
                return dispatcher;
            }
        }

        Dispatcher dispatcher = new Dispatcher(true);

        void Awake()
        {
            //Setup routing for MessageReceived event
            Client.MessageReceived += Client_MessageReceived;
        }

		void Start()
		{
            //If auto connect is true then connect to the server
            if (autoConnect)
			    Connect(Address, Port, Protocol, IPVersion);
		}

        void Update()
        {
            //Execute all the queued dispatcher tasks
            Dispatcher.ExecuteDispatcherTasks();
        }

        void OnDestroy()
        {
            //Remove resources
            Close();
        }

        void OnApplicationQuit()
        {
            //Remove resources
            Close();
        }

        /// <summary>
        ///     Connects to a remote server.
        /// </summary>
        /// <param name="ip">The IP address of the server.</param>
        /// <param name="port">The port of the server.</param>
        /// <param name="protocol">The protocol to connect using.</param>
        public void Connect(string ip, int port, TransportProtocol protocol, IPVersion ipVersion)
        {
            Client.Connect(ip, port, protocol, ipVersion);

            if (Connected)
                Debug.Log("Connected to " + ip + " on port " + port + " using " + protocol + " and " + ipVersion + ".");
            else
                Debug.Log("Connection failed to " + ip + " on port " + port + " using " + protocol + " and " + ipVersion + ".");
        }

        /// <summary>
        ///     Connects to a remote asynchronously.
        /// </summary>
        /// <param name="ip">The IP address of the server.</param>
        /// <param name="port">The port of the server.</param>
        /// <param name="protocol">The protocol to connect using.</param>
        /// <param name="callback">The callback to make when the connection attempt completes.</param>
        public void ConnectInBackground(string ip, int port, TransportProtocol protocol, IPVersion ipVersion, Action callback = null)
        {
            Client.ConnectInBackground(
                ip,
                port, 
                protocol, 
                ipVersion, 
                delegate ()
                {
                    if (invokeFromDispatcher)
                        dispatcher.InvokeAsync(callback);
                    else
                        callback.Invoke();

                    dispatcher.InvokeAsync(
                        delegate ()
                        {
                            if (Connected)
                                Debug.Log("Connected to " + ip + " on port " + port + " using " + protocol + " and " + ipVersion + ".");
                            else
                                Debug.Log("Connection failed to " + ip + " on port " + port + " using " + protocol + " and " + ipVersion + ".");
                        }
                    );
                }
            );
        }

        /// <summary>
        ///     Sends a message to the server.
        /// </summary>
        /// <param name="message">The message template to send.</param>
        public void SendMessage(Message message, SendMode sendMode)        //TODO 1 rename to avoid naming collision with Unity?
        {
            Client.SendMessage(message, sendMode);
        }

        /// <summary>
        ///     Invoked when DarkRift receives a message from the server.
        /// </summary>
        /// <param name="sender">THe client that received the message.</param>
        /// <param name="e">The arguments for the event.</param>
        void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //If we're handling multithreading then pass the event to the dispatcher
            if (invokeFromDispatcher)
            {
                Dispatcher.InvokeAsync(
                    () => 
                        {
                            if (sniffData)
                                Debug.Log("Message Received");      //TODO more information!

                            MessageReceived.Invoke(sender, e);
                        }
                );
            }
            else
            {
                if (sniffData)
                {
                    Dispatcher.InvokeAsync(
                        () => Debug.Log("Message Received")      //TODO more information!
                    );
                }

                MessageReceived.Invoke(sender, e);
            }
        }
        
        /// <summary>
        ///     Closes this client.
        /// </summary>
        public void Close()
        {
            Client.Dispose();
        }
	}
}
