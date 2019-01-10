using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;


namespace Lib {
    public class App {
        private Scanner input;
        private TextWriter output;
        private bool bufferedOutput;


        protected virtual void Run() {
            output.WriteLine("Hello world!");
        }


        protected Scanner In {
            get {
                return input;
            }
        }


        protected TextWriter Out {
            get {
                return output;
            }
        }


        public static void Exec<T>(string[] args) where T : App, new() {
            var app = new T();
            app.input = args.Length == 0 ? Scanner.ForStdin() : Scanner.ForFile(args[0]);
            app.output = Console.Out;
            app.Run();
        }


        public static string ExecString<T>(string input) where T : App, new() {
            var app = new T();
            app.input = Scanner.ForString(input);
            app.output = new StringWriter();
            app.Run();
            return app.output.ToString();
        }


        protected void SetBufferedOutput() {
            if (output == Console.Out) {
                output = new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding, 8192);
                bufferedOutput = true;
            }
        }


        protected void SetNormalOutput() {
            if (bufferedOutput) {
                output.Flush();
                output = Console.Out;
                bufferedOutput = false;
            }
        }
    }


    public class Scanner : IEnumerator<string>, IEnumerable<string> {
        private TextReader reader;
        private string current;
        private StringBuilder sb = new StringBuilder();


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
                return current;
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
            sb.Clear();
            return true;
        }


        public void Reset() {
            throw new NotSupportedException();
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
                throw new NotSupportedException();
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
}