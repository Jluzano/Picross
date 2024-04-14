using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public Image tile;
    public Sprite regularTile;
    public Sprite grayXTile;
    public Sprite blueTile;
    
    //private bool locked = false;
    public int state = 0;
    //State 0 = Blank
    //State 1 = Marked
    //State 2 = X

    private void Start()
    {
        tile = GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
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
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(this.state == 0)
            {
                tile.sprite = grayXTile;
                this.state = 2;
            }
            else if(this.state == 2)
            {
                tile.sprite = regularTile;
                this.state = 0;
            }
        }
    }
}
