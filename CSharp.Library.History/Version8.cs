using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CSharp.Library.History
{
    public class Version8 : IVersion
    {
        private string[] Words = new string[]
        {
                        // index from start    index from end
            "The",      // 0                   ^9
            "quick",    // 1                   ^8
            "brown",    // 2                   ^7
            "fox",      // 3                   ^6
            "jumped",   // 4                   ^5
            "over",     // 5                   ^4
            "the",      // 6                   ^3
            "lazy",     // 7                   ^2
            "dog"       // 8                   ^1
        };              // 9 (or words.Length) ^0
        public void Test()
        {
            IAlreadyImplemented already = new AlreadyImplemented();
            if (already.IsAdded())
                Console.WriteLine("Already implemented!!!");
            //Using of pattern
            PointV8 pointV8 = new PointV8() { X = 1, Y = 2, Rainbow = Rainbow.Blue };
            Console.WriteLine($"Multiply: {Multiply(pointV8)}");
            Console.WriteLine($"Quadrant: {GetQuadrant(pointV8)}");
            Console.WriteLine($"Rainbow: {FromRainbow(pointV8.Rainbow)}");
            Console.WriteLine($"Rock vs Paper wins: {RockPaperScissors(Rps.Rock, Rps.Paper)}");
            Console.WriteLine($"Ï want to take a taxi and toll is: {CalculateToll(new Taxi())}");
            //It's disposed when it's out of scope.
            using StreamWriter streamWriter = new StreamWriter(new MemoryStream());
            streamWriter.Write("Close this stream writer at the end of this method");
            //If I add to a ref struct the Dispose() method, i can use using statement to dispose it at the end of scope
            using DisposableStruct disposableStruct = new DisposableStruct();
            bool isWhite = false;
            Console.WriteLine("It's not white: " + IsNotWhite(isWhite));
            //static local function
            static bool IsNotWhite(bool isWhite) => !isWhite;
            Console.WriteLine($"{Words[0]} is equal to {Words[^9]}");
            //Indices and ranges
            string[] firstFourWords = Words[0..4]; // or [..4]
            string[] firstFourWords2 = Words[^9..^5];
            Console.WriteLine($"First four words: {string.Join(" ", firstFourWords)}");
            Console.WriteLine($"First four words: {string.Join(" ", firstFourWords2)}");
            string[] lastPhrase = Words[6..];
            string[] lastPhrase2 = Words[^3..];
            Console.WriteLine($"Last phrase: {string.Join(" ", lastPhrase)}");
            Console.WriteLine($"Last phrase: {string.Join(" ", lastPhrase2)}");
            Range range = ..4;
            Console.WriteLine($"First phrase: {string.Join(" ", Words[range])}");
            //Async Stream
            Console.WriteLine($"Total from async stream is: {ConsumeAsyncStream().ConfigureAwait(false).GetAwaiter().GetResult()}");
            //Nullable context
            NullableClass nullableClass = new NullableClass();
            nullableClass.Caring = "Saturn";
            int nullableCount = nullableClass.Count.Value;
            Console.WriteLine($"Nullable string is length: {nullableCount}");
        }
        //Method that uses the async stream
        static async Task<int> ConsumeAsyncStream()
        {
            int total = 0;
            await foreach (int x in new AsynchronousStream().Fetch())
                total += x;
            return total;
        }
        //Property Pattern
        public static double Multiply(PointV8 pointV8)
            => pointV8 switch
            {
                { Rainbow: Rainbow.Blue } => pointV8.X * pointV8.Y * 0.5,
                { Rainbow: Rainbow.Green } => pointV8.X * pointV8.Y * 0.7,
                _ => pointV8.X * pointV8.Y
            };
        //Positional Pattern
        static Quadrant GetQuadrant(PointV8 point) => point switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            //var (_, _) => Quadrant.OnBorder, //you can choose this one or the next one for default value
            _ => Quadrant.Unknown
        };
        //This is a switch expression, similar to switch statement
        public static Color FromRainbow(Rainbow colorBand)
        => colorBand switch
        {
            Rainbow.Red => Color.FromArgb(0xFF, 0x00, 0x00),
            Rainbow.Orange => Color.FromArgb(0xFF, 0x7F, 0x00),
            Rainbow.Yellow => Color.FromArgb(0xFF, 0xFF, 0x00),
            Rainbow.Green => Color.FromArgb(0x00, 0xFF, 0x00),
            Rainbow.Blue => Color.FromArgb(0x00, 0x00, 0xFF),
            Rainbow.Indigo => Color.FromArgb(0x4B, 0x00, 0x82),
            Rainbow.Violet => Color.FromArgb(0x94, 0x00, 0xD3),
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
        };
        //This is the same version with the switch statement
        public static Color FromRainbowClassic(Rainbow colorBand)
        {
            switch (colorBand)
            {
                case Rainbow.Red:
                    return Color.FromArgb(0xFF, 0x00, 0x00);
                case Rainbow.Orange:
                    return Color.FromArgb(0xFF, 0x7F, 0x00);
                case Rainbow.Yellow:
                    return Color.FromArgb(0xFF, 0xFF, 0x00);
                case Rainbow.Green:
                    return Color.FromArgb(0x00, 0xFF, 0x00);
                case Rainbow.Blue:
                    return Color.FromArgb(0x00, 0x00, 0xFF);
                case Rainbow.Indigo:
                    return Color.FromArgb(0x4B, 0x00, 0x82);
                case Rainbow.Violet:
                    return Color.FromArgb(0x94, 0x00, 0xD3);
                default:
                    throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand));
            };
        }
        //Tuple Patterns
        public static string RockPaperScissors(Rps first, Rps second)
            => (first, second) switch
            {
                (Rps.Rock, Rps.Paper) => "rock is covered by paper. Paper wins.",
                (Rps.Rock, Rps.Scissors) => "rock breaks scissors. Rock wins.",
                (Rps.Paper, Rps.Rock) => "paper covers rock. Paper wins.",
                (Rps.Paper, Rps.Scissors) => "paper is cut by scissors. Scissors wins.",
                (Rps.Scissors, Rps.Rock) => "scissors is broken by rock. Rock wins.",
                (Rps.Scissors, Rps.Paper) => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };
        //Deep in pattern
        public decimal CalculateToll(object vehicle) =>
            vehicle switch
            {
                Car { Passengers: 0 } => 2.00m + 0.50m,
                Car { Passengers: 1 } => 2.0m,
                Car { Passengers: 2 } => 2.0m - 0.50m,
                Car c => 2.00m - 1.0m,

                Taxi { Fares: 0 } => 3.50m + 1.00m,
                Taxi { Fares: 1 } => 3.50m,
                Taxi { Fares: 2 } => 3.50m - 0.50m,
                Taxi t => 3.50m - 1.00m,

                Bus b when ((double)b.Riders / 10) < 0.50 => 5.00m + 2.00m,
                Bus b when ((double)b.Riders / 10) > 0.90 => 5.00m - 1.00m,
                Bus b => 5.00m,
                { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
                null => throw new ArgumentNullException(nameof(vehicle))
            };
    }
    public class AlreadyImplemented : IAlreadyImplemented
    {
        public IAlreadyImplemented Me => this;
        public void Make()
        {
            //this.IsAdded(); //I cannot call this method inside another method, but I can cast this to IAlreadyImplemented to see that method
            //((IAlreadyImplemented)this).IsAdded();
            if (IAlreadyImplemented.Mordor || IAlreadyImplemented.PublicCheck() || Me.IsAdded())
                return;
        }
        //public override bool base(IAlreadyImplemented).IsAdded()
        //{
        //    return false;
        //}
    }
    public interface IAlreadyImplemented
    {
        void Make();
        //It's not possible to auto-implement property in interface
        //public bool FirstCheck { get; set; } = false;
        public bool IsAdded()
        {
            return PublicCheck() || Check();
        }
        public static bool Mordor { get; set; }
        private static bool Check()
        {
            return Mordor;
        }
        private static bool Asgard = false;
        public static bool PublicCheck()
        {
            return Asgard;
        }
    }
    public struct PointV8
    {
        public Rainbow Rainbow { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public readonly double Distance => Math.Sqrt(X * X + Y * Y);
        public PointV8(double x, double y, Rainbow rainbow) => (X, Y, Rainbow) = (x, y, rainbow);
        //The readonly in struct if properties or methods don't change, despite they are in get or method 
        //this feature lets you specify your design intent so the compiler can enforce it, and make optimizations based on that intent.
        public readonly override string ToString() =>
            $"({X}, {Y}) is {Distance} from the origin";
        //Deconstruct created in c# 7+
        public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
    }
    public ref struct DisposableStruct
    {
        public void Dispose()
        {
            Console.WriteLine("Disposable Ref Struct disposed");
        }
    }
    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }
    public enum Quadrant
    {
        Unknown,
        Origin,
        One,
        Two,
        Three,
        Four,
        OnBorder
    }
    public enum Rps
    {
        Rock,
        Paper,
        Scissors
    }
    public class Car
    {
        public int Passengers { get; set; }
    }
    public class Taxi
    {
        public int Fares { get; set; }
    }
    public class Bus
    {
        public int Riders { get; set; }
    }
    public class AsynchronousStream
    {
        //To understand further https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/generate-consume-asynchronous-stream
        public async IAsyncEnumerable<int> Fetch()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100); //Simulation of a stream retrieving
                yield return i;
            }
        }
    }
    public class NullableClass
    {
#nullable safeonly
        public string? Caring;
#nullable restore
        public int? Count => this.Caring!.Length;
    }
}
