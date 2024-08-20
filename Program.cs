using System.Globalization;
using System.IO;
using static moo; //idc I'm leavin it moo because it's cute.


//ConsoleColor holder = Console.ForegroundColor;

//Console.ForegroundColor = ConsoleColor.Green;
//Console.WriteLine();
//Console.ForegroundColor = holder;
print("***************************************\r\n    Screenshot Renamer by Adachi91\r\n***************************************", ConsoleColor.Green);

Console.WriteLine("\r\nPlease enter the full path to the Directory where the screenshots are located (example: C:\\User\\MyUser\\Pictures\\Screenshots)." +
    "\r\nAlternatively you can place the executable in the directory with the screenshots.");

print(">> At any point you can type quit to stop and exit the program.\r\n", ConsoleColor.Red);
print("hello |c0World|r I like |c6cats|r", ConsoleColor.Red);
string[] supportedExt = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".tga" };
string[] accepted_inputs = new string[] { "y", "n", "no", "yes", "exit", "cancel", "stop", "q", "quit" };

List<string> testDirectory = retrieveDirpyFiles(Environment.CurrentDirectory);


foreach(string file in testDirectory) {
    //Console.Write(Path.GetFileNameWithoutExtension(file).Length == "WoWScrnShot_120909_042440".Length);
    Console.WriteLine(file);
}

if(testDirectory.Count > 0) {
    Console.Write("Detected Screenshots. Proceede? (y/n) ");
    string f;
    while (true) {
        f = Console.ReadLine().ToLower();

        if (!string.IsNullOrWhiteSpace(f) && accepted_inputs.Contains(f))
            break;
        Console.Write("invalid input\r\nProceede? (y/n) ");
    }

    switch(f) {
        case "no":
        case "n":
            Console.WriteLine("Proccess cancelled, exiting...");
            Environment.Exit(0);
        break;
        case "yes":
        case "y":
            Console.WriteLine("Proceeding to fuck shit up"); //poolease change this
        break;
        case "exit":
        case "stop":
        case "cancel":
        case "q":
        case "quit":
            Console.WriteLine("Exiting program.");
        break;
    }
} else {
    string f;

    while(true) {
        f = Console.ReadLine();

        if(string.IsNullOrEmpty(f) || !Directory.Exists(f)) {
            Console.WriteLine($"Could not find directory {f}.");
        } else
            break;
    }
}


//WoWScrnShot_120909_042440.jpg
//WoWScrnShot_082820_223514.avif IT'S REAL DON'T QUESTION IT, just believe in the power of AV1 encoding
List<string> retrieveDirpyFiles(string path) {
    return Directory.GetFiles(path).Where(
        f => supportedExt.Contains(Path.GetExtension(f).ToLower())
        && !File.GetAttributes(f).HasFlag(FileAttributes.ReparsePoint)
        && !Path.GetFileName(f).EndsWith(".lnk", StringComparison.OrdinalIgnoreCase)
        && Path.GetFileNameWithoutExtension(f).StartsWith("WoWScrnShot_", StringComparison.CurrentCultureIgnoreCase)
        && Path.GetFileNameWithoutExtension(f).Length == 25
    ).ToList();
}


class moo {// Moooo (^o^)   )\

    /// <summary>
    ///  Colors the cow Moos in.
    /// </summary>
    private enum MooCodes {
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3
    }

