using System.Collections.Generic;
using UnityEngine;

public enum CellProcedures
{
    Instantiation,
    Interaction,
    Rules
}

public class CellModel
{
    private const int SCREEN_WIDTH = 64; // 1024 pixels
    private const int SCREEN_HEIGHT = 48; // 768 pixels

    private CellView CellObj
    {
        get
        {
            return Resources.Load("Prefab/Cell", typeof(CellView)) as CellView;
        }
    }

    System.Random random = new System.Random();

    private readonly CellView[,] _grid = new CellView[SCREEN_WIDTH, SCREEN_HEIGHT];
    private List<CellView> _cellViews = new List<CellView>(); // To be use for randomization

    private int _numNeighbors;

    public int aliveCount { get; private set; }

    private void GenerateCells(int x, int y)
    {
        CellView cell = Object.Instantiate(CellObj, new Vector2(x, y), Quaternion.identity);
        _grid[x, y] = cell;
        _grid[x, y].SetActivity(false);
        aliveCount = 0;

        _cellViews.Add(_grid[x, y]);
    }

    /// <summary>
    /// Every cell interacts with its eight neighbours, which are the cells that are horizontally,
    /// vertically, or diagonally adjacent.
    /// </summary>
    private void CountNeighbors(int x, int y)
    {
        _numNeighbors = 0;

         // North
        if (y + 1 < SCREEN_HEIGHT)
            CheckCellCoordinates(x, y + 1);

        // East
        if (x + 1 < SCREEN_WIDTH)
            CheckCellCoordinates(x + 1, y);

        // South
        if (y - 1 >= 0)
            CheckCellCoordinates(x, y - 1);

        // West
        if (x - 1 >= 0)
            CheckCellCoordinates(x - 1, y);

        // NorthEast
        if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
            CheckCellCoordinates(x + 1, y + 1);

        // NorthWest
        if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
            CheckCellCoordinates(x - 1, y + 1);

        // SouthEast
        if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
            CheckCellCoordinates(x + 1, y - 1);

        // SouthWest
        if (x - 1 >= 0 && y - 1 >= 0)
            CheckCellCoordinates(x - 1, y - 1);

        _grid[x, y].numNeighbors = _numNeighbors;
    }

    private void CheckCellCoordinates(int x, int y)
    {
        if (_grid[x, y].isActive)
            _numNeighbors++;
    }

    private void PopulationControl(int x, int y)
    {
        // Rules
        // Any live cell with fewer than two live neighbours dies, as if by underpopulation
        // Any live cell with two or three neighbours lives on to the next generation.
        // Any live cell with more than three live neighbours dies, as if by overpopulation.
        // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        if (_grid[x, y].isActive)
        {
            if (_grid[x, y].numNeighbors != 2 && _grid[x, y].numNeighbors != 3)
            {
                _grid[x, y].SetActivity(false);
                aliveCount--;
            }
        }
        else
        {
            if (_grid[x, y].numNeighbors == 3)
            {
                _grid[x, y].SetActivity(true);
                aliveCount++;
            }
        }
    }

    private bool RandomAliveCell()
    {
        return random.Next(2) == 1;
    }

    /// <summary>
    /// To create cell pattern by clicking in the grid
    /// </summary>
    public void ClickCellInBoundary()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int x = Mathf.RoundToInt(mousePoint.x);
        int y = Mathf.RoundToInt(mousePoint.y);

        if (x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
        {
            // mouse point is in bounds
            bool isActive = !_grid[x, y].isActive;
            _grid[x, y].SetActivity(isActive);

            if (isActive)
                aliveCount++;
        }
    }

    public void RandomCellPattern()
    {
        aliveCount = 0;

        foreach (CellView cellView in _cellViews)
        {
            bool isActive = RandomAliveCell();
            cellView.SetActivity(isActive);

            if (isActive)
                aliveCount++;
        }
    }

    public void RestartGame()
    {
        foreach (CellView cellView in _cellViews)
        {
            cellView.SetActivity(false);
            aliveCount = 0;
        }
    }

    public void ExecuteCellProcedure(CellProcedures gridProcedure)
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                switch (gridProcedure)
                {
                    case CellProcedures.Instantiation:
                        GenerateCells(x, y);
                        break;
                    case CellProcedures.Interaction:
                        CountNeighbors(x, y);
                        break;
                    case CellProcedures.Rules:
                        PopulationControl(x, y);
                        break;
                }
            }
        }
    }
}
