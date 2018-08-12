using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour
{
    public Tilemap tileMap;

    public List<Vector3> availablePlaces;

    public static Field instance;

    public List<bool> field;

    private int xMin;
    private int xMax;
    private int yMin;
    private int yMax;
    private bool[,] matrix;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {

        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                    var min = FindMin(availablePlaces);
                   
                    field.Add(false);

                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }


    public void FindMin(List<Vector3> vectors)
    {
        xMin = int.MinValue;
        xMax = int.MaxValue;
        yMin = int.MinValue;
        yMax = int.MaxValue;
        foreach (var vector3 in vectors)
        {
            if (xMin > vector3.x)
            {
                xMin = (int)vector3.x;
            }
            if (xMax < vector3.x)
            {
                xMax = (int)vector3.x;
            }
            if (yMin > vector3.x)
            {
                yMin = (int)vector3.x;
            }
            if (yMax < vector3.x)
            {
                yMax = (int)vector3.x;
            }
        }
        //for (int i = 0; i < availablePlaces.Count; i++)
        //{
        //    if (xMin > (int) vectors[i + 1].x)
        //        xMin = (int) vectors[i + 1].x;

        //    if (xMax < (int) vectors[i + 1].x)
        //        xMax = (int) vectors[i + 1].x;
        //}
    }

    private List<Vector3> ShiftVector(List<Vector3> vectors)
    {
        List<Vector3> shiftedVectors = new List<Vector3>();
        foreach (var vector3 in vectors)
        {
            shiftedVectors.Add(new Vector3(vector3.x + Mathf.Abs(xMin),vector3.y + Mathf.Abs(yMin), vector3.z));
        }

        return shiftedVectors;
    }

    public void VectorToMatrix()
    {
        matrix = new bool[xMax + Mathf.Abs(xMin), yMax + Mathf.Abs(yMin)];
        for (int i = 0; i < UPPER; i++)
        {
            
        }
    }

    public void Update()
    {
        //tileMap = transform.GetComponentInParent<Tilemap>();
       
    }

    public void AddElements(Vector3Int objectsCoordinates)
    {
        foreach (var availablePlace in availablePlaces)
        {
            if (availablePlace == objectsCoordinates)
            {
                instance.field[availablePlaces.IndexOf(availablePlace)] = true;
            }
        }
    }
}
