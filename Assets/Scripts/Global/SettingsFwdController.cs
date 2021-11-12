using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
namespace YVR.Global
{
    
    public class SettingsFwdController : MonoBehaviour
    {
        public Game game;
        public bool isLobby;
        public GameObject KitchenBtn,TriangleBtn,
        FootballBtn,CardBtn,KitchenL1Btn,KitchenL2Btn,KitchenL3Btn,
        CardSBtn,CardMBtn;
        internal bool? KitchenReset;
        internal string ipv4_location,KitchenResetPwd;
        internal int? port,kitchenTimeLimit,nonspawningX,nonspawningY,
        spawningX,spawningY,FootballSTimeout,FootballFTimeout,
        TriangleSpawnTime,TriangleLives,CardTimeout,CardQCount;
        // Start is called before the first frame update
        void Start()
        {
            string json=File.ReadAllText(Application.persistentDataPath+"/Configs.json");
            Configs configs=JsonConvert.DeserializeObject<Configs>(json);
            switch(game)
            {
                case Game.None:
                    KitchenBtn.SetActive(configs.KitchenState);
                    TriangleBtn.SetActive(configs.TriangleState);
                    FootballBtn.SetActive(configs.FootballState);
                    CardBtn.SetActive(configs.CardsState);
                    break;
                case Game.Kitchen:
                    if(isLobby)
                    {
                        KitchenL1Btn.SetActive(configs.KitchenL1);
                        KitchenL2Btn.SetActive(configs.KitchenL2);
                        KitchenL3Btn.SetActive(configs.KitchenL3);
                    }
                    break;
                case Game.Card:
                    if(isLobby)
                    {
                        CardSBtn.SetActive(configs.CardSingle);
                        CardMBtn.SetActive(configs.CardMulti);
                    }
                    break;
                default:break;
            }
            KitchenReset=configs.KitchenCanReset;
            ipv4_location=configs.IPv4_location;
            port=configs.Port;
            kitchenTimeLimit=configs.KitchenTimeLimit;
            KitchenResetPwd=configs.KitchenResetPwd;
            nonspawningX=configs.NonspawningX;
            nonspawningY=configs.NonspawningY;
            spawningX=configs.SpawningX;
            spawningY=configs.SpawningY;
            FootballFTimeout=configs.FootballFTimeout;
            FootballSTimeout=configs.FootballSTimeout;
            TriangleLives=configs.Triangle_Lives;
            TriangleSpawnTime=configs.Triangle_SpawnTime;
            CardTimeout=configs.CardTimePerQuestion;
            CardQCount=configs.CardNumbers;
        }
    }
}