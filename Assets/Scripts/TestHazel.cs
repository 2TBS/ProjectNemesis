using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hazel;
using Hazel.Udp;
using System.Net;
using System;

public class TestHazel : MonoBehaviour {

    static ConnectionListener listener;

    // Use this for initialization
    void Start () {
        listener = new UdpConnectionListener(IPAddress.Any, 4296);

        listener.NewConnection += NewConnectionHandler;

        Debug.Log("Starting server!");

        listener.Start();

        

        listener.Close();
    }

    static void NewConnectionHandler(object sender, NewConnectionEventArgs args)
    {
        Console.WriteLine("New connection from " + args.Connection.EndPoint.ToString());

        //args.Connection.DataReceived += DataReceivedHandler;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
