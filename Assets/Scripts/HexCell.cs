using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;
    public Color color;
    public GameObject cityPrefab;
    public HexGrid hexGrid;
    public long population;

    private Color[] popColors = {
        Color.gray, Color.green, Color.yellow, Color.cyan,
        Color.blue, Color.magenta, Color.red, Color.black };

    private GameObject city;   
    private bool activeCity;

    //1.1% growth rate in 2017
    private float growthRate = .011f;

    public bool isMetro;
    
    private static int cityCount;

   
    

    private void Start() {
        hexGrid = GetComponentInParent<HexGrid>();
        cityCount = 3;
        isMetro = false;
        activeCity = false;

        PopulateCities();
        
    }
    private void PopulateCities() {

        if (isMetro) {
            population = Random.Range(800000, 800000);
        } else {
            population = Random.Range(0, 25000);
        }
    }
   

    public void FlagCities() {
       
        if(Random.Range(0f, 1.0f) > .9f) {
            isMetro = true;
            cityCount--;
            population = 800000;
        }
        
    }

    private void FixedUpdate() {
        if (cityCount > 0) {
            FlagCities();
        }
        PopulationGrowth();
    }

    private void PopulationGrowth() {
        //  growthChance = Random.value > .8f ? true : false;
        if (!isMetro) {
            population += (int)(population * growthRate);
        }
        DisplayPopulation();
    }

    private void DisplayPopulation() {

        if(population > 800000) {
            if(activeCity) {
                Destroy(city);
                activeCity = false;
                PopulateCities();
                hexGrid.ColorCell(transform.position, popColors[0]);
            }
        }

        else if (population > 600000) {
            if(activeCity) {
                city.transform.localScale = new Vector3(8, 30, 8);
                hexGrid.ColorCell(transform.position, popColors[popColors.Length - 1]);
            } 
             } 
            else if (population >= 400000) {
                if (activeCity) {
                    city.transform.localScale = new Vector3(8, 10, 8);
                    hexGrid.ColorCell(transform.position, popColors[popColors.Length - 1]);
                }
            } 
            else if (population >= 300000) {
                if (activeCity) {
                    city.transform.localScale = new Vector3(7, 7, 7);
                    hexGrid.ColorCell(transform.position, popColors[6]);
                }
            }
       else if (population >= 250000) {
            if (activeCity) {
                city.transform.localScale = new Vector3(6, 6, 6);
                hexGrid.ColorCell(transform.position, popColors[5]);
            }
        } 
        else if(population >= 180000) {
            if (activeCity) {
                city.transform.localScale = new Vector3(5, 5, 5);
                hexGrid.ColorCell(transform.position, popColors[4]);
            }
        }
        else if(population >= 120000) {
            if(activeCity) {
                city.transform.localScale = new Vector3(4, 4, 4);
                hexGrid.ColorCell(transform.position, popColors[3]);
            }
        } 
        else if( population >= 70000) {
            if(activeCity) {
                city.transform.localScale = new Vector3(3, 3, 3);
                hexGrid.ColorCell(transform.position, popColors[2]);
            }
        }
        if (population >= 25000) {
            if (!activeCity) {
                city = Instantiate(cityPrefab, 
                    new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), 
                    new Quaternion(0f, Random.Range(0f, 360f), 0f, Random.Range(0f, 360f)));               
                activeCity = true;
                hexGrid.ColorCell(transform.position, popColors[1]);
            }
        }
    }
}
