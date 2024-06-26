using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image tile;
    public Sprite regularTile;
    public Sprite grayXTile;
    public Sprite blueTile;

    public int state = 0;
    // State 0 = Blank
    // State 1 = Marked
    // State 2 = X

    private bool isLeftPointerDown = false;
    private bool isRightPointerDown = false;
    private static HashSet<Cell> processedCells = new HashSet<Cell>();

    private Nonogram nonogramScript;

    private void Start()
    {
        tile = GetComponent<Image>();
        nonogramScript = FindObjectOfType<Nonogram>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isLeftPointerDown = true;
            processedCells.Clear();
            ChangeTileState(true);
            processedCells.Add(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            isRightPointerDown = true;
            processedCells.Clear();
            ChangeTileState(false);
            processedCells.Add(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isLeftPointerDown = false;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            isRightPointerDown = false;
        }
        processedCells.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((isLeftPointerDown || isRightPointerDown) && eventData.pointerEnter != null)
        {
            Cell cell = eventData.pointerEnter.GetComponent<Cell>();
            if (cell != null && !processedCells.Contains(cell))
            {
                if (isLeftPointerDown)
                {
                    cell.ChangeTileState(true);
                }
                else if (isRightPointerDown)
                {
                    cell.ChangeTileState(false);
                }
                processedCells.Add(cell);
            }
        }
    }

    private void ChangeTileState(bool isLeftClick)
    {
        if (isLeftClick)
        {
            if (this.state == 0)
            {
                tile.sprite = blueTile;
                this.state = 1;
            }
            else if (this.state == 1)
            {
                tile.sprite = regularTile;
                this.state = 0;
            }
        }
        else
        {
            if (this.state == 0)
            {
                tile.sprite = grayXTile;
                this.state = 2;
            }
            else if (this.state == 2)
            {
                tile.sprite = regularTile;
                this.state = 0;
            }
        }

        if (nonogramScript != null)
        {
            nonogramScript.CheckSolution();
        }
    }
}
