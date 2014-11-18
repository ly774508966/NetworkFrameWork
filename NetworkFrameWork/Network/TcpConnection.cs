
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;

#region disableWarning
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#endregion

namespace NGE.Network
{
	/// <summary>
	/// Tcp 连接类
	/// </summary>
	public sealed class TcpConnection : SocketConnection
	{
		short m_magicnumOfCurrentPacket;
		int m_lengthOfCurrentPacket;
		int m_packetIDOfCurrentPacket;
		int m_dispatcherIDOfCurrentPacket;

		byte[] m_lastPacketData;
		

		int m_lastRecvPacketSerialNumber = 0;
		int m_lastSendPacketSerialNumber = 0;
#if _NC_Compress
		TeaEncryption m_encrypt;
		TeaEncryption m_encryptfirst;
#else
        object m_encrypt;
        object m_encryptfirst;
#endif
		byte[] m_firstkey;
		object m_sendlock = new object();
		//bool m_delayHandlePacket = false;
		bool m_compressneedchecksum = true;
		

		public TcpConnection()
			: this(null, false)
		{
			//if (m_socket == null)
				//CreateSocket();
		}

		internal TcpConnection(Socket socket, bool secure)
			: base(socket, secure)
		{
			if (m_socket != null)
				SetSocketDefaultOption();
		}

        /*
         * 发送一个消息包
         * **/
        public override void SendPacket(byte[] packet, bool encrypt_if_need)
        {
            //SendPacket(packet, false);
            this.Send(packet);
        }

        protected override void OnReceivedDataCallBack(byte[] data, int length)
        {
            //string dataString = ArrayUtility.HexArrayToString(data);
            //UnityEngine.Debug.Log(dataString);

            try
            {
                //取得包头 - 包头是二字节 - 表示整个消息的长度
                byte[] buffer = new byte[2];
                // 取消息头(长度)
                short msgLen = ArrayUtility.GetShort(data, 0);
                byte[] msgBuf = new byte[msgLen];
                Buffer.BlockCopy(data, 2, msgBuf, 0, msgLen);
                MemoryStream mem = new MemoryStream(msgBuf);
                noteGetData(mem);               
            }
            catch (SocketException pEP)
            {
                UnityEngine.Debug.Log("OnReceivedDataCallBack SocketException error:" + pEP.Message);
            }
            catch (Exception exp)
            {
                UnityEngine.Debug.Log("OnReceivedDataCallBack Exception error:" + exp.Message);
            }
        }
       

		protected override void CreateSocket()
		{
			if (m_socket != null)
			{
				try { m_socket.Shutdown(SocketShutdown.Both); }
				catch { }
				try { m_socket.Close(); }
				catch { }
			}
			m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			m_socket.Blocking = false;
		}

		public override void Update(object state)
		{
			if (!object.ReferenceEquals(m_PacketHandlerCallbackArgument, state))
				m_PacketHandlerCallbackArgument = state;
			if (m_isAsyncNetCallBack)
				return;
			base.Update(state);
		}

        protected override void StartSecureValidate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

		public override void CloseConnection()
		{
			base.CloseConnection();
			m_lastRecvPacketSerialNumber = 0;
			m_lastSendPacketSerialNumber = 0;
			m_magicnumOfCurrentPacket = 0;
			m_lengthOfCurrentPacket = 0;
			m_packetIDOfCurrentPacket = 0;
			m_dispatcherIDOfCurrentPacket = 0;
		}

		protected override void Dispose(bool disposing)
		{
			m_lastPacketData = null;
			m_encrypt = null;
			m_encryptfirst = null;
			m_firstkey = null;		
			base.Dispose(disposing);
		}
	}
}
