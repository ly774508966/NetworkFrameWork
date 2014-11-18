using NGE.Network;
//////////////////////////////////////////////////////////////////////////
//
//	file path:	E:\Codes\XProject\Assets\Scripts\ClientNet
//	created:	2013-10-30
//	author:		Mingzhen Zhang
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class MessageSender
{
	static private MessageSender m_singleton;
	static public MessageSender Singleton
	{
		get
		{
			if (m_singleton == null)
			{
				m_singleton = new MessageSender();
			}
			return m_singleton;
		}
	}

	public MessageSender()
	{
	}

	public void SendSyncPlayerData()
	{
		//同步单机副本记录
		//同步单机装备
		
	}
	public void SendSyncDungeonRecordData()
	{
		
	}
	public void SendEnterMainScene()
	{
		
	}
	public void SendLeaveMainScene()
	{

	}
	public void SendPaymentInfo_GooglePlay(string jsonPurchaseInfo, string signature)
	{
		Debug.Log("SendPaymentInfo_GooglePlay start");
		if (string.IsNullOrEmpty(jsonPurchaseInfo) || string.IsNullOrEmpty(signature))
		{
			return;
		}
		Debug.Log(jsonPurchaseInfo);
		Debug.Log(signature);
		
		//ClientNet.Singleton.SendPacket(message);
	}

	public void SendPaymentInfo_APPStore(string itemName, string base64Token)
	{
		//itemName = "test000001";
		//base64Token = "ewoJInNpZ25hdHVyZSIgPSAiQWlmUGI2OEdXd0M2YktkVVV4WC9MSE9FT01Jd3FwUERzU3h3RVFmN1ptVldudmhWSnpjTVZIbVRFd0hLOW5CODRNQW43TXNTSU1PS0VHK055M0ltc2l1M0VtTml3VUNvcU1IUitaVWpYWkdPNHdiRlY2M0NtcXZ0djlNWE1kQUxDWExRQmlBcnRxTHd0QzY3bFFuOGUvZjNMRVF4OGdkRDBYYXdGem5yODBYU0FBQURWekNDQTFNd2dnSTdvQU1DQVFJQ0NHVVVrVTNaV0FTMU1BMEdDU3FHU0liM0RRRUJCUVVBTUg4eEN6QUpCZ05WQkFZVEFsVlRNUk13RVFZRFZRUUtEQXBCY0hCc1pTQkpibU11TVNZd0pBWURWUVFMREIxQmNIQnNaU0JEWlhKMGFXWnBZMkYwYVc5dUlFRjFkR2h2Y21sMGVURXpNREVHQTFVRUF3d3FRWEJ3YkdVZ2FWUjFibVZ6SUZOMGIzSmxJRU5sY25ScFptbGpZWFJwYjI0Z1FYVjBhRzl5YVhSNU1CNFhEVEE1TURZeE5USXlNRFUxTmxvWERURTBNRFl4TkRJeU1EVTFObG93WkRFak1DRUdBMVVFQXd3YVVIVnlZMmhoYzJWU1pXTmxhWEIwUTJWeWRHbG1hV05oZEdVeEd6QVpCZ05WQkFzTUVrRndjR3hsSUdsVWRXNWxjeUJUZEc5eVpURVRNQkVHQTFVRUNnd0tRWEJ3YkdVZ1NXNWpMakVMTUFrR0ExVUVCaE1DVlZNd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFNclJqRjJjdDRJclNkaVRDaGFJMGc4cHd2L2NtSHM4cC9Sd1YvcnQvOTFYS1ZoTmw0WElCaW1LalFRTmZnSHNEczZ5anUrK0RyS0pFN3VLc3BoTWRkS1lmRkU1ckdYc0FkQkVqQndSSXhleFRldngzSExFRkdBdDFtb0t4NTA5ZGh4dGlJZERnSnYyWWFWczQ5QjB1SnZOZHk2U01xTk5MSHNETHpEUzlvWkhBZ01CQUFHamNqQndNQXdHQTFVZEV3RUIvd1FDTUFBd0h3WURWUjBqQkJnd0ZvQVVOaDNvNHAyQzBnRVl0VEpyRHRkREM1RllRem93RGdZRFZSMFBBUUgvQkFRREFnZUFNQjBHQTFVZERnUVdCQlNwZzRQeUdVakZQaEpYQ0JUTXphTittVjhrOVRBUUJnb3Foa2lHOTJOa0JnVUJCQUlGQURBTkJna3Foa2lHOXcwQkFRVUZBQU9DQVFFQUVhU2JQanRtTjRDL0lCM1FFcEszMlJ4YWNDRFhkVlhBZVZSZVM1RmFaeGMrdDg4cFFQOTNCaUF4dmRXLzNlVFNNR1k1RmJlQVlMM2V0cVA1Z204d3JGb2pYMGlreVZSU3RRKy9BUTBLRWp0cUIwN2tMczlRVWU4Y3pSOFVHZmRNMUV1bVYvVWd2RGQ0TndOWXhMUU1nNFdUUWZna1FRVnk4R1had1ZIZ2JFL1VDNlk3MDUzcEdYQms1MU5QTTN3b3hoZDNnU1JMdlhqK2xvSHNTdGNURXFlOXBCRHBtRzUrc2s0dHcrR0szR01lRU41LytlMVFUOW5wL0tsMW5qK2FCdzdDMHhzeTBiRm5hQWQxY1NTNnhkb3J5L0NVdk02Z3RLc21uT09kcVRlc2JwMGJzOHNuNldxczBDOWRnY3hSSHVPTVoydG04bnBMVW03YXJnT1N6UT09IjsKCSJwdXJjaGFzZS1pbmZvIiA9ICJld29KSW05eWFXZHBibUZzTFhCMWNtTm9ZWE5sTFdSaGRHVXRjSE4wSWlBOUlDSXlNREUwTFRBeExUQTRJREF6T2pFMk9qVXhJRUZ0WlhKcFkyRXZURzl6WDBGdVoyVnNaWE1pT3dvSkluVnVhWEYxWlMxcFpHVnVkR2xtYVdWeUlpQTlJQ0ppTnpRNFpEZzNOMlF5Tm1ObE1Ua3dPV0k1WldNMVl6WmlOelJrWTJFMU5ETm1ZVGM1TXpkaUlqc0tDU0p2Y21sbmFXNWhiQzEwY21GdWMyRmpkR2x2YmkxcFpDSWdQU0FpTVRBd01EQXdNREE1T0RBeE5EQXlOU0k3Q2draVluWnljeUlnUFNBaU1TNHhMakFpT3dvSkluUnlZVzV6WVdOMGFXOXVMV2xrSWlBOUlDSXhNREF3TURBd01EazRNREUwTURJMUlqc0tDU0p4ZFdGdWRHbDBlU0lnUFNBaU1TSTdDZ2tpYjNKcFoybHVZV3d0Y0hWeVkyaGhjMlV0WkdGMFpTMXRjeUlnUFNBaU1UTTRPVEUzT1RneE1UazFNU0k3Q2draWRXNXBjWFZsTFhabGJtUnZjaTFwWkdWdWRHbG1hV1Z5SWlBOUlDSTJOamt3TVRrd09TMUNSREpGTFRReE5qRXRPVVl5TUMxQ05ETXhNRFZHTkVOQk1VSWlPd29KSW5CeWIyUjFZM1F0YVdRaUlEMGdJblJsYzNRd01EQXdNREVpT3dvSkltbDBaVzB0YVdRaUlEMGdJamM1TVRrek5UQXdNeUk3Q2draVltbGtJaUE5SUNKamIyMHVjMmhsYm1kNmFHRnVMblIzSWpzS0NTSndkWEpqYUdGelpTMWtZWFJsTFcxeklpQTlJQ0l4TXpnNU1UYzVPREV4T1RVeElqc0tDU0p3ZFhKamFHRnpaUzFrWVhSbElpQTlJQ0l5TURFMExUQXhMVEE0SURFeE9qRTJPalV4SUVWMFl5OUhUVlFpT3dvSkluQjFjbU5vWVhObExXUmhkR1V0Y0hOMElpQTlJQ0l5TURFMExUQXhMVEE0SURBek9qRTJPalV4SUVGdFpYSnBZMkV2VEc5elgwRnVaMlZzWlhNaU93b0pJbTl5YVdkcGJtRnNMWEIxY21Ob1lYTmxMV1JoZEdVaUlEMGdJakl3TVRRdE1ERXRNRGdnTVRFNk1UWTZOVEVnUlhSakwwZE5WQ0k3Q24wPSI7CgkiZW52aXJvbm1lbnQiID0gIlNhbmRib3giOwoJInBvZCIgPSAiMTAwIjsKCSJzaWduaW5nLXN0YXR1cyIgPSAiMCI7Cn0=";
		
	}

	public void SendMsgOpenPayment_C2S()
	{
#if UNITY_IOS
		MsgOpenPayment_C2S message = new MsgOpenPayment_C2S(1);
		ClientNet.Singleton.SendPacket(message);
#else
		
		
#endif
	}
	public void SyncPlayerPositon(Vector3 currentPos, Vector3 targetPos)
	{

	}
};