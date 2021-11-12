using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace YVR.Global
{
    public class GameLobbyController : MonoBehaviour
    {
        public GameObject panelscore;
        public TextMeshProUGUI txtscore;
        // Start is called before the first frame update
        void Start()
        {
            if(State.Score==""||State.Score==null)
                panelscore.SetActive(false);
            else txtscore.text=State.Score;
        }
    }
}