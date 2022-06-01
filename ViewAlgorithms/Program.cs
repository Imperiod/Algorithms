using Algorithms;
using Algorithms.FizzBuzz.Implementations;
using Algorithms.FizzBuzz.Interfaces;
using System.Diagnostics;
using System.Numerics;
using System.Text;

Console.WriteLine("Algorithms");

Task testNodes = Task.Run(() =>
{
    StringBuilder stringBuilder = new StringBuilder();

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    #region Points
    List<NodePoint> nodePoints = new List<NodePoint>();

    //Start point 0 with his children's A
    NodePoint nodePoint = new NodePoint();
    nodePoint.Points.Add((0, 0));
    nodePoint.Points.Add((1, 3.29));
    //nodePoint.Points.Add((2, 8.04));
    nodePoint.Points.Add((2, 0));

    nodePoints.Add(nodePoint);

    //1 B
    nodePoint = new NodePoint();
    nodePoint.Points.Add((0, 3.29));
    nodePoint.Points.Add((3, 3.21));

    nodePoints.Add(nodePoint);

    //2 M
    nodePoint = new NodePoint();
    nodePoint.Points.Add((0, 8.04));
    //nodePoint.Points.Add((4, 7.36));
    nodePoint.Points.Add((4, 0));

    nodePoints.Add(nodePoint);

    //3 C
    nodePoint = new NodePoint();
    nodePoint.Points.Add((1, 3.21));
    nodePoint.Points.Add((5, 12.24));
    nodePoint.Points.Add((6, 4.54));

    nodePoints.Add(nodePoint);

    //4 N
    nodePoint = new NodePoint();
    nodePoint.Points.Add((2, 7.36));
    //nodePoint.Points.Add((7, 9.69));
    nodePoint.Points.Add((7, 0));

    nodePoints.Add(nodePoint);

    //5 D
    nodePoint = new NodePoint();
    nodePoint.Points.Add((3, 12.24));
    nodePoint.Points.Add((8, 9.31));
    nodePoint.Points.Add((9, 6.66));
    nodePoint.Points.Add((10, 5.54));

    nodePoints.Add(nodePoint);

    //6 K
    nodePoint = new NodePoint();
    nodePoint.Points.Add((3, 4.54));
    nodePoint.Points.Add((10, 16.42));
    nodePoint.Points.Add((11, 10.78));
    nodePoint.Points.Add((12, 15.16));

    nodePoints.Add(nodePoint);

    //7 O
    nodePoint = new NodePoint();
    nodePoint.Points.Add((4, 9.69));
    //nodePoint.Points.Add((13, 7.89));
    nodePoint.Points.Add((13, 0));

    nodePoints.Add(nodePoint);

    //8 E
    nodePoint = new NodePoint();
    nodePoint.Points.Add((5, 9.31));
    nodePoint.Points.Add((14, 7.36));

    nodePoints.Add(nodePoint);

    //9 G
    nodePoint = new NodePoint();
    nodePoint.Points.Add((5, 6.66));
    nodePoint.Points.Add((11, 5.03));
    nodePoint.Points.Add((14, 7.03));
    nodePoint.Points.Add((15, 16.53));

    nodePoints.Add(nodePoint);

    //10 J
    nodePoint = new NodePoint();
    nodePoint.Points.Add((5, 5.54));
    nodePoint.Points.Add((6, 16.42));
    nodePoint.Points.Add((11, 8));
    nodePoint.Points.Add((15, 16.2));

    nodePoints.Add(nodePoint);

    //11 I
    nodePoint = new NodePoint();
    nodePoint.Points.Add((6, 10.78));
    nodePoint.Points.Add((9, 5.03));
    nodePoint.Points.Add((10, 8));
    nodePoint.Points.Add((15, 12.99));

    nodePoints.Add(nodePoint);

    //12 L
    nodePoint = new NodePoint();
    nodePoint.Points.Add((6, 15.16));
    nodePoint.Points.Add((13, 4.46));

    nodePoints.Add(nodePoint);

    //13 P
    nodePoint = new NodePoint();
    nodePoint.Points.Add((7, 7.89));
    nodePoint.Points.Add((12, 4.46));

    //nodePoint.Points.Add((16, 7.02));
    nodePoint.Points.Add((16, 0));

    nodePoint.Points.Add((17, 4.58));

    nodePoints.Add(nodePoint);

    //14 F
    nodePoint = new NodePoint();
    nodePoint.Points.Add((8, 7.36));
    nodePoint.Points.Add((9, 7.03));

    nodePoints.Add(nodePoint);

    //15 H
    nodePoint = new NodePoint();
    nodePoint.Points.Add((9, 16.53));
    nodePoint.Points.Add((10, 16.2));
    nodePoint.Points.Add((11, 12.99));
    nodePoint.Points.Add((20, 2.71));
    //nodePoint.Points.Add((21, 11.03));
    nodePoint.Points.Add((21, 0));

    nodePoints.Add(nodePoint);

    //16 Q
    nodePoint = new NodePoint();
    nodePoint.Points.Add((13, 7.02));
    //nodePoint.Points.Add((18, 16.83));
    nodePoint.Points.Add((18, 0));

    nodePoints.Add(nodePoint);

    //17 T
    nodePoint = new NodePoint();
    nodePoint.Points.Add((13, 4.58));
    nodePoint.Points.Add((19, 5.09));

    nodePoints.Add(nodePoint);

    //18 R
    nodePoint = new NodePoint();
    nodePoint.Points.Add((16, 16.83));
    //nodePoint.Points.Add((20, 15.03));
    nodePoint.Points.Add((20, 0));

    nodePoints.Add(nodePoint);

    //19 W
    nodePoint = new NodePoint();
    nodePoint.Points.Add((17, 5.09));
    nodePoint.Points.Add((21, 6.4));

    nodePoints.Add(nodePoint);

    //20 S
    nodePoint = new NodePoint();
    //nodePoint.Points.Add((15, 2.71));
    nodePoint.Points.Add((15, 0));
    nodePoint.Points.Add((18, 15.03));

    nodePoints.Add(nodePoint);

    //21 V
    nodePoint = new NodePoint();
    nodePoint.Points.Add((15, 11.03));
    nodePoint.Points.Add((19, 6.4));


    nodePoints.Add(nodePoint);
    #endregion

    Node node = new Node(nodePoints);

    node = node.Rebuild(node.GetShortWayToNode(15, node));

    stopwatch.Stop();

    stringBuilder.Append("Test nodes:\n\n");

    stringBuilder.Append($"Spend: {stopwatch.ElapsedMilliseconds} ms\n");
    stringBuilder.Append($"Final sequence is: {node.ToString(node)}\n");

    stringBuilder.Append("\nTest nodes over.\n");

    Console.WriteLine("==========================================================");
    Console.WriteLine(stringBuilder);
    Console.WriteLine("==========================================================");
});

