using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YVR.Triangle
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [ExecuteAlways]
    public class TriangleShapeController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Mesh mesh=new Mesh();
            Vector3[] vertices=new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(-4,0,0),
                new Vector3(0,4,0)
            };
            Vector2[] uvs=new Vector2[]
            {
                new Vector2(0,0),new Vector2(1,0),new Vector2(0,1)
            };
            int[] triangles={0,1,2};
            mesh.name="Triangle_mesh";
            mesh.vertices=vertices;
            mesh.uv=uvs;
            mesh.triangles=triangles;
            mesh.RecalculateNormals();
            this.GetComponent<MeshFilter>().mesh=mesh;
            this.GetComponent<MeshCollider>().sharedMesh=mesh;
        }

        // Update is called once per frame
        void Update()
        {
            Start();
        }
    }
}