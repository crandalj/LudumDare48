using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class GameController : MonoBehaviour
{
    public GameObject IntroMenu;
    public GameObject ActionMenu;
    public GameObject HUD;

    public GameObject GameOverMenu;
    public Text GameOverText;

    public Text healthText;
    public Text fuelText;
    public Text moraleText;
    public Text moneyText;
    public Text systemText;
    public Text distanceText;

    public GameObject EventMenu;
    public Text eventTitle;
    public Text eventText;
    public Text eventChoiceOne;
    public Text eventChoiceTwo;
    public Text eventChoiceThree;

    public GameEvent[] planetEvents;
    public GameEvent[] shipEvents;

    public Material planetMaterial;
    private Color planetColor;

    private bool eventActive;
    private GameEvent _currentEvent;

    private Ship _ship;
    private string _systemName;

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
        systemText.text = "Solar System";
    }

    public void UpdateHUDText()
    {
        healthText.text = _ship.Health + "";
        fuelText.text = _ship.Fuel + "";
        moraleText.text = _ship.Morale + "";
        moneyText.text = _ship.Money + "M";
        systemText.text = _systemName;
        distanceText.text = _ship.Distance + " LY";
    }

    public void StartGame()
    {
        // disable intro menu
        IntroMenu.SetActive(false);

        //  trigger Warp 
        Warp();
        UpdateHUDText();

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
            EventEffectType effectType = EventEffectType.NULL;
            int effectValue = 0;
            EventEffectType costType = EventEffectType.NULL;
            int costValue = 0;

            switch (choice)
            {
                case 1:
                    effectType = _currentEvent.eventEffectTypeOne;
                    effectValue = _currentEvent.eventEffectValueOne;
                    costType = _currentEvent.eventCostTypeOne;
                    costValue = _currentEvent.eventCostValueOne;
                    break;
                case 2:
                    effectType = _currentEvent.eventEffectTypeTwo;
                    effectValue = _currentEvent.eventEffectValueTwo;
                    costType = _currentEvent.eventCostTypeTwo;
                    costValue = _currentEvent.eventCostValueTwo;
                    break;
                case 3:
                    effectType = _currentEvent.eventEffectTypeThree;
                    effectValue = _currentEvent.eventEffectValueThree;
                    costType = _currentEvent.eventCostTypeThree;
                    costValue = _currentEvent.eventCostValueThree;
                    break;
                default:
                    Debug.Log("Failed");
                    break;
            }

            // check if cost can be met
            if(costType != EventEffectType.NULL)
            {
                switch (costType)
                {
                    case EventEffectType.FUEL:
                        if (_ship.Fuel + costValue < 0) return;
                        _ship.UpdateFuel(costValue);
                        break;
                    case EventEffectType.HEALTH:
                        if (_ship.Health + costValue < 0) return;
                        _ship.UpdateHealth(costValue);
                        break;
                    case EventEffectType.MONEY:
                        if (_ship.Money + costValue < 0) return;
                        _ship.UpdateMoney(costValue);
                        break;
                    case EventEffectType.MORALE:
                        if (_ship.Morale + costValue < 0) return;
                        _ship.UpdateMorale(costValue);
                        break;
                    default:
                        Debug.Log("Failed!");
                        break;
                }
            }

            if(effectType != EventEffectType.NULL)
            {
                switch (effectType)
                {
                    case EventEffectType.FUEL:
                        _ship.UpdateFuel(effectValue);
                        break;
                    case EventEffectType.HEALTH:
                        _ship.UpdateHealth(effectValue);
                        break;
                    case EventEffectType.MONEY:
                        _ship.UpdateMoney(effectValue);
                        break;
                    case EventEffectType.MORALE:
                        _ship.UpdateMorale(effectValue);
                        break;
                    default:
                        Debug.Log("Failed!!");
                        break;
                }
            }

            UpdateHUDText();
            EventMenu.SetActive(false);
            ActionMenu.SetActive(true);
            eventActive = false;
        }
    }

    private void UpdatePlanetColor()
    {
        int r = Random.Range(0, 255);
        int g = Random.Range(0, 255);
        int b = Random.Range(0, 255);
        planetColor = new Color(r, g, b);
        planetMaterial.color = planetColor;
    }

    public void GameOver()
    {
        EventMenu.SetActive(false);
        ActionMenu.SetActive(false);
        GameOverMenu.SetActive(true);
        GameOverText.text = "You journeyed into the depths of space, further than any human has gone... maybe, as it was only " + _ship.Distance + " light years from earth.";
    }
}
