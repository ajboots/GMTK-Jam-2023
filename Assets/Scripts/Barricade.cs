using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarricadeSize { 
    Small,
    Medium,
    Large
}

public class Barricade : MonoBehaviour
{

    [SerializeField] BarricadeSize size;

    GameObject healthPickup; //Healing potion prefab to spawn when barricade is destroyed

    //Spawning
    [SerializeField] GameObject spawnUnit;
    [SerializeField] List<Transform> spawns;

    //VFX
    [SerializeField] GameObject destructFX;
    [SerializeField] List<Transform> destructSpawns;
    
    


    // Start is called before the first frame update
    void Start()
    {
        int unitsToSpawn = 0;
        switch (size) { 
            case BarricadeSize.Small:
                unitsToSpawn = 2;
                break;
            case BarricadeSize.Medium:
                unitsToSpawn = 4;
                break;
            case BarricadeSize.Large:
                unitsToSpawn = 8;
                break;
        }
        for (int i = 0; i < unitsToSpawn; i++)
        {
            Instantiate(spawnUnit, spawns[i].position, Quaternion.identity);   
        }
    }

    public void Destruct()
    {
        foreach (Transform t in destructSpawns)
        {
            Instantiate(destructFX, t.position, Quaternion.identity);
        }

        Destroy(this.gameObject);

        //Instantiate(healthPickup, transform.position, Quaternion.identity);
    }
}
