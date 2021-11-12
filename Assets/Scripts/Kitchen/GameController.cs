using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Global;
using TMPro;
using System.Linq;
namespace YVR.Kitchen
{
    public class GameController : MonoBehaviour
    {
        public TEXDraw txteqn,txtans;
        private Question question; private QuestionController questionController;
        
        public TextMeshProUGUI txtTime;
        public GameObject progressBar,crate,pwdSheet,keys1,keys2,keys3,pwdpanel,timeleftpanel,keyboard;
        SettingsFwdController settingsFwdController;

        float currentTime=0;int time;
        private float remainingTime{get=>(float)time-currentTime;}

        [System.NonSerialized]
        public bool isOpened=false,haskey=false;
        private GameObject key,kt;
        public TMP_InputField txtpwd;
        bool canReset;string resetpwd;

        // Start is called before the first frame update
        void Start()
        {
            questionController=new QuestionController();
            question=questionController
                .GetRandomEquation(YVR.Global.State.Difficulty);
            List<string> listans=new List<string>();
            listans.Add($"{question.CorrectAnswer}(密碼 \\ Password: {question.AnsCode}) \\\\");
            for(int i=0;i<question.Codes.Count;i++)
            listans.Add($"{question.Answers[i]}(密碼 \\ Password: {question.Codes[i]}) \\\\");
            listans.Shuffle(5);
            txteqn.text=question.Equation;
            txtans.text=string.Join(System.Environment.NewLine,listans);

            settingsFwdController=this.GetComponent<SettingsFwdController>();
            time=settingsFwdController.kitchenTimeLimit.Value;

            crate.GetComponent<Animator>().Play("Closed");
            pwdSheet.SetActive(false);

            List<GameObject> allkeys
            =Resources.FindObjectsOfTypeAll<GameObject>().Where(obj=>obj.name=="Key").ToList();
            IEnumerable<GameObject> keys;
            switch(Global.State.Difficulty)
            {
                case 1:kt=keys1;break;
                case 2:kt=keys2;break;
                case 3:default:kt=keys3;break;
            }
            keys=kt.transform.GetComponentsInChildren<Transform>()
                    .Where(X=>X.name=="Key")
                    .Select(X=>X.gameObject);
            key=keys.ToList()[Random.Range(0,keys.Count())];
            foreach(GameObject k in allkeys) k.SetActive(false);
            key.SetActive(true);

            canReset=settingsFwdController.KitchenReset.Value;
            resetpwd=settingsFwdController.KitchenResetPwd;
            PanelHide();
        }
        void Update()
        {
            if(remainingTime>0)
            {
                currentTime+=Time.deltaTime;
                string str=(currentTime%2==0)?@"mm\:ss":@"mm\ ss";
                txtTime.SetText(System.TimeSpan
                .FromSeconds((double)remainingTime)
                .ToString(str));
                progressBar.transform.GetComponent<RectTransform>()
                .SetRight(currentTime*280/(float)time);
            }
            else SceneController.ChangeToGameLobby(Game.Kitchen,"You ran out of time");
        }
        public void UnlockCrate()
        {
            if(haskey&&!isOpened)
            {
                crate.GetComponent<Animator>().Play("Open");
                pwdSheet.SetActive(true);
                isOpened=true;
            }          
        }
        public void HideKey()
        {
            haskey=true;key.SetActive(false);
        }
        public void PanelShow()
        {
            pwdpanel.SetActive(true);
            timeleftpanel.SetActive(false);
            keyboard.SetActive(true);
        }
        public void PanelHide()
        {
            pwdpanel.SetActive(false);
            timeleftpanel.SetActive(true);
            keyboard.SetActive(false);
            txtpwd.text=null;
        }
        public void AggregrateNum(int t) => txtpwd.text+=t.ToString();
        public void BackSpace()
        {
            if(txtpwd.text.Length>0)
                txtpwd.text=txtpwd.text.Remove(txtpwd.text.Length-1);
        }
        public void ChkPwd()
        {
            if(canReset&&txtpwd.text==resetpwd)
            {
                Global.SceneController.ChangeToGameLobby(Global.Game.Kitchen);
                return;
            }
            try{int.Parse(txtpwd.text);}
            catch(System.Exception){return;}
            if(txtpwd.text.Trim()==question.AnsCode)
            Global.SceneController.ChangeToGameLobby(Global.Game.Kitchen,"You are correct");

        }
    }
}