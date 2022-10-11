using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
public class waterGroundGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] verticesBase;
    float[] humidity;
    int[] triangles;
    public int col, row;
    public float distance;
    private Color[] colors;
    public Color blue;
    float time =0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        

        GetComponent<MeshFilter>().mesh=mesh;
        createShape(col,row, distance);
        updateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i =0; i< col; i++)
        for (int j =0; j< col; j++){
        vertices[i*row+j] = verticesBase[i*row+j]+ Vector3.up*Mathf.Cos((float)(Mathf.Sqrt(i*i+j*j)/10+time))*0.1f;
        humidity[i*row+j]= (Mathf.Cos((float)(Mathf.Sqrt(i*i+j*j)/10+time))/2+0.5f);
        colors[i*row+j].a = (177f/255f)*(humidity[i*row+j]*0.5f+0.5f); 
        }
        time += 0.01f;
        updateMesh();
    }


    void createShape(int row, int col, float distance)
    {
        int i, j;

        vertices = new Vector3[col*row];
        verticesBase = new Vector3[col*row];
        colors = new Color[col*row];
        humidity = new float[col*row];

        triangles = new int[((col-1)*(row)+(col-2))*6+6];
        

        for (i=0; i<col;i++)
        {
            for (j=0; j<row;j++)
            {
                vertices[i*row+j]= transform.position + new Vector3(distance*(j+(((float)(i%2))/2))-(distance*row)/2,0.0f,distance*i-(distance*col)/2);
                verticesBase[i*row+j]=vertices[i*row+j];
                colors[i*row+j] = blue;
                humidity[i*row+j] = (char) 0;
            }
        }

        for (i=0; i<col-1;i++)
        {
            for (j=0; j<row-1;j++)
            {
                    triangles[(i*row+j)*6]  = i*row+j;
                    triangles[(i*row+j)*6+1]= ((i+1))*row+j;
                    triangles[(i*row+j)*6+2]= i*row+j+1;
                    
                    triangles[(i*row+j)*6+3] = i*row+j+1;
                    triangles[(i*row+j)*6+4]= ((i+1)*row)+j;
                    triangles[(i*row+j)*6+5]= ((i+1)*row)+j+1;
            }
        }
    }

    void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}
