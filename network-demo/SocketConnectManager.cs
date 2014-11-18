using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using clientmessage;
using NGE.Network;
using System.IO;
using ProtoBuf;

namespace game.appManager.connectManager
{
    /*
     * socket链接管理器
     * **/
    class SocketConnectManager
    {
        public static SocketConnectManager _instance;
        private ClientNet _clientNet = null;
       
        public static SocketConnectManager instance
        {
            get 
            {
                if (null == _instance)
                    _instance = new SocketConnectManager();
                return _instance;
            }
        }

        public SocketConnectManager()
        {  
            //初始化监听
            NetProxy.Singleton.MainConnect.ConnectionGetData += 
                new ConnectionGetDataCallback(OnConnectGetData);
        }

        /*发起连接请求
         * **/
        public void ConnectIP(string ip,int port)
        {
            ClientNet cn = ClientNet.Singleton;
            cn.ConnectIP(GameConfig.SERVER_HOST, GameConfig.SERVER_PORT);
        }

        /*
        * 向服务端发送数据
        * **/
        public void sendToServer(global::ProtoBuf.IExtensible netMsg)
        {
            Type msgType= netMsg.GetType();
            uint msgId = NetMsgMap.instance.getMsgIdByType(msgType);

            byte[] byteArr
               = Parse<global::ProtoBuf.IExtensible>(netMsg, msgId);

            if (null == _clientNet)
                _clientNet = ClientNet.Singleton;

            _clientNet.SendPacket(byteArr);
        }

        /*
         * 抓到socket发来的消息。反序列化并交给command处理
         * **/
        private void OnConnectGetData(MemoryStream dataStream)
        {
            try
            {
                MsgClient msgClient = Serializer.Deserialize<MsgClient>(dataStream);
                MemoryStream mems = new MemoryStream(msgClient.msg);

                Action<MemoryStream> handlerAction = NetMsgMap.instance.getActionById(msgClient.type);
                if (null != handlerAction)
                    handlerAction(mems);
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.Log("socket get message error!");
            }
        }


        //客户端向服务端发消息打包
        private byte[] Parse<T>(T pMsg, uint type)
            where T : global::ProtoBuf.IExtensible
        {
            byte[] pByteMsg = null;
            using (MemoryStream _MS = new MemoryStream())
            {
                Serializer.Serialize<T>(_MS, pMsg);
                pByteMsg = _MS.ToArray();
            }

            MsgClient xMsg = new MsgClient();
            xMsg.type = type;
            xMsg.msg = pByteMsg;

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize<MsgClient>(ms, xMsg);
            byte[] byteArr = ms.ToArray();

            byte[] pMsgHeadLen = DataFormat.shortToByte((short)byteArr.Length);
            byte[] pTrueMsg = DataFormat.ConnectBytes(pMsgHeadLen, byteArr);
            return pTrueMsg;
        } 
    }
}
