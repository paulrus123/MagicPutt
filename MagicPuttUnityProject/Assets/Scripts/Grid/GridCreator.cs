using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Place this gameobject at the origin of the grid.
 */


public class GridCreator : MonoBehaviour
{
    public int numCellsX;
    public int numCellsZ;
    public float cellSize; //lengthh of a side of the cell (unity units / world meters)


    private Vector3 cellScale;
    private Vector3 cellPosition;

    [SerializeField]
    GameObject cellPrefab = default;

    private void Start()
    {
        cellScale = new Vector3(cellSize, cellSize, cellSize);
    }


    public void InstantiateCells()
    {
        for(int i = 0; i < numCellsX; i++)
        {
            float xPosition = i * cellSize;
            //cellPosition = xPosition;

            for (int j = 0; j < numCellsZ; j++)
            {
                float zPosition = j * cellSize;

                GameObject cell = Instantiate(cellPrefab, this.transform);
                cell.GetComponent<Cell>().xIndex = i;
                cell.GetComponent<Cell>().zIndex = j;
                cell.transform.localScale = cellScale;


                cell.transform.localPosition = cellPosition;
            }
        }
    }
}
