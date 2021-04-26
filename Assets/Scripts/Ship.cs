using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    private int _morale;
    private int _fuel;
    private int _health;
    private int _money;
    private int _distance;

    public int Morale { get { return _morale; } set { _morale = value; } }
    public int Fuel { get { return _fuel; } set { _fuel = value; } }
    public int Health { get { return _health; } set { _health = value; } }
    public int Money { get { return _money; } set { _money = value; } }
    public int Distance { get { return _distance; } set { _distance = value; } }

    public Ship()
    {
        _morale = 100;
        _fuel = 100;
        _health = 100;
        _money = 50;
        _distance = 0;
    }

    public void UpdateMorale(int value)
    {
        _morale += value;

        if (_morale > 100) _morale = 100;
        if (_morale <= 0) GameController.Instance.GameOver();
    }

    public void UpdateFuel(int value)
    {
        _fuel += value;

        if (_fuel > 100) _fuel = 100;
        if (_fuel < 0) _fuel = 0; 
    }

    public void UpdateHealth(int value)
    {
        _health += value;

        if (_health > 100) _health = 100;
        if (_health <= 0) GameController.Instance.GameOver();
    }

    public void UpdateMoney(int value)
    {
        _money += value;
    }

    public void UpdateDistance(int value)
    {
        _distance += value;
    }
}
