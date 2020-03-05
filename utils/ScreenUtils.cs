using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Initialize() method calculate game world edges and saves them into public proprieties
/// </summary>
public static class ScreenUtils
{
    #region fields

    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    #endregion fields

    #region proprieties

    public static float ScreenLeft
    {
        get { return screenLeft; }
    }
    public static float ScreenRight
    {
        get { return screenRight; }
    }
    public static float ScreenTop
    {
        get { return screenTop; }
    }
    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    /// <summary>
    /// a propriety for the screen width (which is 2 times the Right edge x)
    /// </summary>
    public static float ScreenWidth
    {
        get { return 2 * screenRight; }
    }

    /// <summary>
    /// a propriety for the screen height (which is 2 times the Top edge y)
    /// </summary>
    public static float ScreenHeight
    {
        get { return 2 * screenTop; }
    }

    #endregion proprieties

    #region methods

    public static void Initialize()
    {
        //save screen Z in a variable
        float screenZ = -Camera.main.transform.position.z;

        //save 2 opposite corners of the screen (lower-left and upper right)
        Vector3 lowerLeftCorner = new Vector3(0, 0, screenZ);
        Vector3 upperRightCorner = new Vector3(Screen.width, Screen.height, screenZ);

        //convert the 2 corners in game world cordinates
        lowerLeftCorner = Camera.main.ScreenToWorldPoint(lowerLeftCorner);
        upperRightCorner = Camera.main.ScreenToWorldPoint(upperRightCorner);

        //save 4 screen edges in world cordinates
        screenLeft = lowerLeftCorner.x;
        screenRight = upperRightCorner.x;
        screenTop = upperRightCorner.y;
        screenBottom = lowerLeftCorner.y;
    }

    #endregion methods
}
