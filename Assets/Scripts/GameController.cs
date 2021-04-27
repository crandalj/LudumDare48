using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

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

    public GameObject EventResultMenu;
    public Text eventResultText;

    public Renderer planetRenderer;
    private Color planetColor;

    private bool eventActive;
    private GameEvent _currentEvent;

    private bool isGameover;
    private Ship _ship;
    private string _systemName;

    // Start is called before the first frame update
    void Start()
    {
        isGameover = false;
        Instance = this;
        IntroMenu.SetActive(false);
        ActionMenu.SetActive(false);
        HUD.SetActive(false);
        EventMenu.SetActive(false);
        EventResultMenu.SetActive(false);
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
            //_currentEvent = planetEvents[Random.Range(0, (int)planetEvents.Length - 1)];
            _currentEvent = GeneratePlanetEvent();
            ShowEvent();
        }
    }

    public void Warp()
    {
        if(_ship.Fuel >= 25)
        {
            // generate new system
            _systemName = _systemNames[Random.Range(0, _systemNames.Length - 1)];
            // update resources
            _ship.UpdateDistance(Random.Range(75, 150));
            _ship.UpdateFuel(-25);

            // randomly select planet
            UpdatePlanetColor();

            UpdateHUDText();
        }
    }

    public void Ship()
    {
        if (!eventActive)
        {
            eventActive = true;
            // Get random Ship event
            // _currentEvent = shipEvents[Random.Range(0, (int)shipEvents.Length - 1)];
            _currentEvent = GenerateShipEvent();
            ShowEvent();
        }
    }

    public GameEvent GenerateShipEvent()
    {
        GameEvent gameEvent = new GameEvent();

        Type type = typeof(ShipEventType);
        Array values = type.GetEnumValues();
        int index = Random.Range(0, values.Length - 1);
        ShipEventType eventType = (ShipEventType)values.GetValue(index);

        gameEvent.eventTitle = eventType.ToString();
        gameEvent.eventText = "The crew would like to focus on " + eventType.ToString() + "!";

        // assign event types based on main event type
        switch (eventType)
        {
            case ShipEventType.BUILD:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.HEALTH;
                gameEvent.eventCostTypeOne = EventEffectType.MONEY;
                gameEvent.eventEffectValueOne = Random.Range(5, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 25);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.FUEL;
                gameEvent.eventCostTypeTwo = EventEffectType.MONEY;
                gameEvent.eventEffectValueTwo = Random.Range(5,25);
                gameEvent.eventCostValueTwo = -Random.Range(5, 25);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.MORALE;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
            case ShipEventType.DISCIPLINE:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.HEALTH;
                gameEvent.eventCostTypeOne = EventEffectType.MORALE;
                gameEvent.eventEffectValueOne = Random.Range(5, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 15);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.HEALTH;
                gameEvent.eventCostTypeTwo = EventEffectType.MORALE;
                gameEvent.eventEffectValueTwo = Random.Range(25, 75);
                gameEvent.eventCostValueTwo = -Random.Range(25, 55);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.HEALTH;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
            case ShipEventType.MAINTENANCE:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.HEALTH;
                gameEvent.eventCostTypeOne = EventEffectType.MORALE;
                gameEvent.eventEffectValueOne = Random.Range(5, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 25);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.HEALTH;
                gameEvent.eventCostTypeTwo = EventEffectType.MORALE;
                gameEvent.eventEffectValueTwo = Random.Range(25, 75);
                gameEvent.eventCostValueTwo = -Random.Range(25, 75);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.HEALTH;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(15, 25);
                gameEvent.eventCostValueThree = 0;
                break;
            case ShipEventType.REST:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.MORALE;
                gameEvent.eventCostTypeOne = EventEffectType.MONEY;
                gameEvent.eventEffectValueOne = Random.Range(20, 45);
                gameEvent.eventCostValueOne = -Random.Range(5, 25);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.MORALE;
                gameEvent.eventCostTypeTwo = EventEffectType.MONEY;
                gameEvent.eventEffectValueTwo = Random.Range(50, 75);
                gameEvent.eventCostValueTwo = -Random.Range(30, 50);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.MORALE;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
        }
        // assign choice based on event type and value

        gameEvent.choiceOne = "Get " + gameEvent.eventEffectTypeOne.ToString();
        if (gameEvent.eventCostTypeOne != EventEffectType.NULL) gameEvent.choiceOne = "Spend " + gameEvent.eventCostValueOne + " " + gameEvent.eventCostTypeOne.ToString() + " for " + gameEvent.eventEffectTypeOne.ToString();
        if (gameEvent.eventEffectValueOne < 0 && gameEvent.eventEffectTypeOne != EventEffectType.NULL && gameEvent.eventCostTypeOne == EventEffectType.NULL) gameEvent.choiceOne = "Lose " + gameEvent.eventEffectValueOne + " " + gameEvent.eventEffectTypeOne.ToString();
        gameEvent.choiceTwo = "Get " + gameEvent.eventEffectTypeTwo.ToString();
        if (gameEvent.eventCostTypeTwo != EventEffectType.NULL) gameEvent.choiceTwo = "Spend " + gameEvent.eventCostValueTwo + " " + gameEvent.eventCostTypeTwo.ToString() + " for " + gameEvent.eventEffectTypeTwo.ToString();
        if (gameEvent.eventEffectValueTwo < 0 && gameEvent.eventEffectTypeTwo != EventEffectType.NULL && gameEvent.eventCostTypeTwo == EventEffectType.NULL) gameEvent.choiceTwo = "Lose " + gameEvent.eventEffectValueTwo + " " + gameEvent.eventEffectTypeTwo.ToString();
        gameEvent.choiceThree = "Get " + gameEvent.eventEffectTypeThree.ToString();
        if (gameEvent.eventCostTypeThree != EventEffectType.NULL) gameEvent.choiceThree = "Spend " + gameEvent.eventCostValueThree + " " + gameEvent.eventCostTypeThree.ToString() + " for " + gameEvent.eventEffectTypeThree.ToString();
        if (gameEvent.eventEffectValueThree < 0 && gameEvent.eventEffectTypeThree != EventEffectType.NULL && gameEvent.eventCostTypeThree == EventEffectType.NULL) gameEvent.choiceThree = "Lose " + gameEvent.eventEffectValueThree + " " + gameEvent.eventEffectTypeThree.ToString();
        return gameEvent;
    }

    public GameEvent GeneratePlanetEvent()
    {
        GameEvent gameEvent = new GameEvent();

        Type type = typeof(PlanetEventType);
        Array values = type.GetEnumValues();
        int index = Random.Range(0, values.Length - 1);
        PlanetEventType eventType = (PlanetEventType)values.GetValue(index);

        gameEvent.eventTitle = eventType.ToString();
        gameEvent.eventText = "The crew would like to focus on " + eventType.ToString() + "!";

        // assign event types based on main event type
        switch (eventType)
        {
            case PlanetEventType.COMBAT:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.FUEL;
                gameEvent.eventCostTypeOne = EventEffectType.HEALTH;
                gameEvent.eventEffectValueOne = Random.Range(10, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 15);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.FUEL;
                gameEvent.eventCostTypeTwo = EventEffectType.HEALTH;
                gameEvent.eventEffectValueTwo = Random.Range(50, 75);
                gameEvent.eventCostValueTwo = -Random.Range(25, 40);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.HEALTH;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(15, 25);
                gameEvent.eventCostValueThree = 0;
                break;
            case PlanetEventType.INVESTIGATE:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.MONEY;
                gameEvent.eventCostTypeOne = EventEffectType.FUEL;
                gameEvent.eventEffectValueOne = Random.Range(15, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 15);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.MONEY;
                gameEvent.eventCostTypeTwo = EventEffectType.MORALE;
                gameEvent.eventEffectValueTwo = Random.Range(45, 75);
                gameEvent.eventCostValueTwo = -Random.Range(25, 50);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.HEALTH;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
            case PlanetEventType.LAND:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.MORALE;
                gameEvent.eventCostTypeOne = EventEffectType.NULL;
                gameEvent.eventEffectValueOne = Random.Range(5, 25);
                gameEvent.eventCostValueOne = 0;
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.HEALTH;
                gameEvent.eventCostTypeTwo = EventEffectType.NULL;
                gameEvent.eventEffectValueTwo = Random.Range(5, 25);
                gameEvent.eventCostValueTwo = 0;
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.FUEL;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = Random.Range(5, 25);
                gameEvent.eventCostValueThree = 0;
                break;
            case PlanetEventType.MAP:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.MONEY;
                gameEvent.eventCostTypeOne = EventEffectType.HEALTH;
                gameEvent.eventEffectValueOne = Random.Range(20, 45);
                gameEvent.eventCostValueOne = -Random.Range(5, 25);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.MONEY;
                gameEvent.eventCostTypeTwo = EventEffectType.MORALE;
                gameEvent.eventEffectValueTwo = Random.Range(20, 45);
                gameEvent.eventCostValueTwo = -Random.Range(5, 25);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.MONEY;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
            case PlanetEventType.MINE:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.FUEL;
                gameEvent.eventCostTypeOne = EventEffectType.MORALE;
                gameEvent.eventEffectValueOne = Random.Range(15, 25);
                gameEvent.eventCostValueOne = -Random.Range(5, 15);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.FUEL;
                gameEvent.eventCostTypeTwo = EventEffectType.MORALE;
                gameEvent.eventEffectValueTwo = Random.Range(25, 50);
                gameEvent.eventCostValueTwo = -Random.Range(15, 25);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.FUEL;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
            case PlanetEventType.RESEARCH:
                // choice 1
                gameEvent.eventEffectTypeOne = EventEffectType.MONEY;
                gameEvent.eventCostTypeOne = EventEffectType.MONEY;
                gameEvent.eventEffectValueOne = Random.Range(15, 50);
                gameEvent.eventCostValueOne = -Random.Range(5, 40);
                // choice 2
                gameEvent.eventEffectTypeTwo = EventEffectType.MONEY;
                gameEvent.eventCostTypeTwo = EventEffectType.MONEY;
                gameEvent.eventEffectValueTwo = Random.Range(5, 25);
                gameEvent.eventCostValueTwo = -Random.Range(5, 25);
                // choice 3
                gameEvent.eventEffectTypeThree = EventEffectType.HEALTH;
                gameEvent.eventCostTypeThree = EventEffectType.NULL;
                gameEvent.eventEffectValueThree = -Random.Range(5, 15);
                gameEvent.eventCostValueThree = 0;
                break;
        }
        // assign choice based on event type and value

        gameEvent.choiceOne = "Get " + gameEvent.eventEffectTypeOne.ToString();
        if (gameEvent.eventCostTypeOne != EventEffectType.NULL) gameEvent.choiceOne = "Spend " + gameEvent.eventCostValueOne + " " + gameEvent.eventCostTypeOne.ToString() + " for " + gameEvent.eventEffectTypeOne.ToString();
        if (gameEvent.eventEffectValueOne < 0 && gameEvent.eventEffectTypeOne != EventEffectType.NULL && gameEvent.eventCostTypeOne == EventEffectType.NULL) gameEvent.choiceOne = "Lose " + gameEvent.eventEffectValueOne + " " + gameEvent.eventEffectTypeOne.ToString();
        gameEvent.choiceTwo = "Get " + gameEvent.eventEffectTypeTwo.ToString();
        if (gameEvent.eventCostTypeTwo != EventEffectType.NULL) gameEvent.choiceTwo = "Spend " + gameEvent.eventCostValueTwo + " " + gameEvent.eventCostTypeTwo.ToString() + " for " + gameEvent.eventEffectTypeTwo.ToString();
        if (gameEvent.eventEffectValueTwo < 0 && gameEvent.eventEffectTypeTwo != EventEffectType.NULL && gameEvent.eventCostTypeTwo == EventEffectType.NULL) gameEvent.choiceTwo = "Lose " + gameEvent.eventEffectValueTwo + " " + gameEvent.eventEffectTypeTwo.ToString();
        gameEvent.choiceThree = "Get " + gameEvent.eventEffectTypeThree.ToString();
        if (gameEvent.eventCostTypeThree != EventEffectType.NULL) gameEvent.choiceThree = "Spend " + gameEvent.eventCostValueThree + " " + gameEvent.eventCostTypeThree.ToString() + " for " + gameEvent.eventEffectTypeThree.ToString();
        if (gameEvent.eventEffectValueThree < 0 && gameEvent.eventEffectTypeThree != EventEffectType.NULL && gameEvent.eventCostTypeThree == EventEffectType.NULL) gameEvent.choiceThree = "Lose " + gameEvent.eventEffectValueThree + " " + gameEvent.eventEffectTypeThree.ToString();
        return gameEvent;
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
                eventResultText.text = "You spent " + costValue + " " + costType.ToString();
                if(effectType != EventEffectType.NULL)
                {
                    eventResultText.text += " and received " + effectValue + " " + effectType.ToString();
                }
            }
            else
            {
                eventResultText.text = "Your ship received " + effectValue + " " + effectType.ToString();
            }

            

            if (effectType != EventEffectType.NULL)
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
            EventResultMenu.SetActive(true);
        }
    }

    public void CloseEvent()
    {
        if (!isGameover)
        {         
            ActionMenu.SetActive(true);
            eventActive = false;
        }
        EventResultMenu.SetActive(false);
    }

    private void UpdatePlanetColor()
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);
        planetColor = new Color(r,g,b, 1);
        //planetRenderer.material.SetColor("planet", planetColor);
        planetRenderer.material.color = planetColor;
    }

    public void GameOver()
    {
        isGameover = true;
        EventMenu.SetActive(false);
        ActionMenu.SetActive(false);
        GameOverMenu.SetActive(true);
        GameOverText.text = "You journeyed into the depths of space, further than any human has gone... maybe, as it was only " + _ship.Distance + " light years from earth.";
    }

    private string[] _systemNames = new string[]
    {
        "Kameeraska",
        "Whiteridge",
        "Edinborourgh",
        "Kameeraska",
        "Orkney",
        "Southwold",
        "Craydon",
        "Holden",
        "Myrefall",
        "Limesvilles",
        "Vustroria",
        "Hengaetov",
        "Benrigawa",
        "Tholov",
        "Hidilles",
        "Cocarro",
        "Ludenia",
        "Dretanus",
        "Gnilles AY",
        "Kinocury",
        "Yusomia",
        "Lutis",
        "Muchabos",
        "Yewei"
    };
}
