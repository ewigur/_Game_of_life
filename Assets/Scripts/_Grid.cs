using UnityEngine;
public class _Grid : MonoBehaviour
{
    GameObject[,] myCells;

    [Header("Cell Attributes")]
    public int CellRows = 202;
    public int CellColumns = 126;
    public float spaceBetweenCells = 1.5f;

    [Header("Speed")]
    public int SpeedOfSimulation = 4;

    [Header("Patterns of Life")]
    public bool RandomPatterns = false;
    public bool DrawPatterns = false;

    private bool PauseGame = false;

    private Camera myCamera;

    public int generations;

    void Start()
    {
        myCamera = Camera.main;

        Application.targetFrameRate = 4; 
        GridCreation();
    }

    private void Update()
    {
        
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PauseGame = !PauseGame;
            }

            if (PauseGame)
            {
                DrawSpawn();
                return;
            }

            Application.targetFrameRate = SpeedOfSimulation;
            NeighborCheck();
            DrawSpawn();
        }
    }

    public void GridCreation()
    {

        myCells = new GameObject[CellRows, CellColumns];

        var texture = new Texture2D(32, 32);

        Vector2 ScreenBounds = new();

        ScreenBounds.x = myCamera.orthographicSize * myCamera.aspect * 2;
        ScreenBounds.y = myCamera.orthographicSize * 2;

        float CellSizeX = ScreenBounds.x / CellRows;
        float CellSizeY = ScreenBounds.y / CellColumns;

        float CellStartX = myCamera.transform.position.x - (ScreenBounds.x * 0.5f) + (CellSizeX * 0.5f);
        float CellStartY = myCamera.transform.position.y - (ScreenBounds.y * 0.5f) + (CellSizeY * 0.5f);


        for (int x = 0; x < CellRows; x++)
        {
            for (int y = 0; y < CellColumns; y++)
            {
                var cellObject = new GameObject();

                cellObject.transform.parent = transform;

                SpriteRenderer spriteRenderer = cellObject.AddComponent<SpriteRenderer>();

                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                spriteRenderer.color = Color.blue; //new Color(1, 0 , 1 , 1f);

                //spriteRenderer.color = new Color(1f, 1f, 1f, 1f);  // Set default color with full opacity

                float scaleX = CellSizeX / texture.width * spaceBetweenCells;
                float scaleY = CellSizeY / texture.height * spaceBetweenCells;

                cellObject.transform.localScale = new Vector2(scaleX, scaleY);

                myCells[x, y] = cellObject;

                float posX = CellStartX + (x * CellSizeX);
                float posY = CellStartY + (y * CellSizeY);

                cellObject.transform.position = new Vector3(posX, posY, 0);

                RandomSpawn(x, y);
         
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

                //---------//
                bool isAlive = myCells[x, y] != null && myCells[x, y].activeSelf;

                if (isAlive)
                {
                    generations++;

                    if (liveNeighborCount < 2 || liveNeighborCount > 3)
                    {                        
                        NextGen[x, y] = false; 
                    }

                    else
                    {
                        generations = 0;
                        NextGen[x, y] = true;                       
                    }

                    ColorLerp(myCells[x, y]);
                }

                else
                {
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

        //Buffer
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

    private void ColorLerp(GameObject Cells)
    {
        SpriteRenderer spriteRenderer = Cells.GetComponent<SpriteRenderer>();

        Color aliveColor = Color.cyan;
        Color dyingColor = Color.blue;

        int maxGens = 3;

        float cLerping = Mathf.Clamp01((float)generations / maxGens);

        spriteRenderer.color = Color.Lerp(aliveColor, dyingColor, cLerping);

    }

    private void RandomSpawn(int x, int y)
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

    private void DrawSpawn()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);

            for (int x = 0; x < CellRows; x++)
            {
                for (int y = 0; y < CellColumns; y++)
                {
                    Vector3 cellPosition = myCells[x, y].transform.position;

                    float cellSizeX = myCells[x, y].transform.localScale.x;
                    float cellSizeY = myCells[x, y].transform.localScale.y;
   
                    if (mousePosition.x > cellPosition.x - cellSizeX / 4 && mousePosition.x < cellPosition.x + cellSizeX / 4 &&
                        mousePosition.y > cellPosition.y - cellSizeY / 4 && mousePosition.y < cellPosition.y + cellSizeY / 4) 
                    {
                        if (!myCells[x, y].activeSelf)
                        {
                            myCells[x, y].SetActive(true);
                            SpriteRenderer spriteRenderer = myCells[x, y].GetComponent<SpriteRenderer>();
                            
                            spriteRenderer.color = new Color(1f, 0f, 1f, 0.4f);
                        }
                    }
                }
            }
        }
    }
}

