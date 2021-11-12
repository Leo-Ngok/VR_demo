using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace YVR.Triangle
{
    [RequireComponent(typeof(Global.SettingsFwdController))]
    public class GameController : MonoBehaviour
    {
        public GameObject triangle_prefab;
        public Text txtScore,txtLives;
        public float speedRatio=1f,radius=2f,spawnRate=10f,minSpawnRate=5f,elapsed=0f;
        int lives=3;
        int score=0;
        TriangleController triController;
        Global.SettingsFwdController settingsFwdController;
        List<TriangleController> tricontrollers=new List<TriangleController>();
        // Start is called before the first frame update
        void Start()
        {
            settingsFwdController=this.GetComponent<Global.SettingsFwdController>();
            minSpawnRate=settingsFwdController.TriangleSpawnTime.Value;
            lives=settingsFwdController.TriangleLives.Value;
            Spawn_Triangle();
            txtLives.text=lives.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            for(int i=0;i<tricontrollers.Count;i++)
            {
                if(tricontrollers[i].triangleContainer!=null&&tricontrollers[i]!=null&&tricontrollers[i].status!=null)
                {
                    
                    if(tricontrollers[i].status.Value)score++;
                    else 
                    {
                        score--;lives--;
                        txtLives.text=lives.ToString();
                        if (lives <= 0)
                            Global.SceneController.ChangeToGameLobby(Global.Game.Triangle,$"Score: {score}");
                    }
                    txtScore.text=score.ToString();
                    Destroy(tricontrollers[i]);
                    Destroy(tricontrollers[i].triangleContainer);
                    tricontrollers.RemoveAt(i--);
                }
            }
            speedRatio+=0.0002f;
            elapsed+=Time.deltaTime;
            if(elapsed>=minSpawnRate)
            {
                elapsed=0;
                Spawn_Triangle();
            }
        }
        void Spawn_Triangle()
        {
            var tri=Instantiate(triangle_prefab);
            triController=tri.transform.GetComponent<TriangleController>();
            triController.CreateTriangle(tri,speedRatio,radius);
            tricontrollers.Add(triController);
        }
        
    }
}