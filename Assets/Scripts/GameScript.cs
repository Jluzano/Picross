using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Nonogram : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject grid;
    public GameObject winText;

    private int gridSize = 5; //size of the grid
    private float cellSpacing = 1.1f; //cell spacing

    private int[,] gridArray; // 0 = unknown, 1 = filled, -1 = empty

    // Clues for rows and columns
    private List<int>[] rowClues = new List<int>[5];
    private List<int>[] columnClues = new List<int>[5];

    // Solution grid, replace with JSON
    private int[,] solutionArray = new int[,]
    {
        { 1, 1, 0, 0, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 1, 1, 0, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 1, 0, 0, 0 }
    };

    void Start()
    {
        gridArray = new int[gridSize, gridSize];

        // Initialize the grid visually
        InitializeGrid();
        //Solve the grid
        //Solve();
    }

    void InitializeGrid()
    {
        //createing a 2d array
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                var newCell = Instantiate(cellPrefab); //creating a cell
                newCell.transform.SetParent(grid.transform); //setting parent to grid
                newCell.transform.localScale = new Vector3(1, 1, 1); // adjusting size

                //spacing between cells
                float xPos = col * cellSpacing;
                float yPos = row * cellSpacing;
                //moving cell into position
                newCell.transform.localPosition = new Vector3(xPos, yPos, 0);

                // Initialize grid array to unknown (0)
                gridArray[row, col] = 0;
            }
        }
    }
    /*
    void Solve()
    {
        //looping through 2d array named gridArray
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                var cell = grid.transform.GetChild(row * gridSize + col).GetComponent<Cell>();

                // Set the cell state based on the solution array
                if (cell.state != solutionArray[row, col])
                {
                    //If the tile state is 0 but is a 1 in the solution array, change state to 1 and color to blue
                    cell.tile.sprite = cell.blueTile;
                    cell.state = 1;
                }
            }
        }
    }
    */
    void UpdateGridArray()
    {
        //Updating the 2d array to be compared with the solution
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                //grabbing the cell
                var cell = grid.transform.GetChild(row * gridSize + col).GetComponent<Cell>();
                gridArray[row, col] = cell.state;
            }
        }
    }

    public void CheckSolution()
    {
        UpdateGridArray();
        bool isCorrect = true;

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                // Ignore cells with state 2 (X)
                if ((gridArray[row, col] == 2) && (solutionArray[row, col] == 0))
                {
                    continue;
                }

                if (gridArray[row, col] != solutionArray[row, col])
                {
                    isCorrect = false;
                    break;
                }
            }
            if (!isCorrect) break;
        }

        if (isCorrect)
        {
            Debug.Log("Finished!");
            winText.SetActive(true);
        }
        else
        {
            Debug.Log("Still incomplete.");
        }
    }
}
