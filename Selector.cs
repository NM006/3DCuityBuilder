using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;
    public EventSystem eventSystem;
    public static Selector instance;

    // Singleton erlaubt uns direkt auf dieses Objekt zuzugreifen
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // rufen die Kamera am Start auf
        cam = Camera.main;
    }


    // Hiermit wird die aktuelle Position des Tiles ausgegeben
    public Vector3 GetCurrentTilePosition()
    {
        // wenn man über UI sich befindet, dann wird diese if-Abfrage ausgeführt
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0, -99, 9);
        }
        // erstellt ein Plane RayCast
        // Ziel: bestimmt die position wo die Plane berührt wurde
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        // gibt die Distanz zwischen Kamera und Plane aus
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float rayOut)) // schießt ray auf die plane

        {
            // Position an der wir mit der Plane kollidieren
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0.0f, Mathf.CeilToInt(newPos.z)); // rundet auf/ab
                                                                                              // wird uns die neue Position in rayOut angegeben 
                                                                                              // pinnt
            return newPos;
        }

        return new Vector3(0, -99, 9);

    }

}
