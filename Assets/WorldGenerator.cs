using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject tree;
    public int worldHeight;
    public int worldWidth;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < worldWidth; x++) {
            for (int y = 0; y < worldHeight; y++) {
                Instantiate(tree, new Vector2(x,y) - new Vector2(worldWidth/2, worldHeight/2), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
