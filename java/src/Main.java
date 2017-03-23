import java.io.*;
import java.util.*;
import java.util.stream.*;

public class Main {
    private Scanner input;
    private PrintStream out = System.out;

    private void run() throws Exception {
        
    }

    public static void main(String[] args) throws Exception {
        Main main = new Main();
        main.input = args.length == 0 ? new Scanner(System.in) : new Scanner(new File(args[0]));
        main.run();
    }
}
