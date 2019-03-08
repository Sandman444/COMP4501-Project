using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Temp checkerboard pattern for prototype testing
 * Code based on tutorial in https://joseph-easter.blogspot.com/2016/12/procedural-generation-tutorial-checker.html
 */
public class CheckerBoardPattern : MonoBehaviour
{
    public Texture2D checkerboard;
    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        SetCheckerBoardSize();
        CreatePattern();
    }

    void SetCheckerBoardSize()
    {
        checkerboard = new Texture2D(width, height);
    }

    void CreatePattern()
    {
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (((i + j) % 2) == 1)
                {
                    checkerboard.SetPixel(i, j, Color.black);
                }
                else
                {
                    checkerboard.SetPixel(i, j, Color.white);
                }
            }
        }
        checkerboard.Apply();
        GetComponent<Renderer>().material.mainTexture = checkerboard;
        checkerboard.wrapMode = TextureWrapMode.Clamp;
        checkerboard.filterMode = FilterMode.Point;
    }


}