Task testFxs = Task.Run(() =>
{
    StringBuilder stringBuilder = new StringBuilder();

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    List<ulong> n = new List<ulong>();
    n.Add(2166);
    n.Add(6099);
    n.Add(798);
    n.Add(1311);

    ulong gcd = Fx.GCD(n);

    stopwatch.Stop();

    stringBuilder.Append("Test Fx's:\n\n");

    stringBuilder.Append($"GCD [{string.Join(" : ", n)}] is: {gcd}\n");
    stringBuilder.Append($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n");

    stringBuilder.Append("Test Fx's over.\n");

    Console.WriteLine("==========================================================");
    Console.WriteLine(stringBuilder.ToString());
    Console.WriteLine("==========================================================");
});

Task testSort = Task.Run(() =>
{
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    Random random = new Random();

    List<int> list = new List<int>();

    for (int i = 0; i < 1000; i++)
    {
        list.Add(random.Next(0, 10_000_000));
    }

    SortNode<int> sort = new SortNode<int>(list.ToArray());
    stopwatch.Stop();

    Stopwatch stopwatchAnother = new Stopwatch();
    stopwatchAnother.Start();
    list.Sort();
    stopwatchAnother.Stop();

    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.Append("Test sort:\n\n");
    stringBuilder.Append($"Elapsed time is: {stopwatch.ElapsedMilliseconds} ms\n");
    stringBuilder.Append($"Elements amount before was: {list.Count}\n");
    stringBuilder.Append($"Elements amount after is: {sort.ToList().Count}\n");
    stringBuilder.Append($"Another spend is: {stopwatchAnother.ElapsedMilliseconds} ms");

    Console.WriteLine("==========================================================");
    Console.WriteLine(stringBuilder);
    Console.WriteLine("==========================================================");

});

Task taskTestFizzBuzz = Task.Run(() =>
{
    IFizzBuzzRulesCompositer compositer = new FizzBuzzRulesCompositer();
    IFizzBuzz fizzBuzz = new FizzBuzz();
    StringBuilder stringBuilder = new StringBuilder();

    var list = fizzBuzz.GenList(100, compositer);

    stringBuilder.Append("Test FizzBuzz:\n\n");

    list.ForEach(f => stringBuilder.Append(f + "\n"));

    stringBuilder.Append("Test FizzBuzz over.");

    Console.WriteLine("==========================================================");
    Console.WriteLine(stringBuilder);
    Console.WriteLine("==========================================================");

});

Thread threadTestFibonacci = new Thread(() =>
{
    StringBuilder stringBuilder = new StringBuilder();

    //string? inputString = null;
    string? inputString = "10000";
    //BigInteger number = default;
    BigInteger number = BigInteger.Parse(inputString);

    //Console.WriteLine("Enter number for eval fibonacci:");

    while (inputString is null)
    {
        inputString = Console.ReadLine();
        if (inputString is null)
        {
            Console.WriteLine("String cannot be null. Try again.");
        }
        else
        {
            if (BigInteger.TryParse(inputString, out number) == false)
            {
                Console.WriteLine("String must be as integer number. Try again.");
                inputString = null;
            }
            else if (number < 0)
            {
                Console.WriteLine("String must be as positive integer number. Try again.");
                inputString = null;
            }
        }
    }

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    BigInteger fibonacci = Fibonacci.Get(number).Result;

    stopwatch.Stop();

    stringBuilder.Append("Test Fibonacci:\n\n");

    stringBuilder.Append($"Fibonacci [{inputString}] is: {fibonacci:N0}\n");
    stringBuilder.Append($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n");

    stringBuilder.Append("Test Fibonacci over.\n");

    Console.WriteLine("==========================================================");
    Console.WriteLine(stringBuilder.ToString());
    Console.WriteLine("==========================================================");
}, int.MaxValue);

Console.WriteLine("Start");

Task.WaitAll(testNodes, testFxs, testSort, taskTestFizzBuzz);

threadTestFibonacci.Start();

while (threadTestFibonacci.ThreadState == System.Threading.ThreadState.Running)
{
    Thread.Sleep(100);
}

Console.WriteLine("End");
Console.ReadKey();