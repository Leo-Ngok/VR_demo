using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace YVR.Football
{
    [RequireComponent(typeof(Global.SettingsFwdController))]
    public class GameController : MonoBehaviour
    {
        public GameObject rig;
        int[] min=new int[2],max=new int[2],
        no_spawn_min=new int[2],no_spawn_max=new int[2];
        public GameObject pillar,football,panel;
        public Text txtFootballLocation, txtPlayerLocation, txtMessage;
        Global.SettingsFwdController settingsFwdController;
        List<Stone> stones=new List<Stone>();
        private int[] goal=new int[2];
        // Start is called before the first frame update
        private bool X_reached=false,hasCalledForEnd=false;
        void Start()
        {
            settingsFwdController=this.GetComponent<Global.SettingsFwdController>();
            min[0]=-settingsFwdController.spawningX.Value;
            max[0]=-min[0];
            max[1]=settingsFwdController.spawningY.Value;
            min[1]=-max[1];
            no_spawn_max[0]=settingsFwdController.nonspawningX.Value;
            no_spawn_min[0]=-no_spawn_max[0];
            no_spawn_max[1]=settingsFwdController.nonspawningY.Value;
            no_spawn_min[1]=-no_spawn_max[1];
            do
            {
                goal[0]=Random.Range(min[0],max[0]+1);
                goal[1]=Random.Range(min[1],max[1]+1);
            }
            while((goal[0]>no_spawn_min[0]&&goal[0]<no_spawn_max[0])||
                    (goal[1]>no_spawn_min[1]&&goal[1]<no_spawn_max[1]));
            football.transform.position=new Vector3(goal[0],0.2f,goal[1]);
            txtFootballLocation.text=$"({goal[0]},{goal[1]})";
            var newpillar=Instantiate(pillar);
            newpillar.transform.position=new Vector3(goal[0],-2.5f,goal[1]);
            stones.Add(new Stone(goal[0],goal[1],newpillar));
        }

        // Update is called once per frame
        void Update()
        {
            txtPlayerLocation.text=$"({(int)Camera.main.transform.position.x},{(int)Camera.main.transform.position.z})";
            
            float dt=0;
            dt+=Time.deltaTime;
            Vector3 camPos=Camera.main.transform.position;
            int camX=Mathf.RoundToInt(camPos.x),
            camY=Mathf.RoundToInt(camPos.y),
            camZ=Mathf.RoundToInt(camPos.z);
            
            if((rig.GetComponent<Rigidbody>()==null)&&!(camX==0&&camZ==0)&&((camX*(camX-goal[0])>0||camZ*(camZ-goal[1])>0)||
            (!X_reached&&camZ!=0)||(X_reached&&camX!=goal[0]&&camZ!=0)))
                rig.AddComponent<Rigidbody>();
            
            if(!(camX==0&&camZ==0)&&camY>-1&&!stones.Any(w=>w.X==camX&&w.Z==camZ))
            {
                if(camZ==0&&!X_reached&&(camX*(camX-goal[0])<=0))
                {
                    var newpillar=Instantiate(pillar);
                    stones.Add(new Stone(camX,camZ,pillar));
                    newpillar.transform.position=new Vector3(camX,-2.5f,0);
                }
                else if(camX==goal[0]&&camZ!=0&&(camZ*(camZ-goal[1])<=0))
                {
                    var newpillar=Instantiate(pillar);
                    stones.Add(new Stone(camX,camZ,pillar));
                    newpillar.transform.position=new Vector3(camX,-2.5f,camZ);
                }
            }
            if(camX==goal[0]&&camY>-1&&camZ==0&&!X_reached)
            {
                X_reached=true;
                txtMessage.text=
                $"You have done {Mathf.Abs(goal[0])} units, let's give a try on the Y-axis.";

            }
            if(camX==goal[0]&&camPos.y>-1&&camZ==goal[1])
            {
                txtMessage.text="Great! You have picked up the football!";
                
                if(!hasCalledForEnd)
                    StartCoroutine(EndScene(settingsFwdController.FootballSTimeout.Value,"Great! You picked up the football",Color.green));
            }
            if(camY<-1||rig.transform.position.y<-1)
            {
                txtMessage.text=$"You dropped off the cliff, ending in {settingsFwdController.FootballFTimeout.Value} secs.";
                if(!hasCalledForEnd)
                    StartCoroutine(EndScene(settingsFwdController.FootballFTimeout.Value,"You dropped off the cliff",Color.red));
            }
        }
        IEnumerator EndScene(int sec, string msg,Color color)
        {
            hasCalledForEnd=true;
            panel.GetComponent<Image>().color=color;
            yield return new WaitForSeconds(sec);
            Global.SceneController.ChangeToGameLobby(Global.Game.Football,msg);
        }
    }
    public class Stone
    {
        public int X{get;set;}
        public int Z{get;set;}
        public GameObject Pillar{get;set;}
        public Stone(int x,int z, GameObject pillar)
        {
            this.X=x;this.Z=z;this.Pillar=pillar;
        }
    }
}