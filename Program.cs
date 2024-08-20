using System.Globalization;
using System.IO;

//ConsoleColor holder = Console.ForegroundColor;

//Console.ForegroundColor = ConsoleColor.Green;
//Console.WriteLine();
//Console.ForegroundColor = holder;
print("***************************************\r\n    Screenshot Renamer by Adachi91\r\n***************************************", ConsoleColor.Green);

Console.WriteLine("\r\nPlease enter the full path to the Directory where the screenshots are located (example: C:\\User\\MyUser\\Pictures\\Screenshots)." +
    "\r\nAlternatively you can place the executable in the directory with the screenshots.\r\n>> At any point you can type quit to stop and exit the program.\r\n");

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


void print(string txt, ConsoleColor c = ConsoleColor.Magenta) {
    if (c == ConsoleColor.Magenta) {
        Console.WriteLine(txt);
        return;
    }

    ConsoleColor Neverevereverveverstop = Console.ForegroundColor; //Chvches - Never Say Die();
    Console.ForegroundColor = c;
    Console.WriteLine(txt);
    Console.ForegroundColor = Neverevereverveverstop;
}
