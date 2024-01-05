using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public float xoffset = 0;
    public float Tilelenght = 30;
    public GameObject[] gameObj;// Start is called before the first frame update
    public int numberoftiles=3;
    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playertransform;
    void Start()
    {
        SpawnTile(0);
       for (int i=0;i<numberoftiles;i++)
       {
           if(i==0)
                SpawnTile(1);
           else
                SpawnTile(Random.Range(0,gameObj.Length));
       }
    }

    // Update is called once per frame
    void Update()
    {   
        if(playertransform.position.z+10>xoffset-(numberoftiles*Tilelenght))
        {
             SpawnTile(Random.Range(0,gameObj.Length));
             DeleteTile();
        }
        
    }

    public void SpawnTile(int tileIndex)
    {
        
        GameObject go = Instantiate(gameObj[tileIndex], transform.forward*xoffset, transform.rotation);
        activeTiles.Add(go);
        xoffset +=Tilelenght;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
