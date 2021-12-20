using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Random = UnityEngine.Random;

public class Tiles : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] public int TileIndex;
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] public SpriteRenderer _renderer;
    private TextMesh _textMesh;
    public Color _color;
    public GameObject grid;
    private bool isActive;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }
    
    private void OnMouseEnter()
    {
        isActive = true;
        //getNeighbours(true);
    }

    private void OnMouseExit()
    {
        isActive = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)

        {
            highlight.SetActive(true);
            _renderer.color = Random.ColorHSV();
            getNeighbours(true);
        }
        
        _textMesh.text = TileIndex.ToString();
        _textMesh.color = Color.black;
        _textMesh.fontSize = 8;
        _textMesh.characterSize = 1;
        _textMesh.anchor = TextAnchor.MiddleCenter;
        
    }

    private void Start()
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(highlight.transform, false);
        transform.localPosition = new Vector3(0,0,0);
        _textMesh = gameObject.GetComponent<TextMesh>();
        _color = Random.ColorHSV();
    }

    private void getNeighbours(bool active)
    {
        Vector3 pos = transform.position;
        Tiles[,] tiles = grid.gameObject.GetComponent<GridManager>().tiles;
        int x = (int) pos.x;
       int y = (int) pos.y;   
      
       for (int i = -1; i < 2; i++)
       {
           for (int j = -1; j < 2; j++)
           {
               
               try
               {
                   if (tiles[i+x, j+y] != null && (i != 0 || j != 0))
                   {
                       setObjectStatus(tiles[i + x,j + y].gameObject);    
                   }
                   
               }
               catch (Exception e)
               {
                   Debug.Log((i+x, j+y));
                   throw;
               } 
              
               
           }
       }
    }
    

    public void setObjectStatus(GameObject gameObject)
    {
        gameObject.GetComponent<Tiles>().highlight.SetActive(true);
        //_renderer.color = _color;
        gameObject.GetComponent<Tiles>()._renderer.color = Random.ColorHSV();
    }
}
