﻿using System;
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
        public static Color FromRainbow(Rainbow colorBand) =>
    colorBand switch
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
    }
    public class AlreadyImplemented : IAlreadyImplemented
    {
        public void Make()
        {
            //this.IsAdded(); //I cannot call this method inside another method
            if (IAlreadyImplemented.Mordor || IAlreadyImplemented.PublicCheck())
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
        public double X { get; set; }
        public double Y { get; set; }
        public double Distance => Math.Sqrt(X * X + Y * Y);
        public readonly override string ToString() =>
            $"({X}, {Y}) is {Distance} from the origin";
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
}
