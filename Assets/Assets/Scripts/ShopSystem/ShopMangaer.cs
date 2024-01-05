using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopMangaer : MonoBehaviour
{
    public int CurrentCubeIndex;
    public GameObject[] cubeModels;
    public CubeBluePrints[] cubeBlueprint;
    public Button buyButton;
    public ParticleSystem particle;
    public GameObject blueCubeAbout;
    public GameObject greenCubeAbout;
    public Text CubeText;
    public GameObject enoughText;
    // Start is called before the first frame update
    void Start()
    {
        enoughText.SetActive(false);
        blueCubeAbout.SetActive(false);
        greenCubeAbout.SetActive(false);
        Time.timeScale = 1;
        foreach (CubeBluePrints cube in cubeBlueprint)
        {
            if (cube.price == 0)
            {
                cube.isEnabled = true;
            }
            else
            {
                cube.isEnabled = PlayerPrefs.GetInt(cube.name, 0) == 0 ? false : true;
            }
        }
        CurrentCubeIndex = PlayerPrefs.GetInt("SelectedCube", 0);
        foreach (GameObject cube in cubeModels)
        {
            cube.SetActive(false);
        }
        cubeModels[CurrentCubeIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        AboutCube();

    }
    public void ChangeNext()
    {
        cubeModels[CurrentCubeIndex].SetActive(false);
        CurrentCubeIndex++;
        if (CurrentCubeIndex == cubeModels.Length)
        {
            CurrentCubeIndex = 0;
        }
        cubeModels[CurrentCubeIndex].SetActive(true);
        CubeBluePrints c = cubeBlueprint[CurrentCubeIndex];
        if (!c.isEnabled)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCube", CurrentCubeIndex);
    }
    public void ChangePrevious()
    {
        cubeModels[CurrentCubeIndex].SetActive(false);
        CurrentCubeIndex--;
        if (CurrentCubeIndex == -1)
        {
            CurrentCubeIndex = cubeModels.Length - 1;
        }
        cubeModels[CurrentCubeIndex].SetActive(true);
        CubeBluePrints c = cubeBlueprint[CurrentCubeIndex];
        if (!c.isEnabled)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCube", CurrentCubeIndex);
    }

    public void UnlockCar()
    {

        CubeBluePrints c = cubeBlueprint[CurrentCubeIndex];
        if (c.price <= PlayerPrefs.GetInt("PlayerCube", 0))
        {
            PlayerPrefs.SetInt(c.name, 1);
            PlayerPrefs.SetInt("SelectedBird", CurrentCubeIndex);



            c.isEnabled = true;
            PlayerPrefs.SetInt("PlayerCube", PlayerPrefs.GetInt("PlayerCube", 0) - c.price);
            CubeText.text = PlayerPrefs.GetInt("PlayerCube", 0).ToString();
        }
    }
    private void UpdateUI()
    {
        CubeBluePrints c = cubeBlueprint[CurrentCubeIndex];
        if (c.isEnabled)
        {
            buyButton.gameObject.SetActive(false);
            enoughText.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<Text>().text = "Buy-" + c.price;
            if (c.price <= PlayerPrefs.GetInt("PlayerCube", 0))
            {
                buyButton.interactable = true;
                enoughText.SetActive(false);
            }
            else
            {
                buyButton.interactable = false;
                enoughText.SetActive(true);
            }
        }

    }
    public void BuyButtonParticle()
    {
        enoughText.SetActive(false);
        particle.Play();
        CubeText.text = PlayerPrefs.GetInt("PlayerCube", 0).ToString();
;       
    }
    public void AboutCube()
    {

        if (CurrentCubeIndex == 1)
        {
            blueCubeAbout.SetActive(true);
            greenCubeAbout.SetActive(false);
        }
        if (CurrentCubeIndex == 0)
        {
            blueCubeAbout.SetActive(false);
            greenCubeAbout.SetActive(false);
        }
        if (CurrentCubeIndex == 2)
        {
            blueCubeAbout.SetActive(false);
            greenCubeAbout.SetActive(true);
        }
    }
}

