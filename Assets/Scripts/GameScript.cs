using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject cell;
    public GameObject grid;
    
    int[] solution;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 25; i++)
        {
            var test = Instantiate(cell);
            test.transform.SetParent(grid.transform);
            test.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int test2 = cell.state;
    }
}
