using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CSharp.Library.History
{
    public class Version8 : IVersion
    {
        public void Test()
        {
            IAlreadyImplemented already = new AlreadyImplemented();
            if (already.IsAdded())
                Console.WriteLine("Already implemented!!!");
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
        public static string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
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
}
