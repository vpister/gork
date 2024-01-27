using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Creature : Entity
{
    public string name;
}

public class Entity
{
    public string name;
}
public class Location
{
    public string tileType;
    public Entity occupant;
}

public class WorldGenerator : MonoBehaviour
{

    public int width = 100;
    public int height = 100;
    public float scale = 20f; // Determines the scale of the noise
    public float landThreshold = 0.5f; // Threshold for land generation
    public float forestThreshold = 0.6f; // Threshold for forest generation

    public GameObject landTilePrefab;
    public GameObject waterTilePrefab;
    public GameObject forestPrefab;

    public GameObject hero;

    public Location[,] world;

    private bool placedHero;

    void Start()
    {
        world = new Location[width,height];
        placedHero = false;
        GenerateWorld(world);
    }

    void GenerateWorld(Location[,] world)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x / scale, y / scale);

                // Create land or water based on the perlin value
                GameObject tilePrefab = perlinValue > landThreshold ? landTilePrefab : waterTilePrefab;
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);

                tile.transform.parent = this.gameObject.transform;

                world[x, y] = new Location { tileType = tilePrefab.name, occupant = null };

                // If it's land, check for forest generation
                if (perlinValue > landThreshold)
                {
                    float forestValue = Mathf.PerlinNoise(x / (scale * 2), y / (scale * 2));
                    if (forestValue > forestThreshold)
                    {
                        GameObject t = Instantiate(forestPrefab, new Vector3(x, y), Quaternion.identity, tile.transform);
                        t.transform.parent = this.transform;
                        world[x, y].occupant = new Entity { name = "tree" };
                    } else
                    {
                        if (!placedHero)
                        {
                            world[x, y].occupant = new Creature { name = "hero" };
                            hero.transform.position = new Vector3(x, y);
                            hero.transform.parent = this.transform;
                            placedHero = true;
                            Debug.Log(x + " " + y);
                            Debug.Log("placed!");
                        }
                    }
                }
            }
        }

        transform.position = new Vector2(-width / 2, -height / 2);
        hero.transform.parent = null;
    }
}