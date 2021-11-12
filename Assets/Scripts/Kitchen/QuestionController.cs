using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YVR.Global;
namespace YVR.Kitchen
{
    public class QuestionController
    {
        List<Question> listq=new List<Question>();
        
        public QuestionController()
        {
            TextAsset tempAsset=Resources.Load<TextAsset>("Kitchen/kitchen_questions");
            string json=tempAsset.text;
            listq=JsonConvert.DeserializeObject<List<Question>>(json);
            foreach (var question in listq)
            {
                int len=question.Answers.Count;
                for(int i=0;i<len;i++)
                    question.Codes.Add(RandomString(4));
                question.AnsCode=RandomString(4);
            }
        }
        
        public Question GetRandomEquation(int level)
        {
            listq.Shuffle(10);
            return listq.Where(x=>x.Level==level)
                    .FirstOrDefault();
        }
        public static string RandomString(int len)
        {
            const string chars="0123456789";
            return new string(Enumerable.Repeat(chars,len)
            .Select(s=>s[UnityEngine.Random.Range(0,s.Length)])
            .ToArray());
        }
    }
    public class Question
    {
        public string Equation{get;set;} 
        public string CorrectAnswer{get;set;}
        public int Level{get;set;}
        public List<string> Answers{get;set;}
        [JsonIgnore]
        public List<string> Codes=new List<string>();
        [JsonIgnore]
        public string AnsCode{get;set;}
    }

}