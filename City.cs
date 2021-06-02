using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class City : MonoBehaviour
{
    public int money; // wieviel Geld hat man im Moment
    public int day; // welcher Tag ist im Moment
    public int currentPop; // wieviele Einwohner hat man im Moment
    public int currentJobs; // wieviele Jobs gibt es im Moment;
    public int currentFood; // wieviele Nahrung gibt es im Moment
    public int maxPopulation; // maximale Bevölkerungszahl
    public int maxJobs; // maximale Job Anzahl
    public int incomePerJob;

    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();

    public static City instance;

    private void Awake()
    {
        instance = this;
    }
     void Start()
    {
        UpdateStatText();
    }
    public void OnPlaceBuilding(Building building )
    {
        money = money - building.preset.cost;

        maxPopulation = maxPopulation + building.preset.population;
        maxJobs = maxJobs + building.preset.jobs;
        buildings.Add(building);

        UpdateStatText();
    }

    public void OnRemoveBuilding(Building building)
    {
        maxPopulation = maxPopulation -  building.preset.population;
        maxJobs = maxJobs - building.preset.jobs;
        buildings.Remove(building);

        Destroy(building.gameObject);

        UpdateStatText();
    }

    // Diese Funktion updated die Information in der UI
    void UpdateStatText()
    {
        statsText.text = string.Format("Day: {0}   Money: ${1}   Pop: {2} / {3}   Jobs: {4} / {5}   Food: {6}"
            , new object[7] { day, money, currentPop, maxPopulation, currentJobs, maxJobs, currentFood });
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    
    public void EndTurn()
    {
        
        Debug.Log("next Day");
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateFood();
        CalculateJobs();
        UpdateStatText();

        if (money <= 0)
        {
            EndGame();
        }
    }

    void CalculateMoney()
    {
        money = money - currentJobs * incomePerJob;

        foreach (Building building in buildings)
        {
            money = money - building.preset.costPerTurn;
        }

        

    }

    void CalculatePopulation()
    {
        if (currentFood >= currentPop && currentPop < maxPopulation)
        {
            currentFood = currentFood - currentPop / 4;
            currentPop = Mathf.Min(currentPop + (currentFood / 4), maxPopulation);
        }
        else if (currentFood < currentPop)
        {
            currentPop = currentFood;
        }
    }

    void CalculateJobs()
    {
        currentJobs = Mathf.Min(currentPop, maxJobs);
    }

    void CalculateFood()
    {
        currentFood = 0;

        foreach (Building building in buildings)
        {
            currentFood = currentFood + building.preset.food;
        }
    }

}
