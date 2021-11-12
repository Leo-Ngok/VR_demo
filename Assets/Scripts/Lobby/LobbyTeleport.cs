using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
namespace YVR.Lobby
{

    public class LobbyTeleport : MonoBehaviour
    {
        public Transform player;
        public TextAsset configs;
        void Start()
        {
            if(!File.Exists(Application.persistentDataPath+"/Configs.json"))
                File.WriteAllText(Application.persistentDataPath+"/Configs.json",configs.text);
            if(!YVR.Global.State.FirstRun)
            {
                if(YVR.Global.State.pos!=null)
                    player.position=YVR.Global.State.pos.Value;
                
                if(YVR.Global.State.rot!=null)
                    player.rotation=YVR.Global.State.rot;
            }
            YVR.Global.State.FirstRun=false;
        
        }
        void Update()
        {
            if(player.position.y<-5)
            {
                player.position=new Vector3(-3f,1f,-21f);
            }
        }
        public void recordpos()
        {
           YVR.Global.State.pos=player.position;
           YVR.Global.State.rot=player.rotation;
        }
        public void ToRoof()=>player.position=new Vector3(-28,36,-20);
        public void ToGround()=>player.position=new Vector3(-24,1,-20);
        public void Quit()=>Application.Quit(0);
    }
}

