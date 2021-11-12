using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YVR.Global;
namespace YVR.Cards
{
    [RequireComponent(typeof(Global.SettingsFwdController))]
    public class GameController : MonoBehaviour
    {
        int count=10;
        float timePerQuestion=5f;
        public TEXDraw txtQuestion;
        public GameObject progressBar;
        public List<GameObject> listPlayers;
        public Object asset{get;set;}
        public TextAsset TxtEquations;
        private Equation equation;
        float currentTime;
        public float remainingTime{get=>timePerQuestion-currentTime;}
        PlayerController playerController;
        internal Global.SettingsFwdController settingsFwdController;
        // Start is called before the first frame update
        void Start()
        {
            settingsFwdController=this.GetComponent<Global.SettingsFwdController>();
            timePerQuestion=settingsFwdController.CardTimeout.Value;
            count=settingsFwdController.CardQCount.Value;
            playerController=listPlayers[0].GetComponent<PlayerController>();
            SetQues();
        }

        // Update is called once per frame
        void Update()
        {
            currentTime+=Time.deltaTime;
            if(currentTime<=timePerQuestion)
            progressBar.transform.GetComponent<RectTransform>()
            .SetRight(currentTime*5f/(float)timePerQuestion);
            if(remainingTime<=0)
            {            
                playerController.Check(equation.Value);
                currentTime=0;count--;
                if(count<=0)
                SceneController.ChangeToGameLobby(Game.Card,$"Score: {playerController.score}");
                SetQues();
            }
        }
        void SetQues()
        {
            EquationController equationController=new EquationController(TxtEquations.text);
            Equation tempEq;
            do tempEq=equationController.GetRandomEqn();
            while(!playerController.Refresh(tempEq.ID,tempEq.Value,TxtEquations.text));
            equation=tempEq;
            txtQuestion.text=equation.Expression;
        }
    }
}