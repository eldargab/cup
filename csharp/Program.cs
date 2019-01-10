using System.Collections.Generic;
using System.Linq;
using Lib;


class Program : App {
    protected override void Run() {
        Out.WriteLine("Hello world!");
    }


    static void Main(string[] args) {
        Exec<Program>(args);
    }
}
