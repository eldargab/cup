using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

class Program {
    Scanner input;
    TextWriter output;

    void Run() {
        output.WriteLine("Hello world!");
    }

    #region Main

    static void Main(string[] args) {
        var p = new Program();
        p.input = args.Length == 0 ? Scanner.ForStdin() : Scanner.ForFile(args[0]);
        p.output = Console.Out;
        p.Run();
    }

    static string StringMain(string input) {
        var p = new Program();
        p.input = Scanner.ForString(input);
        p.output = new StringWriter();
        p.Run();
        return p.output.ToString();
    }

    void withBufferedOut(Action a) {
        if (output != Console.Out) {
            a();
            return;
        }
        var std = output;
        std.Flush();
        output = new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding, 8192);
        try {
            a();
        } finally {
            var o = output;
            output = std;
            o.Dispose();
        }
    }

    #endregion
}

#region StdLib

static class Exts {
    public static void Times(this int n, Action a) {
        for (var i = 0; i < n; i++) {
            a();
        }
    }

    public static void Times(this int n, int start, Action<int> a) {
        for (var i = start; i < start + n; i++) {
            a(i);
        }
    }

    public static void Times(this int n, Action<int> a) {
        n.Times(0, a);
    }

    public static V Get<K, V>(this IDictionary<K, V> d, K k, V def) {
        V v;
        return d.TryGetValue(k, out v) ? v : def;
    }
}

class Scanner : IEnumerator<string>, IEnumerable<string> {
    private TextReader reader;
    private string current;
    private System.Text.StringBuilder sb = new System.Text.StringBuilder();

    public Scanner(TextReader reader) {
        this.reader = reader;
    }

    public string Current {
        get {
            return current;
        }
    }

    object IEnumerator.Current {
        get {
            return Current;
        }
    }

    public bool MoveNext() {
        current = null;

        while (true) {
            var c = reader.Read();
            if (c < 0) return false;
            var ch = (char)c;
            if (char.IsWhiteSpace(ch)) continue;
            sb.Append(ch);
            break;
        }

        while (true) {
            var c = reader.Read();
            if (c < 0) break;
            var ch = (char)c;
            if (char.IsWhiteSpace(ch)) break;
            sb.Append(ch);
        }

        current = sb.ToString();
        sb.Clear(); // best to be in finally, but MEH
        return true;
    }

    public void Reset() {
        throw new NotImplementedException();
    }


    public void Dispose() {
        reader.Dispose();
    }

    public string Next() {
        MoveNext();
        return Current;
    }

    public int NextInt() {
        return int.Parse(Next());
    }

    public long NextLong() {
        return long.Parse(Next());
    }

    public double NextDouble() {
        return double.Parse(Next());
    }

    public IEnumerator<string> GetEnumerator() {
        return new SeqImp(this);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    private class SeqImp : IEnumerator<string> {
        private Scanner scanner;

        public SeqImp(Scanner s) { this.scanner = s; }

        public void Dispose() { }

        public string Current {
            get {
                return scanner.Current;
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public bool MoveNext() {
            return scanner.MoveNext();
        }

        public void Reset() {
            throw new NotImplementedException();
        }
    }

    public T[] NextArray<T>(int size, Func<string, T> parse) {
        var a = new T[size];
        int i = 0;
        while (i < size) {
            a[i] = parse(Next());
            i += 1;
        }
        return a;
    }

    public string[] NextArray(int size) {
        return NextArray(size, x => x);
    }

    public int[] NextIntArray(int size) {
        return NextArray(size, int.Parse);
    }

    public long[] NextLongArray(int size) {
        return NextArray(size, long.Parse);
    }

    public double[] NextDoubleArray(int size) {
        return NextArray(size, double.Parse);
    }

    public static Scanner ForString(string s) {
        return new Scanner(new StringReader(s));
    }

    public static Scanner ForFile(string file) {
        return new Scanner(new StreamReader(new FileStream(file, FileMode.Open)));
    }

    public static Scanner ForStdin() {
        return new Scanner(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding));
    }
}

#endregion