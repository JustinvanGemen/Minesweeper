public class Grid{
    private static int W = 16; 
    private static int H = 16; 
    public static Element[,] Elements = new Element[W, H];

    public static void UncoverMines() {
        foreach (Element elem in Elements)
            if (elem.Mine)
                elem.LoadTexture(0);
    }
    
    public static bool MineAt(int x, int y) {
        if (x >= 0 && y >= 0 && x < W && y < H)
            return Elements[x, y].Mine;
        return false;
    }

    public static int AdjacentMines(int x, int y) {
        int count = 0;

        if (MineAt(x,   y+1)) ++count; // top
        if (MineAt(x+1, y+1)) ++count; // top-right
        if (MineAt(x+1, y  )) ++count; // right
        if (MineAt(x+1, y-1)) ++count; // bottom-right
        if (MineAt(x,   y-1)) ++count; // bottom
        if (MineAt(x-1, y  )) ++count; // left
        if (MineAt(x-1, y+1)) ++count; // top-left

        return count;
    }
    
    public static void FFuncover(int x, int y, bool[,] visited) {
        if (x >= 0 && y >= 0 && x < W && y < H) {
            if (visited[x, y])
                return;

            Elements[x, y].LoadTexture(AdjacentMines(x, y));

            if (AdjacentMines(x, y) > 0)
                return;

            visited[x, y] = true;

            FFuncover(x-1, y, visited);
            FFuncover(x+1, y, visited);
            FFuncover(x, y-1, visited);
            FFuncover(x, y+1, visited);
        }
    }
    
    public static bool IsFinished() {
        foreach (Element elem in Elements)
            if (elem.IsCovered() && !elem.Mine)
                return false;
        return true;
    }
}




/*using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private readonly List<List<GameObject>> _elementList = new List<List<GameObject>>();
    [SerializeField] private GameObject _elementPrefab;
    private GameObject _element;

    private int _x;
    private int _y;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (var x = 0; x < 16; x++)
        {
            _elementList.Add(new List<GameObject>());
            for (var y = 0; y < 16; y++)
            {
                _elementList[x].Add(_elementPrefab);
                _element = Instantiate(_elementPrefab, transform);
                _element.transform.position = new Vector2(_x,_y);
                _elementList[_x][_y] = _element;
                _y++;
            }
            _y = 0;
            _x++;
        }
        Debug.Log(_elementList);
    }
    
}*/
