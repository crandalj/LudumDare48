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

    public GameObject EventMenu;
    public Text eventTitle;
    public Text eventText;
    public Text eventChoiceOne;
    public Text eventChoiceTwo;
    public Text eventChoiceThree;

    public GameEvent[] planetEvents;
    public GameEvent[] shipEvents;

    private bool eventActive;
    private GameEvent _currentEvent;

    private Ship _ship;

    // Start is called before the first frame update
    void Start()
    {
        IntroMenu.SetActive(false);
        ActionMenu.SetActive(false);
        HUD.SetActive(false);
        EventMenu.SetActive(false);
        eventActive = false;

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
        if (!eventActive)
        {
            eventActive = true;
            // Get random Planet event
            _currentEvent = planetEvents[Random.Range(0, (int)planetEvents.Length - 1)];
            ShowEvent();
        }
    }

    public void Warp()
    {
        // generate new system

        // randomly select planet

    }

    public void Ship()
    {
        if (!eventActive)
        {
            eventActive = true;
            // Get random Ship event
            _currentEvent = shipEvents[Random.Range(0, (int)shipEvents.Length - 1)];
            ShowEvent();
        }
    }

    private void ShowEvent()
    {
        // disable action menu
        ActionMenu.SetActive(false);
        // update text menu
        eventTitle.text = _currentEvent.eventTitle;
        eventText.text = _currentEvent.eventText;
        eventChoiceOne.text = _currentEvent.choiceOne;
        eventChoiceTwo.text = _currentEvent.choiceTwo;
        eventChoiceThree.text = _currentEvent.choiceThree;
        // enable text menu
        EventMenu.SetActive(true);
    }

    public void ProcessEvent(int choice)
    {
        if (eventActive)
        {

        }
    }
}
