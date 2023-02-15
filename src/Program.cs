using DSudoku;

try {
    var sudoku = Sudoku.FromFile("res/sample1.txt");

    if (sudoku.Solve()) {
        Console.WriteLine(sudoku);
    } else {
        Console.WriteLine("Could not solve");
    }
} catch(Exception e) {
    Console.WriteLine(e.Message);
}