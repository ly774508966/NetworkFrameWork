using UnityEngine;
using System.Collections;
using NGE.Network;
using System.Net;
using System;
using System.Text;

public class ClientNet
{
	#region Singleton
	static ClientNet m_singleton;
	static public ClientNet Singleton
	{
		get
		{
			if (m_singleton == null)
			{
				m_singleton = new ClientNet();
			}
			return m_singleton;
		}
	}
	public ClientNet()
	{
	}
    public void ConnectIP(string ip, int port)
	{
		Debug.Log("ConnectIP " + ip);
        NetProxy.Singleton.ConnectServer(ip, port);
	}
	#endregion

    private TcpConnection m_mainConnect { get { return NetProxy.Singleton.MainConnect; } }
    public void SendPacket(byte[] p)
    {
        MessageProcess.Singleton.Init();
        NetProxy.Singleton.OnClientMessage(p, false);
    }
	public void CloseConnect()
	{
		Debug.Log("CloseConnect");
		NetProxy.Singleton.CloseConnect();
	}
	
}
