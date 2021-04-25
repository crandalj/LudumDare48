using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject IntroMenu;
    public GameObject ActionMenu;
    public GameObject HUD;

    public Text healthText;
    public Text fuelText;
    public Text moraleText;
    public Text moneyText;

    private Ship _ship;

    // Start is called before the first frame update
    void Start()
    {
        IntroMenu.SetActive(false);
        ActionMenu.SetActive(false);
        HUD.SetActive(false);

        _ship = new Ship();
        Intro();
    }

    public void Intro()
    {
        // show introduction menu
        IntroMenu.SetActive(true);
    }

    public void StartGame()
    {
        // disable intro menu
        IntroMenu.SetActive(false);

        //  trigger Warp 

        // enable action menu & HUD
        ActionMenu.SetActive(true);
        HUD.SetActive(true);
    }

    public void Explore()
    {
        // 
    }

    public void Warp()
    {
        // generate new system

        // randomly select planet

    }

    public void Ship()
    {
        // Open ship menu

    }

    public void NewEvent()
    {

    }
}
