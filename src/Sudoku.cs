using System.Text;
using System.Text.Json.Serialization;

namespace DSudoku;
public class Sudoku {
    private readonly int[,] _data;
    private readonly int _size;
    private readonly int _boxSize;
    private readonly int _maxValue;

    public Sudoku(int[,] data, int maxValue) {
        _data = data;
        _maxValue = maxValue;
        _size = data.GetLength(0);
        _boxSize = (int)Math.Sqrt(_size);
    }

    private Coord? FindEmpty() {
        for (int i = 0; i < _size; ++i) { 
            for (int j = 0; j < _size; ++j) { 
                if (_data[i, j] == 0) return new(i, j);
            }
        }

        return null;
    }

    private bool IsValid(int value, Coord coord) {
        for (int i = 0; i < _size; ++i) {
            if (_data[coord.X, i] == value || _data[i, coord.Y] == value) {
                return false;
            }
        }

        var boxStart = new Coord(coord.X / _boxSize * _boxSize, coord.Y / _boxSize * _boxSize); // note: int floors the number
        var boxEnd = new Coord(boxStart.X + _boxSize, boxStart.Y + _boxSize);

        for (int i = boxStart.X; i < boxEnd.X; ++i) {
            for (int j = boxStart.Y; j < boxEnd.Y; ++j) {
                if (_data[i, j] == value) {
                    return false;
                }   
            }
        }

        return true;
    }
    
    public bool Solve() {
        if (FindEmpty() is Coord coord) {
            for (int i = 1; i < _maxValue + 1; ++i) {
                if (IsValid(i, coord)) {
                    _data[coord.X, coord.Y] = i;

                    if (Solve()) return true;

                    _data[coord.X, coord.Y] = 0;
                }
            }
        } else return true;

        return false;
    }

    public static Sudoku FromFile(string path) {
        var lines = File.ReadAllLines(path);
        int[,] data = new int[lines.Length, lines.Length]; // rows count = columns count

        for (int i = 0; i < lines.Length; ++i) {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var splitted = lines[i].Split(' ');
            if (splitted.Length != lines.Length) {
                throw new Exception("Invalid file content");
            }

            for (int j = 0; j < splitted.Length; ++j) {
                data[i, j] = int.Parse(splitted[j]);
            }
        }

        return new Sudoku(data, data.GetLength(0));
    }

    public override string ToString() {
        var builder = new StringBuilder();

        for (int i = 0; i < _size; ++i) {
            for (int j = 0; j < _size; ++j) {
                builder.Append($"{_data[i, j]} ");
            }
            builder.AppendLine();
        }

        return builder.ToString();
    }
}
