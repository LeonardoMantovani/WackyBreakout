using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    #region Fields

    //spawning support
    GameObject blockPrefab;
    float blockColliderWidth;
    float blockColliderHeight;

    //a dictionary based on a new enumeration for block sprites
    List<Sprite> blockSprites = new List<Sprite>();

    //how many blocks can be spawned in a row
    int blocksInARow;
    int numberOfRows = 3;

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        //get the block prefab and its collider
        blockPrefab = Resources.Load<GameObject>(@"prefabs\Block");
        blockColliderWidth = blockPrefab.GetComponent<BoxCollider2D>().size.x;
        blockColliderHeight = blockPrefab.GetComponent<BoxCollider2D>().size.y;

        //get possible sprites
        blockSprites.Add(Resources.Load<Sprite>(@"sprites\block_green"));
        blockSprites.Add(Resources.Load<Sprite>(@"sprites\block_magenta"));
        blockSprites.Add(Resources.Load<Sprite>(@"sprites\block_purple"));
        blockSprites.Add(Resources.Load<Sprite>(@"sprites\block_yellow"));

        //calculate how many blocks can be spawned in a row
        blocksInARow = (int)(ScreenUtils.ScreenWidth / blockColliderWidth);

        for (int i = 0; i < numberOfRows; i++)
        {
            SpawnARow(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Return a "Random" Sprite based on block percentage from the Configuration Data File
    /// </summary>
    Sprite GetSprite()
    {
        int rand = Random.Range(0, 100);

        float standardRange = ConfigurationUtils.StandardBlocksPercentage;
        float bonusRange = standardRange + ConfigurationUtils.BonusBlocksPercentage;
        float freezerRange = bonusRange + ConfigurationUtils.FreezerBlocksPercentage;
        float speedupRange = freezerRange + ConfigurationUtils.SpeedUpBlocksPercentage;

        if (rand <= standardRange)
        {
            return blockSprites[0];
        }
        else if (rand > standardRange && rand <= bonusRange)
        {
            return blockSprites[1];
        }
        else if (rand > bonusRange && rand <= freezerRange)
        {
            return blockSprites[2];
        }
        else
        {
            return blockSprites[3];
        }
    }

    void SpawnARow(int rowNumber)
    {
        //get the Y of the row
        float rowY = ScreenUtils.ScreenTop - (blockColliderHeight / 2) - (blockColliderHeight * rowNumber);

        //create Vector3 for the spawn positions and set both of them to the middle of the screen x
        Vector3 leftSpawnPosition = new Vector3(0, rowY, 0);
        Vector3 rightSpawnPosition = leftSpawnPosition;

        //spawn the first block in the middle of the screen x
        GameObject middleBlock = Instantiate<GameObject>(blockPrefab, leftSpawnPosition, Quaternion.identity);
        middleBlock.GetComponent<SpriteRenderer>().sprite = GetSprite();

        for (int blockSpawned = 1; blockSpawned < blocksInARow; blockSpawned += 2)
        {
            leftSpawnPosition.x += blockColliderWidth;
            rightSpawnPosition.x -= blockColliderWidth;

            //spawn a block to the left
            GameObject leftBlock = Instantiate<GameObject>(blockPrefab, leftSpawnPosition, Quaternion.identity);
            leftBlock.GetComponent<SpriteRenderer>().sprite = GetSprite();

            //spawn a block to the right
            GameObject rightBlock = Instantiate<GameObject>(blockPrefab, rightSpawnPosition, Quaternion.identity);
            rightBlock.GetComponent<SpriteRenderer>().sprite = GetSprite();
        }
    }

    #endregion
}
