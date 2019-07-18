using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Library.History
{
    public class Version7Plus : IVersion
    {
        public void Test()
        {
            #region C# 7.1
            Console.WriteLine("Now you can create an async Main");

            Func<string, bool> whereClause = default(Func<string, bool>);
            //now it's possible to do
            Func<string, bool> whereClause2 = default;
            int? x = default; //and so on

            //in C# 7.0
            int count = 5;
            string label = "Colors used in the map";
            var pair = (count: count, label: label);
            //Now
            var pair2 = (count, label); // element names are "count" and "label", compiler knows.

            Console.WriteLine("There are two new compiler options that generate reference-only assemblies: /refout and /refonly.");
            #endregion
            #region C# 7.2
            // The method can be called in the normal way, by using positional arguments.
            PrintOrderDetails("Gift Shop", 31, "Red Mug");
            // Named arguments can be supplied for the parameters in any order.
            PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
            PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
            // Named arguments mixed with positional arguments are valid
            // as long as they are used in their correct position.
            PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");
            // C# 7.2 onwards
            PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");  //position counts
            PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");

            ReadOnlyStruct readOnlyStruct = new ReadOnlyStruct();
            RefStruct refStruct = new RefStruct();
            Set(readOnlyStruct, refStruct);

            //Finally, a new compound access modifier: private protected indicates that a member may be accessed by containing 
            //class or derived classes that are declared in the same assembly.While protected internal allows access by derived
            //classes or classes that are in the same assembly, private protected limits access to derived types declared in the same assembly.
            SampleForRef[] array = new SampleForRef[2] { new SampleForRef() { X = 1 }, new SampleForRef() { X = 2 } };
            SampleForRef[] otherArray = new SampleForRef[2] { new SampleForRef() { X = 4 }, new SampleForRef() { X = 3 } };
            ref SampleForRef r = ref (array != null ? ref array[0] : ref otherArray[0]);
            SampleForRef withoutRef = array[0];
            r.X = 40;
            Console.WriteLine($"{r.X} equals {withoutRef.X} equals {array[0].X}");
            int[] arrayInt = new int[2] { 1, 3 };
            int[] otherArrayInt = new int[2] { 1, 3 };
            ref int rInt = ref (arrayInt != null ? ref arrayInt[0] : ref otherArrayInt[0]);
            int intWithoutRef = arrayInt[0];
            rInt = 9;
            Console.WriteLine($"{rInt} equals {arrayInt[0]} but it's not equal {intWithoutRef}");
            #endregion
            #region C# 7.3

            #endregion
        }
        private protected class A { }
        internal protected class B { }
        public static void Set(ReadOnlyStruct readOnlyStruct, RefStruct refStruct)
        {
            Console.WriteLine("Calling Set");
        }
        static void PrintOrderDetails(string sellerName, int orderNum, string productName)
        {
            Console.WriteLine($"{sellerName}-{orderNum}-{productName}");
        }
    }
    public class ReferenceSemantics
    {
        private int X = 2;
        public ref readonly int Method(in string x)
        {
            //x = "Test";  //compiler error
            //x is not modifiable. The in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method.
            //The ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object.
            return ref this.X;
        }
    }
    public readonly struct ReadOnlyStruct
    {
        public string A { get; } //it's not possible to assing in readonly struct
        public void Method()
        {
            Console.WriteLine("Call Method");
        }
    }
    public ref struct RefStruct
    {
        public string A { get; }
    }
    public class SampleForRef
    {
        public int X { get; set; }
    }
}
