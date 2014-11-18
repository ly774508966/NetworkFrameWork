//////////////////////////////////////////////////////////////////////////
//
//	file path:	E:\Codes\XProject_svn\Branch_West\Assets\Scripts\VirtualServer
//	created:	2014-5-6
//	author:		Mingzhen Zhang
//
//////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using NGE.Network;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class NetProxy
{
	#region Singleton
	static NetProxy m_singleton;
	static public NetProxy Singleton
	{
		get
		{
			if (m_singleton == null)
			{
				m_singleton = new NetProxy();
			}
			return m_singleton;
		}
	}
	#endregion

    private LinkedList<byte[]> m_messageByteArrayList = null;
	private TcpConnection m_mainConnect = new TcpConnection();
	public TcpConnection MainConnect { get { return m_mainConnect; } }
    private string ServerToken { get; set; }

	private NetProxy()
	{
		ConnectState = ENConnectState.enClosed;
		
		// 成功连接时执行OnMainConnected函数
		m_mainConnect.Connected += new ConnectionCallback(OnMainConnected);
		// 连接失败时执行OnConnectFailed函数
		m_mainConnect.ConnectionFailed += new ConnectionFailedCallback(OnConnectFailed);
      
		m_mainConnect.UseAsyncNetCallBack = false;
	}

	public void SendCacheMessage()
	{
        if (null == m_messageByteArrayList)
		{
			return;
		}
        foreach (byte[] p in m_messageByteArrayList)
		{
			SendToServer(p);
		}
		ClearCacheMessage();
		m_isLogined = true;
	}

	private void ClearCacheMessage()
	{
        m_messageByteArrayList = null;
	}
	
	// 连接成功时执行的函数
	private void OnMainConnected(ConnectionArgs args, object state)
	{
		UnityEngine.Debug.Log("Connected success!!!!");
		ConnectState = ENConnectState.enConnecting;		
		ServerToken = string.Empty;
	}

	// 连接失败时执行的函数
	private void OnConnectFailed(ConnectionArgs args, ErrorArgs error, object state)
	{
        UnityEngine.Debug.Log("Connected Lost or failed!!!!");
		ConnectState = ENConnectState.enClosed;
	}
   

    public void SendToServer(byte[] p)
    {
        //Debug.LogError(p.PacketID);
        m_mainConnect.SendPacket(p,false);
    }

	private bool m_isLogined = false;
	public bool IsLogined { get { return m_isLogined; } }

    public void OnClientMessage(byte[] p, bool isFirst)
    {
        if (NetworkReachability.NotReachable == Application.internetReachability)
        {
            //无网络连接
            OnNetworkNotReachable();
            return;
        }
        //Debug.Log("PacketID = " + p.PacketID);
        //发送消息
        /*if (m_isLogined)
        {
            SendToServer(p);
            return;
        }
        if (!SendWithLogin(p))
        {
            //获取不到账号信息
            OnNoAccount();
        }*/
        SendToServer(p);
    }

    private void PushMessage(byte[] p, bool isFirst)
    {
        if (null == m_messageByteArrayList)
        {
            m_messageByteArrayList = new LinkedList<byte[]>();
        }
        
        if (isFirst)
        {
            m_messageByteArrayList.AddFirst(p);
        }
        else
        {
            m_messageByteArrayList.AddLast(p);
        }
    }


	enum ENLoginType
	{
		enNone,
		enUserName,
		enIdentifyID,
        enFaceBook,
	}
   
    public void ConnectServer(string ip, int port)
	{
		if (m_mainConnect.IsConnected)
		{
			return;
		}
		MessageProcess.Singleton.Init();
        m_mainConnect.Connect(ip, port, false);
	}
	public enum ENConnectState
	{
		enClosed,
		enCheckToken,
		enStart,
		enConnecting,
	}
	public ENConnectState ConnectState { get; private set; }
	private IEnumerator Update()
	{
		ConnectState = ENConnectState.enStart;
		while (ENConnectState.enClosed != ConnectState)
		{
			m_mainConnect.Update(this);
			yield return null;
		}
	}

	private bool ProcessLoginInfo(byte[] values, out string serverToken, out string serverGUID)
	{
		string valuesStr = Encoding.Default.GetString(values);
		//Hashtable valuesTable = NGUIJson.jsonDecode(valuesStr) as Hashtable;
        Hashtable valuesTable = new Hashtable();
		//返回结果失败
		string result = valuesTable["result"].ToString();
		if (result != "1001")
		{
			serverToken = string.Empty;
			serverGUID = string.Empty;
			return false;
		}
		else
		{
			serverToken = valuesTable["token"] as string;
			serverGUID = valuesTable["guid"] as string;
			return true;
		}
	}

	private void OnCharactorList(ArrayList serverList)
	{
		/*Debug.Log("OnCharactorList");
		UICharacterList uiCharList = UIManager.Singleton.GetUI<UICharacterList>();
		uiCharList.RefreshList(serverList);*/
	}

	private void OnNetworkNotReachable()
	{
		
	}
	private void OnNoAccount()
	{
		
	}
	public void CloseConnect()
	{ 
		m_mainConnect.CloseConnection();
		ConnectState = ENConnectState.enClosed;
		m_isLogined = false;
	}
};