using UnityEngine;

public class Grid
{
    private static bool[,] _visited;
    private const int W = 16;
    private const int H = 16;
    public static GameObject[,] Elements = new GameObject[W, H];
  
    
    public static void ShowMines() {
        foreach (var elem in Elements)
            if (elem.GetComponent<Element>().Mine)
                elem.GetComponent<Element>().UpdateSprite(0);
    }
    
    public static bool MineAt(int x, int y) {
        if (x >= 0 && y >= 0 && x < W && y < H)
        {
            return Elements[x, y].GetComponent<Element>().Mine;
        }
        return false;
    }

    public static int MinesNearby(int x, int y) {
        var count = 0;

        if (MineAt(x,   y+1)) ++count;
        if (MineAt(x+1, y+1)) ++count;
        if (MineAt(x+1, y  )) ++count;
        if (MineAt(x+1, y-1)) ++count;
        if (MineAt(x,   y-1)) ++count;
        if (MineAt(x-1, y-1)) ++count;
        if (MineAt(x-1, y  )) ++count;
        if (MineAt(x-1, y+1)) ++count;

        return count;
    }
    //Flood Fill Uncover inside joke :)
    public static void FunKoffer(int x, int y) {
        _visited = new bool[W,H];
        if (x < 0 || y < 0 || x >= W || y >= H) return;
        if (_visited[x, y])
            return;
        
        Elements[x, y].GetComponent<Element>().UpdateSprite(MinesNearby(x, y));

        if (MinesNearby(x, y) > 0)
            return;
        

        _visited[x, y] = true;

        FunKoffer(x-1, y);
        FunKoffer(x+1, y);
        FunKoffer(x,   y-1);
        FunKoffer(x,   y+1);
        FunKoffer(x-1, y-1);
        FunKoffer(x+1, y+1);
        FunKoffer(x-1, y+1);
        FunKoffer(x+1, y-1);
    }
    
    public static bool IsFinished() {
        foreach (var elem in Elements)
            if (elem.GetComponent<Element>().IsCovered() && !elem.GetComponent<Element>().Mine)
                return false;
        return true;
    }
}
