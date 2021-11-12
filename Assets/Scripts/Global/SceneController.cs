using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Global
{
    public enum Game
       {
           None,Kitchen,Triangle,Football,Card
       }
    public class SceneController:MonoBehaviour
    {
        public static void ChangeScene(string name)=>
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        public void ChangeToLobby()
        {
            State.Score=null;
            ChangeScene("LobbyScene");
        }
        public void ChangeFromLobby(int i)
        {
            ChangeToGameLobby((Game)i);
        }
        public static void ChangeToGameLobby(Game game,string score ="")
        {
            State.Score=score;
            
            switch (game)
            {
                case Game.Kitchen:
                    ChangeScene("KitchenLobbyScene");
                    break;
                case Game.Triangle:
                    ChangeScene("TriangleLobbyScene");
                    break;
                case Game.Football:
                    ChangeScene("FootballLobbyScene");
                    break;
                case Game.Card:
                    ChangeScene("CardLobbyScene");
                    break;
                default:
                    break;
            }
        }
        public void ChangeToGame(int game)
        {
            if(game==6) Global.State.Difficulty=1;
            if(game==7)Global.State.Difficulty=2;
            if(game==8)Global.State.Difficulty=3;
            if(game>=6)game=1;
            switch ((Game)game)
            {
                case Game.Kitchen:
                    ChangeScene("KitchenScene");
                    break;
                case Game.Triangle:
                    ChangeScene("TriangleScene");
                    break;
                case Game.Football:
                    ChangeScene("FootballScene");
                    break;
                case Game.Card:
                    ChangeScene("CardScene");
                    break;
                case (Game)5:
                    ChangeScene("CardMultiScene");
                    break;
                default:
                    break;
            }
        }
        public void ChangeToSettings()=>ChangeScene("SettingsScene");
    }

}

