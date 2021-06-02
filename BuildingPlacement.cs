using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing; // wir gerade ein Geb�ude geb�ut
    private bool currentlyBulldozering; // wird gerade gebulldozert

    private BuildingPreset currentBuildingPreset; // PresetObjekt wird gespeichert in:

    /*wir m�chten nicht jeden Frame damit verbrauchen einen Ray zu rufen
     * reduzieren also die UpdateRate
     */
    private float indicatorUpdateRate = 0.05f; // alle 0.05 sekunden wird die position upgedated
    private float lastUpdateTime; // die letzte bekannte Update Zeit
    private Vector3 currentIndicatorPosition; // Variable f�r derzeitige IndicatorPosition

    public GameObject placementIndicator; // Variable f�r PlatzierungsIndikator
    public GameObject bulldozeIndicator; // Variable f�r BulldozerIndikator

    public AudioSource audioSource;

    private void Update()
    {
        // Hier wird nach dr�cken von Escape in das Hauptmen� zur�ckgekehrt
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape gedr�ckt");
            SceneManager.LoadScene("MainMenu");
        }

        // wenn wir die Rechte Maustaste dr�cken dann wird Geb�udePlatzierung beendet
        if (Input.GetMouseButtonDown(1)) 
        {
            CancelBuildingPlacement();
            CancelBulldoze();
        }

        
        // wird alle 0.05 Sekunden gerufen
        if (Time.time - lastUpdateTime > indicatorUpdateRate)
        {
            lastUpdateTime = Time.time;
            // holt sich die aktuelle ausgew�hlte Tile Position
            currentIndicatorPosition = Selector.instance.GetCurrentTilePosition();

            if (currentlyPlacing)
            {
                placementIndicator.transform.position = currentIndicatorPosition;
            }
            else if (currentlyBulldozering)
            {
                bulldozeIndicator.transform.position = currentIndicatorPosition;
            }
        }
        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
        {
            PlaceBuilding();
            audioSource.Play();
        }
        
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
        {
            Bulldoze();
        }

        
    }
    // Funktion die gerufen wird sobald ein Geb�udebutton in UI gedr�ckt wird
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        //if (City.instance.money < preset.cost)
        //{
        //    return;
        //}


        currentlyPlacing = true; // wir setzen ein Geb�ude = wahr
        currentBuildingPreset = preset; // ausgew�hltes Geb�ude wird zum preset
        placementIndicator.SetActive(true); // Aktivierung von PlatzierungsIndikator
        placementIndicator.transform.position = new Vector3(0, -99, 0);
    }

    // wenn aufgerufen, dann wird Geb�udePlatzierung abgebrochen
    public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }

    // Toggled Bulldoze an bzw. aus
    public void ToggleBulldoze()
    {
        currentlyBulldozering = !currentlyBulldozering;
        bulldozeIndicator.SetActive(currentlyBulldozering);
        bulldozeIndicator.transform.position = new Vector3(0, -99, 0);
    }

    void PlaceBuilding()
    {

        if (currentBuildingPreset.prefab)
        {


            Debug.Log("Funktioniert");
            GameObject buildingObj = Instantiate(currentBuildingPreset.prefab, currentIndicatorPosition, Quaternion.identity);
            City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());

            
            
        }
        else
        {
            CancelBuildingPlacement();

        }


    }

    void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == currentIndicatorPosition);
        if (buildingToDestroy != null)
        {
            Debug.Log("Bulldozing Active");
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
        else
        {
            CancelBulldoze();

        }
    } 

    public void CancelBulldoze()
    {
        currentlyBulldozering = true;
        bulldozeIndicator.SetActive(false);
        bulldozeIndicator.transform.position = new Vector3(0, -99, 0);
    }


}
