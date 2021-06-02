using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Building Preset", menuName = "New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public int cost; // wieviel kostet ein Geb�ude
    public int costPerTurn; // wieviel kostet ein geb�ude pro Runde
    public GameObject prefab; // das prefab GameObject, welches Geb�ude wird nach Platzierung instanziert

    public int population; // wieviel population gibt uns das Geb�ude
    public int jobs; // wieviele Jobs gibt uns das Geb�ude
    public int food; // wieviel Nahrung gibt uns das Geb�ude


}
