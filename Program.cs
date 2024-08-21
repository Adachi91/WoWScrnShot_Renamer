using System.Globalization;
using System.IO;
using static moo; //idc I'm leavin it moo because it's cute.

print($"|c2***************************************|n    Screenshot Renamer by Adachi91|n***************************************|r|n");

print($"Please enter the full path to the Directory where the screenshots are located (example: C:\\User\\MyUser\\Pictures\\Screenshots)." +
    $"|nAlternatively you can place the executable in the directory with the screenshots.");


print($"|n|n|c1>> Type quit to exit the program.|r|n");

string[] supportedExt = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".tga" };
string[] IO_accept = new string[] { "y", "yes", "continue" };
string[] IO_cancel = new string[] { "n", "no", "cancel", "stop" };
string[] exit_strings = new string[] { "exit", "q", "end", "quit" };
string WORKING_DIRECTORY = "";
List<string> testDirectory = retrieve_files(Environment.CurrentDirectory);


if (testDirectory.Count > 0)
{
    string f;
    while (true)
    {
        print("|c2Detected Screenshots.|r Proceede? (y/n) ");
        f = Console.ReadLine().ToLower();

        if (!string.IsNullOrWhiteSpace(f) && (IO_accept.Contains(f) || IO_cancel.Contains(f) || exit_strings.Contains(f)))
            break;
        print($"|c0invalid input|r|n");
    }

    switch (f)
    {
        case "no":
        case "n":
            print("|c2Ignoring current directory.|r|n");
            await confirm_working_directory();
            break;
        case "yes":
        case "y":
            WORKING_DIRECTORY = Environment.CurrentDirectory;
            await rename_files(testDirectory);
            break;
        case "exit":
        case "stop":
        case "cancel":
        case "q":
        case "quit":
            print("|c0Exiting program.|r");
            Environment.Exit(0);
            break;
    }
}
else
    await confirm_working_directory();




async Task confirm_working_directory() {
    string f;

    while (true) {
        print("Enter Path> ");
        f = Console.ReadLine();

        if (exit_strings.Contains(f)) {
            print("|c0Operation cancelled. Exiting program.|r|n");
            Environment.Exit(0);
            break;
        }

        if (string.IsNullOrEmpty(f) || !Directory.Exists(f)) {
            print($"|c1Could not find directory {f}.|r|n");
        } else {
            WORKING_DIRECTORY = f;
            break;
        }
    }

    print($"You set the working directory as |c1\"{WORKING_DIRECTORY}\"|r. Is this correct? (y/n) ");
    string confirm_working_dir = Console.ReadLine();

    if (IO_accept.Contains(confirm_working_dir))
            await rename_files(retrieve_files(WORKING_DIRECTORY));
        else {
            print($"|c0Directory {WORKING_DIRECTORY} is invalid. It does not contain any WoW related screenshot names.|r|n");
            _ = confirm_working_directory();
        }

    if (IO_cancel.Contains(confirm_working_dir))
        _ = confirm_working_directory();

    if (exit_strings.Contains(confirm_working_dir))
        print($"|c1Operation cancelled. Exiting program.|r|n");
}


async Task rename_files(List<string> files) {

    int counter = 0;

    if (files.Count < 1) {
        print($"|c0Directory {WORKING_DIRECTORY} is invalid. It does not contain any WoW related screenshot names.|r|n");
        _ = confirm_working_directory();
        return;
    }

    print("|n|n|c3Starting Renamer.|n================================================================================|r|n|n");

    foreach(var file in files) {
        string current_filename = Path.GetFileNameWithoutExtension(file);
        string file_ext = Path.GetExtension(file);

        string file_date = current_filename.Substring(12, 6);
        string file_time = current_filename.Substring(19, 6);
        
        if (DateTime.TryParseExact(file_date + file_time, "MMddyyHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime file_dt)) {
            string new_file_name = $"WoWScrnShot_{file_dt:yyyyMMdd_HHmmss}{file_ext}";
            string new_file_path = Path.Combine(WORKING_DIRECTORY, new_file_name);

            try {
                File.Move(file, new_file_path);
                counter++;
                print($"|c3Renamed {current_filename}{file_ext} => to => {new_file_name}|r|n");
            } catch (Exception ex) {
                print($"|n|c0Could not rename {current_filename}{file_ext}. Error: {ex}.|nPlease report this error to the developer if it presists|r|n");
            }
        }
    }

    print($"|n|c2Complete! Proccessed {counter} files. Enjoy your Sortable Files.|r|nPress any key to close this window.");
    Console.ReadKey();
}


//WoWScrnShot_120909_042440.jpg
//WoWScrnShot_082820_223514.avif IT'S REAL DON'T QUESTION IT, just believe in the power of AV1 encoding
List<string> retrieve_files(string path) {
    return Directory.GetFiles(path).Where(
        f => supportedExt.Contains(Path.GetExtension(f).ToLower())
        && !File.GetAttributes(f).HasFlag(FileAttributes.ReparsePoint)
        && !Path.GetFileName(f).EndsWith(".lnk", StringComparison.OrdinalIgnoreCase)
        && Path.GetFileNameWithoutExtension(f).StartsWith("WoWScrnShot_", StringComparison.CurrentCultureIgnoreCase)
        && Path.GetFileNameWithoutExtension(f).Length == 25
    ).ToList();
}


// YOU WILL NEVER TAKE moo FROM ME!
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
class moo {// Moooo (^o^)   )\
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public static void print(string txt) {
        ConsoleColor Neverevereverveverstop = Console.ForegroundColor; //Chvches - Never Say Die();

        var colorStack = new Stack<ConsoleColor>();
        colorStack.Push(Neverevereverveverstop);

        int i = 0;
        while (i < txt.Length) {
            if (txt[i] == '|') {
                switch (txt[++i]) {
                    case 'r':
                        if (colorStack.Count > 1) colorStack.Pop();
                        Console.ForegroundColor = colorStack.Peek();
                        i += 1;
                    break;
                    case 'c':
                        int color = txt[i + 1] - '0';

                        ConsoleColor Moo = color switch {
                            0 => ConsoleColor.Red,
                            1 => ConsoleColor.Yellow,
                            2 => ConsoleColor.Green,
                            3 => ConsoleColor.Blue,
                            5 => ConsoleColor.Magenta,
                            6 => ConsoleColor.Blue,
                            7 => ConsoleColor.Magenta,
                            8 => ConsoleColor.Yellow,
                            _ => throw new Exception("You entered an incorrect colorcode. I DON'T WANT TO BE HERE ANYMORE")
                        };

                        colorStack.Push(Moo);
                        i += 2;
                    break;
                    case 'n':
                        Console.Write(Environment.NewLine);
                        i++;
                    break;
                    default:
                        Console.Write(txt[i]);
                        i++;
                    break;
                }
            } else {
                Console.ForegroundColor = colorStack.Peek();
                Console.Write(txt[i++]);
            }
        }
    }
}
