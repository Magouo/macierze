namespace macierze
{
    class MatrixMultiplier
    {
        public static Matrix MultiplySequential(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.Rows, b.Cols);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < b.Cols; j++)
                    for (int k = 0; k < a.Cols; k++)
                        result.Data[i, j] += a.Data[i, k] * b.Data[k, j];
            return result;
        }

        public static Matrix MultiplyParallel(Matrix a, Matrix b, int maxThreads)
        {
            Matrix result = new Matrix(a.Rows, b.Cols);
            Parallel.For(0, a.Rows, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, i =>
            {
                for (int j = 0; j < b.Cols; j++)
                    for (int k = 0; k < a.Cols; k++)
                        result.Data[i, j] += a.Data[i, k] * b.Data[k, j];
            });
            return result;
        }

        public static Matrix MultiplyWithThreads(Matrix a, Matrix b,int maxThreads)
        {
            //int maxThreads = Environment.ProcessorCount;

            Matrix result = new Matrix(a.Rows, b.Cols);
            Thread[] threads = new Thread[maxThreads];

            int rowsPerThread = a.Rows / maxThreads;
            int remainingRows = a.Rows % maxThreads;

            for (int i = 0; i < maxThreads; i++)
            {
                int startRow = i * rowsPerThread;
                int endRow = (i + 1) * rowsPerThread;

                // ostatni watek bierze reszte
                if (i == maxThreads - 1)
                    endRow += remainingRows;

                threads[i] = new Thread(() =>
                {
                    for (int row = startRow; row < endRow; row++)
                    {
                        for (int j = 0; j < b.Cols; j++)
                        {
                            for (int k = 0; k < a.Cols; k++)
                            {
                                result.Data[row, j] += a.Data[row, k] * b.Data[k, j];
                            }
                        }
                    }
                });
                threads[i].Start();
            }

            foreach (Thread thread in threads)
                thread.Join();

            return result;
        }
    }

}
