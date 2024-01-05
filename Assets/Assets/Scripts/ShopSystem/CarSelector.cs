using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    //public ShopMangaer shopManager;
    public int CurrentCubeIndex;
    public GameObject[] cubes;
    public GameObject redCube;
    public GameObject purpleCube;
    public GameObject greenCube;
    public string collectables = "Collectables";
    public Material RedMat;
    public Material PurpleMat;
    public Material GreenMat;
    public PlayerScript playerScript;
    public GameObject greenFlame;
    public GameObject purpleFlame;

    // Start is called before the first frame update
    void Start()
    {
        //RedMat = GetComponent<Renderer>().material;
        //PurpleMat = GetComponent<Renderer>().material;
        //GreenMat = GetComponent<Renderer>().material;
        CurrentCubeIndex = PlayerPrefs.GetInt("SelectedCube", 0);
        foreach (GameObject cube in cubes)
        {
            cube.SetActive(false);
        }
        cubes[CurrentCubeIndex].SetActive(true);


    }
    void Update()
    {

        ColorChanger();

    }
    void ColorChanger()
    {
        if (redCube.activeSelf)
        {
            RedMat.color = Color.red;
            PurpleMat.color = Color.blue;
            GreenMat.color = Color.green;
        }
        if (purpleCube.activeSelf)
        {
            purpleFlame.SetActive(true);
            RedMat.color = Color.blue;
            PurpleMat.color = Color.green;
            GreenMat.color = Color.red;
        }
        if (greenCube.activeSelf)
        {
            playerScript.speed = 15;
            greenFlame.SetActive(true);
            RedMat.color = Color.green;
            PurpleMat.color = Color.red;
            GreenMat.color = Color.blue;
        }
    }


}
