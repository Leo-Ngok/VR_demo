using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YVR.Global;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

namespace YVR.Cards
{
    public class PlayerController : MonoBehaviour
    {
        TextMeshProUGUI score_text;
        List<GameObject> listCards=new List<GameObject>();
        List <Equation> equations;
        List <TEXDraw> listChoices=new List<TEXDraw>();
        int? idx_selected;
        float selectedTime=0;
        internal int score=0;
        public GameController gameController;
        public bool isMulti=false;
        [Tooltip("Singleplayer mode only")]
        private Transform board;
        [Tooltip("Multiplayer mode only")]
        public XRRig xRRig;
        private GameObject placeholderPlayer,placeholderBody,placeholderLH,placeholderRH;
        // Start is called before the first frame update
        void Start()
        {

            if(isMulti)
            {
                placeholderPlayer=this.gameObject.transform.Find("Character").gameObject;
                placeholderBody=placeholderPlayer.transform.Find("Body").gameObject;
                placeholderLH=placeholderPlayer.transform.Find("Left_Hand").gameObject;
                placeholderRH=placeholderPlayer.transform.Find("Right_Hand").gameObject;
                placeholderPlayer.SetActive(false);                        
            }
            board=this.gameObject.transform.Find("Board");
            listCards=board
            .GetComponentsInChildren<Transform>()
            .Where(x=>Regex.IsMatch(x.name,@"Paper_\d{1,2}"))
            .Select(x=>x.gameObject).ToList();
            score_text=board.Find("Canvas_CurrentScore").Find("Text_Score").GetComponent<TextMeshProUGUI>();
            
            for(int i=0;i<listCards.Count;i++)
            {
                listChoices.Add(listCards[i]
                .transform.GetComponentInChildren<Transform>()
                .transform.GetComponentInChildren<Transform>()
                .GetComponentInChildren<TEXDraw>());
                RestoreCardColor(i);
            }
                
        }
        internal bool Refresh(int id,string val,string eqns)
        {
            var equationController=new EquationController(eqns);
            var tempList=new List<Equation>();
            tempList.AddRange(equationController
            .GetRandomEquations(listCards.Count,id));
            if(!tempList.Any(K=>K.Value==val))
            {
                var eq=equationController.GetRandomEquationByValue(id,val);
                if(eq==null)return false;
                tempList[0]=eq;
            }
            for(int i=0;i<listCards.Count;i++)
                listCards[i].GetComponent<MeshRenderer>().material.color=new Color(214f / 255f, 214f / 255f, 214f / 255f);
            idx_selected=null;
            selectedTime=0;
            
            tempList.Shuffle(5);
            equations=tempList;
            for(int i=0;i<listCards.Count;i++)
            listChoices[i].text=equations[i].Expression;
            return true;
        }
        public void OnCardSelected(int idx)
        {
            idx_selected=idx;
            selectedTime=gameController.remainingTime;
            listCards[idx].GetComponent<MeshRenderer>().material.color=new Color(230f / 255f, 112f / 255f, 112f / 255f);
            for(int i=0;i<listCards.Count;i++)
                RestoreCardColor(i);
        }
        public void OnCardHover(int idx)
        {
            listCards[idx].GetComponent<MeshRenderer>().material.color=new Color(112f / 255f, 230f / 255f, 230f / 255f);
        }
        public void RestoreCardColor(int idx)
        {
            if(idx!=idx_selected)
            listCards[idx].GetComponent<MeshRenderer>().material.color=new Color(214f / 255f, 214f / 255f, 214f / 255f);
            else
            listCards[idx].GetComponent<MeshRenderer>().material.color=new Color(230f / 255f, 112f / 255f, 112f / 255f);
        }
        public void Check(string val)
        {
            if(idx_selected!=null&&equations[idx_selected.Value].Value==val)
                score+=Mathf.RoundToInt(selectedTime*10f);
            if(!isMulti)
                score_text.text=$"You: {score}";
        }
    }
}