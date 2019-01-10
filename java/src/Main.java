import java.io.*;
import java.util.*;
import java.util.stream.*;


public class Main {
    private void run() {
        out.println("Hello world!");
    }


    private FastScanner input;
    private PrintStream out;


    public static void main(String[] args) throws Exception {
        Main main = new Main();
        main.input = args.length == 0 ? new FastScanner() : new FastScanner(new File(args[0]));
        main.out = System.out;
        main.run();
    }


    public static String stringMain(String input) {
        Main main = new Main();
        main.input = new FastScanner(input);
        ByteArrayOutputStream buffer = new ByteArrayOutputStream();
        try (PrintStream out = new PrintStream(buffer)) {
            main.out = out;
            main.run();
        }
        return buffer.toString();
    }


    private static class FastScanner implements Iterator<String>, Iterable<String>, Closeable {
        private Reader reader;
        private StringBuilder sb = new StringBuilder();
        private char[] buf = new char[4096];
        private char current;
        private int beg = 0;
        private int end = 0;

        public FastScanner(Reader reader) {
            this.reader = reader;
        }

        public FastScanner(File f) {
            try {
                this.reader = new FileReader(f);
            } catch (FileNotFoundException e) {
                throw new RuntimeException(e);
            }
        }

        public FastScanner() {
            this.reader = new InputStreamReader(System.in);
        }

        public FastScanner(String s) {
            this.reader = new StringReader(s);
        }

        @Override
        public Iterator<String> iterator() {
            return this;
        }

        @Override
        public boolean hasNext() {
            if (sb.length() > 0) return true;
            while (read()) {
                if (Character.isWhitespace(current)) continue;
                sb.append(current);
                return true;
            }
            return false;
        }

        @Override
        public String next() {
            if (!hasNext()) return null;
            while (read()) {
                if (Character.isWhitespace(current)) break;
                sb.append(current);
            }
            String s = sb.toString();
            sb.setLength(0);
            return s;
        }

        private boolean read() {
            while (beg >= end) {
                try {
                    int len = reader.read(buf);
                    if (len < 0) return false;
                    beg = 0;
                    end = beg + len;
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            }
            current = buf[beg];
            beg += 1;
            return true;
        }

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public long nextLong() {
            return Long.parseLong(next());
        }

        public double nextDouble() {
            return Double.parseDouble(next());
        }

        public Stream<String> stream() {
            return StreamSupport.stream(this.spliterator(), false);
        }

        public Stream<String> stream(int size) {
            return StreamSupport.stream(Spliterators.spliterator(this.iterator(), size, 0), false).limit(size);
        }

        public IntStream intStream() {
            return stream().mapToInt(Integer::parseInt);
        }

        public IntStream intStream(int size) {
            return stream(size).mapToInt(Integer::parseInt);
        }

        public LongStream longStream() {
            return stream().mapToLong(Long::parseLong);
        }

        public LongStream longStream(int size) {
            return stream(size).mapToLong(Long::parseLong);
        }

        public DoubleStream doubleStream() {
            return stream().mapToDouble(Double::parseDouble);
        }

        public DoubleStream doubleStream(int size) {
            return stream(size).mapToDouble(Double::parseDouble);
        }

        public String nextArray(int size) {
            String[] a = new String[size];
            for (int i = 0; i < size; i++) {
                a[i] = next();
            }
            return a;
        }

        public int[] nextIntArray(int size) {
            int[] a = new int[size];
            for (int i = 0; i < size; i++) {
                a[i] = nextInt();
            }
            return a;
        }

        public long[] nextLongArray(int size) {
            long[] a = new long[size];
            for (int i = 0; i < size; i++) {
                a[i] = nextLong();
            }
            return a;
        }

        public double[] nextDoubleArray(int size) {
            double[] a = new double[size];
            for (int i = 0; i < size; i++) {
                a[i] = nextDouble();
            }
            return a;
        }

        @Override
        public void close() throws IOException {
            reader.close();
        }
    }
}
