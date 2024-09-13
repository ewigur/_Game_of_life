using UnityEngine;
public class _Grid : MonoBehaviour
{
    GameObject[,] myCells;

    [Header("Cell Attributes")]
    [SerializeField] int CellRows = 10;
    [SerializeField] int CellColumns = 10;
    [SerializeField] float CellScale = 1.5f;

    [Header("Speed")]
    [SerializeField] float SpeedOfSimulation = 4f;

    void Start()
    {
        Application.targetFrameRate = 4; 
        GridCreation();
        CellCheck();        
    }

    private void Update()
    {
        Application.targetFrameRate = (int)SpeedOfSimulation;
        NeighborCheck();        
    }

    public void GridCreation()
    {

        myCells = new GameObject[CellRows, CellColumns];

        var texture = new Texture2D(16, 16);

        Vector2 ScreenBounds = new();

        ScreenBounds.x = Camera.main.orthographicSize * Camera.main.aspect * 2;
        ScreenBounds.y = Camera.main.orthographicSize * 2;

        float CellSizeX = ScreenBounds.x / CellRows;
        float CellSizeY = ScreenBounds.y / CellColumns;

        float CellStartX = Camera.main.transform.position.x - (ScreenBounds.x * 0.5f) + (CellSizeX * 0.5f);
        float CellStartY = Camera.main.transform.position.y - (ScreenBounds.y * 0.5f) + (CellSizeY * 0.5f);


        for (int x = 0; x < CellRows; x++)
        {
            for (int y = 0; y < CellColumns; y++)
            {
                var cellObject = new GameObject();

                cellObject.transform.parent = transform;

                SpriteRenderer spriteRenderer = cellObject.AddComponent<SpriteRenderer>();

                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                spriteRenderer.color = Color.green;
                //spriteRenderer.color = new Color(Random.value, Random.value, Random.value); // Random color

                float scaleX = CellSizeX / texture.width * CellScale;
                float scaleY = CellSizeY / texture.height * CellScale;

                cellObject.transform.localScale = new Vector2(scaleX, scaleY);

                myCells[x, y] = cellObject;

                float posX = CellStartX + (x * CellSizeX);
                float posY = CellStartY + (y * CellSizeY);

                cellObject.transform.position = new Vector3(posX, posY, 0);
            }
        }        
    }

    public void LiveOrDead(int x, int y)
    {             
        int spawnChancePercentage = 15;

        int randomSpawn = Random.Range(0, 100);


        if (randomSpawn < spawnChancePercentage)
        {
            myCells[x, y].SetActive(true);
 
        }

        else
        {
            myCells[x, y].SetActive(false);

        }
    }

    public void CellCheck()
    {
        for (int x = 0; x < CellRows; x++)
        {
            for (int y = 0; y < CellColumns; y++)
            {

                if (x + 1 < CellRows && myCells[x + 1, y] != null)
                {
                    LiveOrDead(x + 1, y);
                }


                if (y + 1 < CellColumns && myCells[x, y + 1] != null)
                {
                    LiveOrDead(x, y + 1);
                }


                if (x + 1 < CellRows && y + 1 < CellColumns && myCells[x + 1, y + 1] != null)
                {
                    LiveOrDead(x + 1, y + 1);
                }


                if (x - 1 >= 0 && myCells[x - 1, y] != null)
                {
                    LiveOrDead(x - 1, y);
                }


                if (y - 1 >= 0 && myCells[x, y - 1] != null)
                {
                    LiveOrDead(x, y - 1);
                }


                if (x - 1 >= 0 && y - 1 >= 0 && myCells[x - 1, y - 1] != null)
                {
                    LiveOrDead(x - 1, y - 1);
                }


                if (x - 1 >= 0 && y + 1 < CellColumns && myCells[x - 1, y + 1] != null)
                {
                    LiveOrDead(x - 1, y + 1);
                }


                if (x + 1 < CellRows && y - 1 >= 0 && myCells[x + 1, y - 1] != null)
                {
                    LiveOrDead(x + 1, y - 1);
                }
            }
        }
    }

    private void NeighborCheck()
    {
        
     
        bool[,] NextGen = new bool[CellRows, CellColumns];

        for (int x = 0; x < CellRows; x++)
        {
            for (int y = 0; y < CellColumns; y++)
            {
                int liveNeighborCount = 0;
                int[] CheckX = { -1, 0, 1, 0, -1, 1, -1, 1 };
                int[] CheckY = { 0, 1, 0, -1, -1, -1, 1, 1 };

                for (int i = 0; i < CheckX.Length; i++)
                {

                        int neighborX = x + CheckX[i];
                        int neighborY = y + CheckY[i];

                        if (neighborX >= 0 && neighborX < CellRows && neighborY >= 0 && neighborY < CellColumns)
                        {
                            if (myCells[neighborX, neighborY] != null && myCells[neighborX, neighborY].activeSelf)
                            {
                                liveNeighborCount++;
                            }
                        }

                    
                }

                bool isAlive = myCells[x, y] != null && myCells[x, y].activeSelf;

                if (isAlive)
                {
                    // Rule 1: Any live cell with fewer than two live neighbors dies (underpopulation)
                    // Rule 3: Any live cell with more than three live neighbors dies (overpopulation)
                    if (liveNeighborCount < 2 || liveNeighborCount > 3)
                    {
                        NextGen[x, y] = false;
                    }

                    else
                    {
                        NextGen[x, y] = true;
                    }
                }
                else
                {
                    // Rule 4: Any dead cell with exactly three live neighbors becomes a live cell (reproduction)
                    if (liveNeighborCount == 3)
                    {
                        NextGen[x, y] = true;
                    }
                    else
                    {
                        NextGen[x, y] = false;
                    }
                }
            }
        }

        
        for (int x = 0; x < CellRows; x++)
        {
            for (int y = 0; y < CellColumns; y++)
            {
                if (myCells[x, y] != null)
                {
                    myCells[x, y].SetActive(NextGen[x, y]);
                }
            }
        }
    }
}
