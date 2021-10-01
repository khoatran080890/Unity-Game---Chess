using BestHTTP.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManagement : MonoBehaviour
{
    private SocketManager socketManager;
    private Socket socket;
    protected void Connect(string url, string urlNameSpace)
    {
        SocketOptions option = new SocketOptions();
        option.AutoConnect = false;
        socketManager = new SocketManager(new Uri(url), option);
        socket = socketManager.GetSocket(urlNameSpace);
        socket.On("connected", onConnected);
        socket.On("resp", onResponse);
        socketManager.Open();
    }
    private void onConnected(Socket socket, Packet packet, params object[] args)
    {

    }
    private void onResponse(Socket socket, Packet packet, params object[] args)
    {

    }
}
