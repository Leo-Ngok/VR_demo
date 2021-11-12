using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Global;
namespace YVR.Cards
{
    [RequireComponent(typeof(ClientSocketController))]
    public class GameController_m : GameController
    {
        //public TEXDraw txtQuestion;
        ClientSocketController socketController;
        string s="";
        // Start is called before the first frame update
        void Start()
        {
            socketController=this.GetComponent<ClientSocketController>();
            socketController.msgHandler=HandleMessage;
            settingsFwdController=this.GetComponent<SettingsFwdController>();
            socketController.ServiceLocation=settingsFwdController.ipv4_location;
            socketController.Port=settingsFwdController.port.Value;
        }

        // Update is called once per frame
        void Update()
        {
            
            txtQuestion.text=s;
        }
        ///<summary>
        ///Convert the string message to game data by deserializing json information
        ///</summary>
        void HandleMessage(string msg)
        {
            if(msg=="Connection failed, exit in 5 seconds.")
            SceneController.ChangeToGameLobby(Game.Card,msg.Substring(0,17));
            s=msg;
        }
    }
}