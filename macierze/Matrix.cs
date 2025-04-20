namespace macierze
{
    class Matrix
    {
        public int Rows { get; }
        public int Cols { get; }
        public double[,] Data { get; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Data = new double[rows, cols];
        }

        public void FillRandom()
        {
            Random rand = new Random();
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    Data[i, j] = rand.NextDouble() * 100;
        }

        public void Print(int maxRows = 10, int maxCols = 10)
        {
            for (int i = 0; i < Math.Min(Rows, maxRows); i++)
            {
                for (int j = 0; j < Math.Min(Cols, maxCols); j++)
                    Console.Write($"{Data[i, j]:F2}\t");
                Console.WriteLine();
            }
        }
    }

}
