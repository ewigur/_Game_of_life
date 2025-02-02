using UnityEngine;
public class _Grid : MonoBehaviour
{
    GameObject[,] myCells;
    Camera myCamera;

    int cellRows = 162;
    int cellColumns = 100;
    int spaceBetweenCells = 75;
    int generations;
    
    bool PauseGame = false;

    void Start()
    {
        myCamera = Camera.main;

        Application.targetFrameRate = 4; 
        GridCreation();
    }
    public void GridCreation()
    {
        myCells = new GameObject[cellRows, cellColumns];

        Vector2 ScreenBounds = new();

        var texture = new Texture2D(32, 32);

        ScreenBounds.x = myCamera.orthographicSize * myCamera.aspect * 2;
        ScreenBounds.y = myCamera.orthographicSize * 2;

        float CellSizeX = ScreenBounds.x / cellRows;
        float CellSizeY = ScreenBounds.y / cellColumns;

        float CellStartX = myCamera.transform.position.x - (ScreenBounds.x * 0.5f) + (CellSizeX * 0.5f);
        float CellStartY = myCamera.transform.position.y - (ScreenBounds.y * 0.5f) + (CellSizeY * 0.5f);


        for (int x = 0; x < cellRows; x++)
        {
            for (int y = 0; y < cellColumns; y++)
            {
                var cellObject = new GameObject();

                cellObject.transform.parent = transform;

                SpriteRenderer spriteRenderer = cellObject.AddComponent<SpriteRenderer>();

                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                spriteRenderer.color = Color.blue;

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
           
            NeighborCheck();
            DrawSpawn();
        }
    }
    private void DrawSpawn()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);

            for (int x = 0; x < cellRows; x++)
            {
                for (int y = 0; y < cellColumns; y++)
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
    private void NeighborCheck()
    {
        bool[,] NextGen = new bool[cellRows, cellColumns];

        for (int x = 0; x < cellRows; x++)
        {
            for (int y = 0; y < cellColumns; y++)
            {
                int liveNeighborCount = 0;
                int[] CheckX = { -1, 0, 1, 0, -1, 1, -1, 1 };
                int[] CheckY = { 0, 1, 0, -1, -1, -1, 1, 1 };

                for (int i = 0; i < CheckX.Length; i++)
                {
                    int neighborX = x + CheckX[i];
                    int neighborY = y + CheckY[i];

                    if (neighborX >= 0 && neighborX < cellRows && neighborY >= 0 && neighborY < cellColumns)
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

        for (int x = 0; x < cellRows; x++)
        {
            for (int y = 0; y < cellColumns; y++)
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
}

