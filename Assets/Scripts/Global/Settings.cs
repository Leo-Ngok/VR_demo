using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
namespace YVR.Global
{
    public class Settings : MonoBehaviour
    {
        // Start is called before the first frame update
        public TextAsset defaultconfig;
        public List<Toggle> toggles;
        public List<InputField> textBox;
        public List<GameObject> panels;
        InputField _txtc;
        void Start()
        {
            string json=File.ReadAllText(Application.persistentDataPath+"/Configs.json");
            Configs configs=JsonConvert.DeserializeObject<Configs>(json);
            toggles[0].isOn=configs.KitchenState;
            toggles[1].isOn=configs.KitchenL1;
            toggles[2].isOn=configs.KitchenL2;
            toggles[3].isOn=configs.KitchenL3;
            toggles[4].isOn=configs.KitchenCanReset;
            toggles[5].isOn=configs.TriangleState;
            toggles[6].isOn=configs.FootballState;
            toggles[7].isOn=configs.CardsState;
            toggles[8].isOn=configs.CardSingle;
            toggles[9].isOn=configs.CardMulti;
            string[] IPA=configs.IPv4_location.Split('.');
            for (int i=0;i<4;i++)
            textBox[i].text=IPA[i];
            textBox[4].text=configs.Port.ToString();
            textBox[5].text=configs.KitchenTimeLimit.ToString();
            textBox[6].text=configs.KitchenResetPwd;
            textBox[7].text=configs.NonspawningX.ToString();
            textBox[8].text=configs.NonspawningY.ToString();
            textBox[9].text=configs.SpawningX.ToString();
            textBox[10].text=configs.SpawningY.ToString();
            textBox[11].text=configs.FootballSTimeout.ToString();
            textBox[12].text=configs.FootballFTimeout.ToString();
            textBox[13].text=configs.Triangle_SpawnTime.ToString();
            textBox[14].text=configs.Triangle_Lives.ToString();
            textBox[15].text=configs.CardTimePerQuestion.ToString();
            textBox[16].text=configs.CardNumbers.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if(textBox.Any(x=>x.isFocused))
                _txtc=textBox.Where(x=>x.isFocused).ToList()[0];    
        }
        public void addnum(int i)
        {
            if(_txtc!=null)
                _txtc.text+=i.ToString();
        }
        public void bkspace()
        {
            if(_txtc!=null&&_txtc.text.Length>0)
                _txtc.text=_txtc.text.Remove(_txtc.text.Length-1);
        }
        public void scenebtnclick(int i)
        {
            foreach (var item in panels)
            {
                item.SetActive(false);
            }
            panels[i].SetActive(true);
            _txtc=null;
        }
        public void Quit()
        {
            Configs cfg=new Configs();
            try{
                cfg.KitchenState=toggles[0].isOn;
                cfg.KitchenL1=toggles[1].isOn;
                cfg.KitchenL2=toggles[2].isOn;
                cfg.KitchenL3=toggles[3].isOn;
                cfg.KitchenCanReset=toggles[4].isOn;
                cfg.TriangleState=toggles[5].isOn;
                cfg.FootballState=toggles[6].isOn;
                cfg.CardsState=toggles[7].isOn;
                cfg.CardSingle=toggles[8].isOn;
                cfg.CardMulti=toggles[9].isOn;
                cfg.IPv4_location=$"{int.Parse(textBox[0].text)}.{int.Parse(textBox[1].text)}.{int.Parse(textBox[2].text)}.{int.Parse(textBox[3].text)}";
                cfg.Port=int.Parse(textBox[4].text);
                cfg.KitchenTimeLimit=int.Parse(textBox[5].text);
                cfg.KitchenResetPwd=textBox[6].text;
                cfg.NonspawningX=int.Parse(textBox[7].text);
                cfg.NonspawningY=int.Parse(textBox[8].text);
                cfg.SpawningX=int.Parse(textBox[9].text);
                cfg.SpawningY=int.Parse(textBox[10].text);
                cfg.FootballSTimeout=int.Parse(textBox[11].text);
                cfg.FootballFTimeout=int.Parse(textBox[12].text);
                cfg.Triangle_SpawnTime=int.Parse(textBox[13].text);
                cfg.Triangle_Lives=int.Parse(textBox[14].text);
                cfg.CardTimePerQuestion=int.Parse(textBox[15].text);
                cfg.CardNumbers=int.Parse(textBox[16].text); 
                string str=JsonConvert.SerializeObject(cfg);
                File.WriteAllText(Application.persistentDataPath+"/Configs.json",str);  
            }
            catch(System.Exception)
            {
                return;
            }
            SceneController.ChangeScene("LobbyScene");
        }
    }
    public class Configs
    {
        public bool KitchenState{get;set;}
        public bool KitchenL1{get;set;}
        public bool KitchenL2{get;set;}
        public bool KitchenL3{get;set;}
        public bool KitchenCanReset{get;set;}
        public bool TriangleState{get;set;}
        public bool FootballState{get;set;}
        public bool CardsState{get;set;}
        public bool CardSingle{get;set;}
        public bool CardMulti{get;set;}
        public string IPv4_location{get;set;}
        public int Port {get;set;}
        public int KitchenTimeLimit{get;set;}
        public string KitchenResetPwd{get;set;}
        public int NonspawningX{get;set;}
        public int NonspawningY{get;set;}
        public int SpawningX{get;set;}
        public int SpawningY{get;set;}
        public int FootballSTimeout{get;set;}
        public int FootballFTimeout{get;set;}
        public int Triangle_SpawnTime{get;set;}
        public int Triangle_Lives{get;set;}
        public int CardTimePerQuestion{get;set;}
        public int CardNumbers{get;set;}
    }
}