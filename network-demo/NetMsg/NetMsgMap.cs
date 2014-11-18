using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using clientmessage;
using System.Collections;
using game.command.login;
using System.IO;
using game.command;
using ProtoBuf;
using frameWork.command.cmdInterface;
using frameWork.command;

class NetMsgMap
{
    private static Dictionary<Type, uint> msgIdMap = null;
    private static Dictionary<uint, Action<MemoryStream>> protolHandlerMap;

    private static NetMsgMap _instance;
    public static NetMsgMap instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new NetMsgMap();
            }
            return _instance;
        }
    }

    /*由类型获取消息id
     * 客户端打包
     * **/
    public uint getMsgIdByType(Type msgType)
    {
        if (null == msgIdMap)
             initMsgMap();
        return msgIdMap[msgType];
    }

    /*由协议id获取协议处理
     *接收消息并交给command处理
    * **/
    public Action<MemoryStream> getActionById(uint protocolId)
    {
        if (null == protolHandlerMap)
            initProtolHandlerMap();
        return protolHandlerMap[protocolId];
    }

    private static void initMsgMap()
    {
        msgIdMap = new Dictionary<Type, uint>();
        //type--id映射，用于客户端打包
        msgIdMap[typeof(MsgLoginReq)] = (uint)ClientMsgType.Msg_LoginReq;
    }

    /*
     * 初始化协议处理字典
     * **/
    private static void initProtolHandlerMap()
    {
        protolHandlerMap = new Dictionary<uint, Action<MemoryStream>>();
        RegisterProtolHandler<LoginAckCommand, MsgLoginAck>((uint)ClientMsgType.Msg_LoginAck);       
    }   

    /*注册协议处理
     * **/
    private static void RegisterProtolHandler<C, T>(uint id)
        where C : GameBaseCommand,new()
    {
        Action<MemoryStream> act = (MemoryStream stream) =>
        {
            T ackMsg = Serializer.Deserialize<T>(stream);
            BaseNotification notification = new BaseNotification();
            notification.data = ackMsg;
            C cmd = new C();
            cmd.excute(notification);
        };

        protolHandlerMap[id] = act;
    }
}

