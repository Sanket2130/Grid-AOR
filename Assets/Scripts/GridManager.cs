using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tiles tilePrefab;
    [SerializeField] private new Transform camera;
    public Tiles[,] tiles; 
    private void Start()
    {
        tiles = new Tiles[width,height];
        GenerateGrid();
    }


    void GenerateGrid()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.gameObject.GetComponent<Tiles>().TileIndex = count;
                spawnedTile.gameObject.GetComponent<Tiles>().grid = transform.gameObject;
                tiles[x,y] = spawnedTile;
                count ++;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        camera.transform.position = new Vector3((float)width/2 -.5f,(float)height/2 -.5f,-10 );
    }
}
