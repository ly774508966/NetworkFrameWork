using System;
using System.Collections.Generic;
using System.Text;

namespace NGE.Network
{

	internal sealed class ByteArrayPool
	{
		static Stack<byte[]> s_free_object_array1 = new Stack<byte[]>();//Packet.MaxLength
		static Stack<byte[]> s_free_object_array2 = new Stack<byte[]>();//4k
		static Stack<byte[]> s_free_object_array3 = new Stack<byte[]>();//1k
		const int size_3 = 8192;
		const int size_2 = 4096;
		const int size_1 = 1024;
		static ByteArrayPool m_instance = new ByteArrayPool();
		public static ByteArrayPool Instance
		{
			get
			{
				return m_instance;
			}
		}

		static ByteArrayPool()
		{
			for (int i = 0; i < 50; i++)// init 100 packet memory to free object queue
			{
				s_free_object_array1.Push(new byte[size_1]);
				s_free_object_array2.Push(new byte[size_2]);
				s_free_object_array3.Push(new byte[size_3]);
			}
		}

		public byte[] MallocArray(int size)
		{
			int obj_size = 0;
			Stack<byte[]> array;
			array = FindStackBySize(size, ref obj_size);
			if (array == null)
				return null;
			lock (array)
			{
				if (array.Count == 0)
				{
					for (int i = 0; i < 50; i++)// add 100 packet memory to free object queue
					{
						array.Push(new byte[obj_size]);
					}
				}
				return array.Pop();
			}
		}

		public void Release(byte[] obj)
		{
			if (obj == null)
				return;
			int obj_size = 0;
			Stack<byte[]> array;
			array = FindStackBySize(obj.Length, ref obj_size);
			if (array == null)
				return;

			lock (array)
				array.Push(obj);
		}

		private Stack<byte[]> FindStackBySize(int size, ref int obj_size)
		{
			obj_size = 0;
			Stack<byte[]> array;
			if (size <= size_1)
			{
				obj_size = size_1;
				array = s_free_object_array1;
			}
			else if (size <= size_2)
			{
				obj_size = size_2;
				array = s_free_object_array2;
			}
			else if (size <= size_3)
			{
				obj_size = size_3;
				array = s_free_object_array3;
			}
			else
				return null;
			return array;
		}
	}
}
