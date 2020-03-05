using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Fields

    //the type of the block
    BlockType blockType;

    //array with possible sprites
    Sprite[] sprites = new Sprite[4];

    #endregion

    #region Proprieties

    /// <summary>
    /// a propriety for the type of the block (standard, bonus, freezer or speedup)
    /// </summary>
    public BlockType BlockType
    {
        get { return blockType; }
    }

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        //get sprites from resources
        sprites[0] = Resources.Load<Sprite>(@"sprites\block_green");
        sprites[1] = Resources.Load<Sprite>(@"sprites\block_magenta");
        sprites[2] = Resources.Load<Sprite>(@"sprites\block_purple");
        sprites[3] = Resources.Load<Sprite>(@"sprites\block_yellow");

        //get the block sprite
        Sprite currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        //setting the BlockType variable based on the block sprite
        if (currentSprite == sprites[0])
            blockType = BlockType.Standard;
        else if (currentSprite == sprites[1])
            blockType = BlockType.Bonus;
        else if (currentSprite == sprites[2])
            blockType = BlockType.Freezer;
        else
            blockType = BlockType.SpeedUp;
    }

    #endregion
}