    public static void print(string txt, ConsoleColor c = ConsoleColor.Magenta) {
        if (c == ConsoleColor.Magenta) {
            Console.WriteLine(txt);
            return;
        }

        ConsoleColor Neverevereverveverstop = Console.ForegroundColor; //Chvches - Never Say Die();

        var colorStack = new Stack<ConsoleColor>();
        colorStack.Push(Neverevereverveverstop); // Start with the default color

        int i = 0;
        while (i < txt.Length) {
            if (txt[i] == '|') {
                switch (txt[i]) {
                    case 'r':
                        if (colorStack.Count > 1) colorStack.Pop();
                        Console.ForegroundColor = colorStack.Peek();
                        i += 2;
                    break;
                    case 'c':
                        int color = txt[i + 2] - '0';

                        colorStack.Push((ConsoleColor)Enum.Parse(typeof(MooCodes), color.ToString()));
                        Console.ForegroundColor = colorStack.Last();
                        i += 3;
                    break;
                    default:
                        Console.Write(txt[i]);
                        i++;
                    break;
                }
            } else {
                Console.Write(txt[i++]);
            }
        }

        if (txt.Contains("|c")) {
            List<int> color_escape_start = new List<int>();
            List<int> color_escape_end = new List<int>();

            Dictionary<string, int> ColoredText = new Dictionary<string, int>();
            List<string> outerText = new List<string>();

            //int color_escape_start = txt.IndexOf("|c");
            //int color_escape_stop = txt.IndexOf("|r");
            //int ihopetheresafuckingmethodforthis = txt.LastIndexOf("|r");

                


                if (txt.IndexOf("|r") != txt.LastIndexOf("|r")) {
                //more than 1 escaperefsadaaaaasdaffsdsdafsdafasdf

                int pos = 0;
                int lastpos = txt.LastIndexOf("|r");

                while (pos < lastpos -1) {
                    int i = txt.IndexOf("|c");
                    int j = txt.IndexOf("|r");

                    int reader = txt.IndexOf("|c");
                    int reader_count = 0;
                    while(true) {
                        /*
                         * a |c0Apple|r is read und a |c3cat can be any |c4color|r|r
                         */
                        //read 1|c to make sure there isn't a nested 2|c before 1|r
                        int next = txt.IndexOf("|c", reader + 1);

                        if (next < txt.IndexOf("|r")) {
                            reader_count++; //found nesting
                            reader = txt.IndexOf("|c", next + 1);
                        } else {
                            //some logic to check end of string
                            continue;
                        }
                    }

                    outerText.Add(txt.Substring(0, i));
                    ColoredText.Add(txt.Substring(i + 1, i - j + 1), Convert.ToInt32(txt.Substring(i, 1)));

                    if(txt.IndexOf("|c", i) < j) {
                        //FUCK;
                    }
                    outerText.Add(txt.Substring(j + 2));


                    color_escape_start.Add(i);
                    color_escape_end.Add(j);
                    pos = j;
                }


            }

            /*string colored_text = txt.Substring(0, color_escape_stop);
            colored_text = colored_text.Substring(color_escape_start + 1);
            int colorcode = Convert.ToInt32(colored_text.Substring(1, 1));
            colored_text = colored_text.Substring(2);

            Console.WriteLine($"The text is {colored_text} and the colorcode is {colorcode}");

            ConsoleColor color;

            switch(colorcode) {
                case 0: color = ConsoleColor.Red; break;
                case 1: color = ConsoleColor.Yellow; break;
                case 2: color = ConsoleColor.Green; break;
                case 3: color = ConsoleColor.Blue; break;
                case 5: color = ConsoleColor.Yellow; break;
                case 6: color = ConsoleColor.Blue; break;
                case 7: color = ConsoleColor.Magenta; break;
                case 8: color = ConsoleColor.Yellow; break;
                default: throw new Exception("You entered an incorrect colorcode");
            }

            Console.Write($"{txt.Substring(0, color_escape_start)}");
            Console.ForegroundColor = color;
            Console.Write($"{colored_text}");
            Console.ForegroundColor = Neverevereverveverstop;
            Console.Write($"{txt.Substring(color_escape_stop + 2)}\r\n");*///color_escape_start + 1 + colored_text.Length)}")
            //Console.WriteLine(txt.Substring(0, color_escape_stop));

            return;
        }

        Console.ForegroundColor = c;
        Console.WriteLine(txt);
        Console.ForegroundColor = Neverevereverveverstop;
    }
}
