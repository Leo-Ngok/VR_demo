using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YVR.Global;
using System.Linq;
namespace YVR.Cards
{
    public class Equation
    {
        public int ID {get;set;}
        public string Expression{get;set;}
        public string Value{get;set;}
    }
    public class EquationController
    {
        List<Equation> listeqns=new List<Equation>();
        public EquationController(string eqns)
        {
            listeqns=JsonConvert.DeserializeObject<List<Equation>>(eqns);
        }
        public Equation GetRandomEqn(int ignoreid=-1)
        {
            listeqns.Shuffle(5);
            return (ignoreid==-1)?listeqns[0]:
            listeqns.Where(x=>x.ID!=ignoreid).FirstOrDefault();
        }
        //yet to finish
        public List<Equation> GetRandomEquations(int cnt,int igonreid)
        {
            listeqns.Shuffle(5);
            List<Equation> res=new List<Equation>();
            for(int i=0;i<cnt;i++)res.Add(listeqns[i]);
            return res;
        }
        public Equation GetRandomEquationByValue(int ignoreid,string value)
        {
            listeqns.Shuffle(5);
            return listeqns.Where(x=>x.ID!=ignoreid&&x.Value==value)
            .FirstOrDefault();
        }
    }
}