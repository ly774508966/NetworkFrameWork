/************************************************************************
*	@brief	 ：提供数据转换接口 struct 和 byte* 之间进行转换.
*	                C++与C#通信数据转换. 
************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;

[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]

public class DataFormat 
{
    // 结构体转byte数组 -> 将数据转换成字节流然后发送
    public static byte[] StructToBytes(object structObj)
    {
        int size = Marshal.SizeOf(structObj);
        IntPtr buffer = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.StructureToPtr(structObj, buffer, false);
            byte[] bytes = new byte[size];
            Marshal.Copy(buffer, bytes, 0, size);
            return bytes;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    // byte数组转结构体
    // byte数组 -> 结构体类型 -> 转换后的结构体
    // 讲接收到的字节流转换成结构体
    public static object BytesToStruct(byte[] bytes, Type strcutType)
    {
        int size = Marshal.SizeOf(strcutType);
        IntPtr buffer = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.Copy(bytes, 0, buffer, size);
            return Marshal.PtrToStructure(buffer, strcutType);
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    // 返回data1和data2合拼的byte[]
    public static byte[] ConnectBytes(byte[] data1, byte[] data2)
    {
        byte[] nCon = null;
        if (data2 == null)
        {
            nCon = new byte[data1.Length];
            data1.CopyTo(nCon, 0);
        }
        else
        {
            nCon = new byte[data1.Length + data2.Length];
            data1.CopyTo(nCon, 0);
            data2.CopyTo(nCon, data1.Length);
        }

        return nCon;
    }

    public static void WriteToFile(string fileName, byte[] data)
    {
        try
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
        catch
        {
            //Debug.Log("写文件失败");
        }
    }

    public static void ReadFromFile(string fileName, ref byte[] data)
    {
        //         try
        //         {
        //             FileInfo fi = new FileInfo(fileName);
        //             if (fi.Exists)
        //             {
        //                 FileStream fs = new FileStream(fileName, FileMode.Open);
        //                 fs.Read(data, 0, (int)(fi.Length));
        //                 fs.Close();
        //             }
        //             else
        //             {
        //                 Debug.Log("文件不存在");
        //             }
        // 
        //         }
        //         catch
        //         {
        //             Debug.Log("读文件失败");
        //         }
    }

    public static void WriteToFile(string fileName, string data)
    {
        DataFormat.WriteToFile(fileName, DataFormat.stringToByte(data));
    }

    // 字符串转字节数组
    public static byte[] stringToByte(string str)
    {
        return System.Text.Encoding.UTF8.GetBytes(str);
    }

    // 字节数组转字符串
    public static string ByteToString(byte[] byteArray)
    {
        if (null == byteArray)
        {
            return "";
        }

        return System.Text.Encoding.UTF8.GetString(byteArray);
    }

    // int 转byte数组
    public static byte[] intToByte(int i)
    {
        byte[] bt = System.BitConverter.GetBytes(i);
        return bt;
    }

    // byte数组转int
    public static int byteToInt(byte[] b)
    {
        int n = System.BitConverter.ToInt32(b, 0);
        return n;
    }

    // byte数组转int16
    public static int byteToInt16(byte[] b)
    {
        int n = System.BitConverter.ToUInt16(b, 0);
        return n;
    }

    // float 转byte数组
    public static byte[] floatToByte(float i)
    {
        byte[] bt = System.BitConverter.GetBytes(i);
        return bt;
    }

    // byte数组转float
    public static float byteTofloat(byte[] b)
    {
        float n = System.BitConverter.ToSingle(b, 0);
        return n;
    }

    // short 转byte数组
    public static byte[] shortToByte(short s)
    {
        byte[] bt = System.BitConverter.GetBytes(s);
        return bt;
    }

    public static byte[] byteToByte(byte b)
    {
        byte[] bt = new byte[1];
        bt[0] = b;
        return bt;
    }
}