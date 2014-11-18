using NGE.Network;
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#region disableWarning
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#endregion


public partial class MessageProcess
{
	static private MessageProcess m_singleton;
    bool m_isInitedConnect = false;

	static public MessageProcess Singleton
	{
		get
		{
			if (m_singleton == null)
			{
				m_singleton = new MessageProcess();
			}
			return m_singleton;
		}
	}
	public MessageProcess()
	{

	}
	public void Init()
	{
		if (m_isInitedConnect)
		{
			return;
		}
        m_isInitedConnect = true;        
    }
}