using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Building Preset", menuName = "New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public int cost; // wieviel kostet ein Gebäude
    public int costPerTurn; // wieviel kostet ein gebäude pro Runde
    public GameObject prefab; // das prefab GameObject, welches Gebäude wird nach Platzierung instanziert

    public int population; // wieviel population gibt uns das Gebäude
    public int jobs; // wieviele Jobs gibt uns das Gebäude
    public int food; // wieviel Nahrung gibt uns das Gebäude


}
