using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YVR.Global
{
    public static class ObjExtensions
    {
        public static void Shuffle<T>(this List<T> ts,int iter)
        {
            for(int i=1;i<=iter;i++)
            {
                int cnt=ts.Count;
                for(int j=0;j<cnt-1;j++)
                {
                    int r=Random.Range(j,cnt);
                    T tmp=ts[j];ts[j]=ts[r];ts[r]=tmp;
                }
            }
        }
        public static void SetRight(this RectTransform rectTransform,float right)
        {
            rectTransform.offsetMax=new Vector2(-right,rectTransform.offsetMax.y);
        }
    }
}