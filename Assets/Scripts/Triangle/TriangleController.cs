using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
namespace YVR.Triangle
{
    public class Question
    {
        public string Expression{get;set;}
        public string Answer{get;set;}
        public string LineA{get;set;}
        public string LineB{get;set;}
        public string LineC{get;set;}
        public string AngleA{get;set;}
        public string AngleB{get;set;}
        public string AngleC{get;set;}
        [JsonIgnore]
        public string this[int i]
        {
            get
            {
                switch(i)
                {
                    case 0:return LineA;
                    case 1:return LineB;
                    case 2:return LineC;
                    case 3:return AngleA;
                    case 4:return AngleB;
                    case 5:return AngleC;
                    case 6:return Expression;
                    case 7:return Answer;
                    default:throw new System.Exception();
                }
            }
        }
    }
    public class TriangleController : MonoBehaviour
    {
        internal GameObject triangleContainer;
        float speedRatio=1f;
        internal bool? status=null;
        List<GameObject> listLabels=new List<GameObject>();
        Question question;
        internal void CreateTriangle(GameObject triangle,
        float _speedratio=1f, float R=3f)
        {
            speedRatio=_speedratio;
            triangleContainer=triangle;

            float angle=Random.Range(0f,2*Mathf.PI);
            triangleContainer.transform.position
            =new Vector3(R*Mathf.Cos(angle),1.5f,R*Mathf.Sin(angle));
            var rot=Quaternion.LookRotation(-triangleContainer.transform.position);
            rot=Quaternion.Euler(0,rot.eulerAngles.y-180,rot.eulerAngles.z);
            triangleContainer.transform.rotation=rot;
            listLabels=triangleContainer.transform
            .GetComponentsInChildren<Transform>()
            .Where(x=>x.name!="Triangle_Front"&&x.name!="Triangle_Template(Clone)")
            .Select(x=>x.gameObject)
            .ToList();

            string json=Resources.Load<TextAsset>("Triangle/tri_questions_set").text;
            var questions=JsonConvert.DeserializeObject<List<Question>>(json);
            question=questions[Random.Range(0,questions.Count-1)];
            
            SetTriangle(question);
        }
        void SetTriangle(Question question)
        {
            for(int i=0;i<listLabels.Count;i++)
            listLabels[i].GetComponent<TextMeshPro>().text=question[i];

        }
        // Update is called once per frame
        void Update()
        {
            var pos=triangleContainer.transform.position;
            triangleContainer.transform.position
            =new Vector3(pos.x,pos.y-0.0005f*speedRatio,pos.z);
            if(pos.y<0)status=false;
        }
        public void OnHover(int idx)
        {
            if(listLabels[idx]!=null)
            listLabels[idx].GetComponent<TextMeshPro>().color=Color.blue;
        }
        public void OnHoverExit(int idx)
        {
            if(listLabels[idx]!=null)
            listLabels[idx].GetComponent<TextMeshPro>().color=Color.white;
        }
        public void OnSelected(int idx)
        {
            if(listLabels[idx]!=null)
            status=(listLabels[idx].GetComponent<TextMeshPro>().text==question.Answer);
        }
    }
}