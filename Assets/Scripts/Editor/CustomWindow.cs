using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{
    [MenuItem("Window/CustomWindow")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CustomWindow));
    }


    public Dictionary<string, GameObject> ItemPrefabs = new Dictionary<string, GameObject>();
    void OnGUI()
    {
        if (GUILayout.Button("Spawn \"Visualizers\""))
        {
            foreach (var item in GameObject.FindObjectsOfType<GameObject>())
            {
                if(item.name.StartsWith("SPAWN_", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    string[] args = name.Split('_');
                    switch (args[1].Split('(')[0].Trim())
                    {
                        case "Ammo12gauge":
                        case "Ammo44cal":
                        case "Ammo556x45":
                        case "Ammo762x39":
                        case "Ammo9x19":
                            {
                                /*var item = new Ammo(itemType);
                                item.Scale = tor.transform.localScale;
                                (item.Spawn(tor.transform.position, tor.transform.rotation).Base as AmmoPickup).NetworkSavedAmmo = ushort.Parse(args[2]);
                                */break;
                            }

                        case "GunFSP9":
                        case "GunRevolver":
                        case "GunCrossvec":
                        case "GunE11SR":
                        case "GunLogicer":
                        case "GunAK":
                        case "GunShotgun":
                            {
                                /*var item = new Exiled.API.Features.Items.Firearm(itemType);
                                item.Scale = tor.transform.localScale;
                                item.Spawn(tor.transform.position, tor.transform.rotation);
                                */break;
                            }
                    }
                }
            }
        }

        if (GUILayout.Button("Destroy \"Visualizers\""))
        {
            
        }
    }
}
