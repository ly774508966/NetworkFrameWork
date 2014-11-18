using System;
using System.Collections.Generic;
using UnityEngine;
using NGE.Network;

public class NetCheck
{
	#region Singleton
	static NetCheck m_singleton;
	static public NetCheck Singleton
	{
		get
		{
			if (m_singleton == null)
			{
				m_singleton = new NetCheck();
			}
			return m_singleton;
		}
	}
	#endregion

	private bool m_sendFlag = true;
	private bool m_pauseSend = false;
	private float m_startTime = 0f;
	private int m_count = 0;
	private const float m_difSendTime = 1f;
	private const float m_difRcvTime = 10f;
	private const int CountMax = 3;
	private float m_checkConnectStartTime = 0f;
	private float m_checkConnectWaitTime = 2f;

	public delegate void UICallBack();
	public UICallBack m_callback;

	public NetCheck()
	{

	}

	public void OnUpdate()
	{
		if ((0f != m_checkConnectStartTime) && (Time.time - m_checkConnectStartTime > 0f))
		{
			m_checkConnectStartTime = 0f;
            //UIReconnect uiReconnect = UIManager.Singleton.GetUI<UIReconnect>();
            //if (!uiReconnect.IsVisiable())
            //{
            //    uiReconnect.ShowWindow();
            //}
			return;
		}
		if (!NetProxy.Singleton.IsLogined)
		{
			return;
		}
		if (m_pauseSend)
		{
			return;
		}
		if (!m_sendFlag && (Time.time - m_startTime > m_difRcvTime))
		{
			m_count++;
			if (m_count >= CountMax)
			{
				m_count = 0;
                //UINetCheck uiNetCheck = UIManager.Singleton.GetUI<UINetCheck>();
                //string showStr = GameTable.StringTableAsset.GetString(ENStringIndex.NetConnectTimeOut);
                //uiNetCheck.InitInfo(showStr);
                //if (!uiNetCheck.IsVisiable())
                //{
                //    uiNetCheck.ShowWindow();
                //}
			}
			else
			{
				m_sendFlag = true;
			}
		}
		if (m_sendFlag && (Time.time - m_startTime > m_difSendTime))
		{
			
		}
	}
	
	public void ResetNet()
	{
		m_sendFlag = true;
		m_pauseSend = false;
		m_startTime = 0f;
		m_count = 0;
	}
	public void OnResume()
	{
		if (NetworkReachability.NotReachable != Application.internetReachability)
		{
			if (NetProxy.Singleton.IsLogined)
			{
				
			}
		}
	}
	public void PauseSend()
	{
		m_pauseSend = true;
	}

	public void CloseNetConnect()
	{
		//断开服务器连接
		ClientNet.Singleton.CloseConnect();
		ResetNet();
	}
}
