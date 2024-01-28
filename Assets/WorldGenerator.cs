using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Creature : Entity
{
}

public class Entity
{
    public string name;
    public Location parent; // reference to location where entity is placed.
}
public class Location
{
    public string tileType;
    public Entity occupant;
    public int x;
    public int y;
}

public class WorldGenerator : MonoBehaviour
{

    public int width = 100;
    public int height = 100;
    public float scale = 20f; // Determines the scale of the noise
    public float mountainThreshold = 0.6f; // Threshold for land generation
    public float landThreshold = 0.5f; // Threshold for land generation
    public float sandThreshold = 0.4f; // Threshold for land generation

    public float forestThreshold = 0.6f; // Threshold for forest generation

    public GameObject landTilePrefab;
    public GameObject waterTilePrefab;
    public GameObject sandTilePrefab;

    public GameObject mountainPrefab;


    public GameObject forestPrefab;

    public GameObject hero;
    public Entity heroRef;


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
                GameObject tilePrefab = perlinValue > sandThreshold ? (perlinValue > landThreshold ? landTilePrefab : sandTilePrefab) : (waterTilePrefab);
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);

                tile.transform.parent = this.gameObject.transform;

                world[x, y] = new Location { tileType = tilePrefab.name, occupant = null, x = x, y = y };

                if(perlinValue > mountainThreshold)
                {
                    GameObject t = Instantiate(mountainPrefab, new Vector3(x, y), Quaternion.identity, tile.transform);
                    t.transform.parent = this.transform;
                    world[x, y].occupant = new Entity { name = "mountain", parent = world[x, y] };
                }

                // If it's land, check for forest generation
                if (perlinValue > landThreshold)
                {
                    float forestValue = Mathf.PerlinNoise(x / (scale * 2), y / (scale * 2));
                    if (forestValue > forestThreshold)
                    {
                        GameObject t = Instantiate(forestPrefab, new Vector3(x, y), Quaternion.identity, tile.transform);
                        t.transform.parent = this.transform;
                        world[x, y].occupant = new Entity { name = "tree", parent = world[x, y] };
                    } else
                    {
                        if (!placedHero)
                        {
                            world[x, y].occupant = new Creature { name = "hero", parent = world[x, y] };
                            heroRef = world[x, y].occupant;
                            Debug.Log(heroRef.parent.x + "," + heroRef.parent.y);
                            Debug.Log(world[heroRef.parent.x, heroRef.parent.y].occupant.name);
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

    }

    public void moveOccupant(int srcx, int srcy, int dstx, int dsty)
    {
        Entity temp = world[srcx, srcy].occupant;
        temp.parent = world[dstx, dsty];
        world[srcx, srcy].occupant = null;
        world[dstx, dsty].occupant = temp;

        Debug.Log(temp);
        if(temp.name.Equals("hero"))
        {
            hero.transform.position = new Vector3(dstx, dsty);
        }
    }

    public string look(int srcx, int srcy)
    {
        if(world[srcx, srcy].occupant != null)
        {
            return "You see a " + world[srcx, srcy].occupant.name;
        } else
        {
            return "You see open " + world[srcx, srcy].tileType;
        }
    }
}