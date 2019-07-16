using System;
using System.Collections.Generic;
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
    }
    public class AlreadyImplemented : IAlreadyImplemented
    {
        public void Make()
        {
            //this.IsAdded(); //I cannot call this method inside another method
            if (IAlreadyImplemented.Mordor || IAlreadyImplemented.PublicCheck())
                return;
        }
        public override bool base(IAlreadyImplemented).IsAdded()
        {
            return false;
        }
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
}
