using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace YVR.Global{
    public class ClientSocketController : MonoBehaviour
    {
        internal string ServiceLocation{get;set;}
        internal int Port{get;set;}
        Thread clientThread;
        TcpClient client;
        bool disconnected=false;  
        internal Action<string> msgHandler=null;       
        // Start is called before the first frame update
        void Start()
        {
            try
            {
                if(!Regex.IsMatch(ServiceLocation,@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                throw new Exception();
                clientThread = new Thread(new ThreadStart(recv));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
            catch (Exception)
            {
                msgHandler("Connection failed, exit in 5 seconds.");
            }
        }
        /// <summary>
        /// connect to the server socket, and read data packets
        /// from the network stream.
        /// </summary>
        void recv()
        {
            client = new TcpClient(ServiceLocation, Port);
            byte[] buffer = new byte[65536];
            using (var stream = client.GetStream())
            {
                int len;
                while ((len = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    var buff = new byte[len];
                    Array.Copy(buffer, buff, len);
                    string msgs = Encoding.ASCII.GetString(buff);
                    msgHandler(msgs);
                    if (disconnected) break;
                }
            }
        }
        /// <summary>
        /// Write to the network stream
        /// </summary>
        void send(string msg)
        {
            if (client == null) return;
            try
            {
                var stream = client.GetStream();
                
                if (stream.CanWrite)
                {
                    byte[] dt = Encoding.ASCII.GetBytes(msg + "{break}");
                    stream.Write(dt, 0, dt.Length);
                }
                
            }
            catch (Exception e)
            {
                //_msgReceived = e.Message;
                Debug.Log(e.Message);
            }
        }
        ///  <summary>
        ///  Invoke when scene changes,
        ///  it terminates the client connection as well as the 
        ///  thread for the network stream
        ///  </summary>
        void OnDestroy()
        {
            try
            {
                disconnected = true;
                clientThread.Abort();
                client.Close();
            }
            catch (Exception e )
            {
                Debug.Log(e.Message);
            }
        }
        public void ChangeScene()
        {
            SceneController.ChangeToGameLobby(Game.Card);
        }
    }
}