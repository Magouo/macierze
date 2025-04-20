using macierze;
using System.Diagnostics;

class Program
{

    // tabele i wnioski w readme 
    static void Main(string[] args)
    {
        int size = 300;
        int maxThreads = 18;
        double czasSekwencyjny = 0;
        double czasPararel = 0;
        double czasThread = 0;

        Matrix a = new Matrix(size, size);
        Matrix b = new Matrix(size, size);
        a.FillRandom();
        b.FillRandom();

        for (int j = 1; j<18; j++)
        { 
        for (int i = 0; i < 30; i++)
        {
           var stopwatch = Stopwatch.StartNew();
            Matrix resultSeq = MatrixMultiplier.MultiplySequential(a, b);
            stopwatch.Stop();
            double czasSek = stopwatch.ElapsedMilliseconds;
            czasSekwencyjny += czasSek;

            stopwatch.Restart();
            Matrix resultPar = MatrixMultiplier.MultiplyParallel(a, b, j);
            stopwatch.Stop();
            double czasPar = stopwatch.ElapsedMilliseconds;
            czasPararel += czasPar;

            stopwatch.Restart();
            Matrix resultThread = MatrixMultiplier.MultiplyWithThreads(a, b, j);
            stopwatch.Stop();
            czasThread += stopwatch.ElapsedMilliseconds;
        }

        double speedup = czasSekwencyjny / czasPararel;
        Console.WriteLine($"Thread: {j}");
        Console.WriteLine($"Przyspieszenie: {speedup:F2}");
        Console.WriteLine($"Czas sekwencyjny: {czasSekwencyjny:F2}");
        Console.WriteLine($"Czas pararel : {czasPararel:F2}");
        Console.WriteLine($"Czas mnożenia watkami: {czasThread:F2}");
        }
    }
}
