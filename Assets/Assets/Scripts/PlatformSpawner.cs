using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private float xoffset = 30f;
    private float tileLength = 30.0f;
    public GameObject[] gameObj;
    private Transform playerTransform;
    private int amttileonscreen=7;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpwanTile();

        SpwanTile();

        SpwanTile();

        SpwanTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpwanTile(int prefabIndex = -1)
    {
        GameObject go;
        go=Instantiate(gameObj[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(1,0,0)*xoffset;
        xoffset+=tileLength;
    }
}
