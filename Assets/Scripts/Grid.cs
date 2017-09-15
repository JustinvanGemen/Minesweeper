using UnityEngine;

public class Grid{
    private const int W = 16;
    private const int H = 16;
    public static GameObject[,] Elements = new GameObject[W, H];
  
    
    public static void UncoverMines() {
        foreach (var elem in Elements)
            if (elem.GetComponent<Element>().Mine)
                elem.GetComponent<Element>().LoadTexture(0);
    }
    
    public static bool MineAt(int x, int y) {
        if (x >= 0 && y >= 0 && x < W && y < H)
            Debug.Log(Elements[x, y].GetComponent<Element>().Mine);
            return Elements[x, y].GetComponent<Element>().Mine;
        return false;
    }

    public static int AdjacentMines(int x, int y) {
        var count = 0;

        if (MineAt(x,   y+1)) ++count; // top
        if (MineAt(x+1, y+1)) ++count; // top-right
        if (MineAt(x+1, y  )) ++count; // right
        if (MineAt(x+1, y-1)) ++count; // bottom-right
        if (MineAt(x,   y-1)) ++count; // bottom
        if (MineAt(x-1, y-1)) ++count; // bottom-left
        if (MineAt(x-1, y  )) ++count; // left
        if (MineAt(x-1, y+1)) ++count; // top-left

        return count;
    }
    
    public static void FFuncover(int x, int y, bool[,] visited) {
        if (x < 0 || y < 0 || x >= W || y >= H) return;
        if (visited[x, y])
            return;

        Elements[x, y].GetComponent<Element>().LoadTexture(AdjacentMines(x, y));

        if (AdjacentMines(x, y) > 0)
            return;

        visited[x, y] = true;

        FFuncover(x-1, y, visited);
        FFuncover(x+1, y, visited);
        FFuncover(x, y-1, visited);
        FFuncover(x, y+1, visited);
    }
    
    public static bool IsFinished() {
        foreach (var elem in Elements)
            if (elem.GetComponent<Element>().IsCovered() && !elem.GetComponent<Element>().Mine)
                return false;
        return true;
    }
}
