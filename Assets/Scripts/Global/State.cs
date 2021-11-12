using UnityEngine;
namespace YVR.Global
{
    public class State
    {
        public static bool FirstRun{get;set;}=true;
        public static string Score {get;set;} = null;
        public static int Difficulty {get;set;}=3;
        public static Vector3? pos {get;set;}
        public static Quaternion rot{get;set;}
    }
}