
namespace DSudoku;
public struct Coord {
    public int X { get; set; }
    public int Y { get; set; }

    public Coord(int x, int y)
        => (X, Y) = (x, y);
}
