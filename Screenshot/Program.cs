using System.Diagnostics;
using System.Drawing;
using System.IO;

class Menu
{
    private int SelectedIndex;
    private string[] Options;
    public Menu(string[] options)
    {
        SelectedIndex = 0;
        Options = options;
    }

    private void DisplayOptions()
    {
        Console.WriteLine();
        for (int i = 0; i < Options.Length; i++)
        {
            string prefix;
            string currentOption = Options[i];
            if (SelectedIndex == i)
            {
                prefix = "*";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = "";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

            }
            Console.WriteLine($"{prefix}<<{currentOption}>>");
        }
        Console.ResetColor();
    }
    public int Run()
    {

        ConsoleKey key;
        do
        {
            Console.Clear();
            DisplayOptions();
            ConsoleKeyInfo info = Console.ReadKey(true);
            key = info.Key;

            if (key == ConsoleKey.UpArrow)
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                    SelectedIndex = Options.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                SelectedIndex++;
                if (SelectedIndex == Options.Length)
                    SelectedIndex = 0;
            }


        } while (key != ConsoleKey.Enter);
        return SelectedIndex;

    }

    public void RunMenu(int selectedIndex)
    {
         void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }

        switch (selectedIndex)
        {
            case 0:
                Console.Clear();

                if (!Directory.Exists(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens"))
                    Directory.CreateDirectory(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens");

                Console.WriteLine("Starting the process...");
                Console.WriteLine();
                Bitmap memoryImage;
                memoryImage = new Bitmap(1920, 1080);
                Size s = new Size(memoryImage.Width, memoryImage.Height);

                Graphics memoryGraphics = Graphics.FromImage(memoryImage);

                memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
                memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
                string fileName = string.Format($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens" +
                          @"\Screens" + "_" +
                          DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png");
                
                memoryImage.Save(fileName);

                Console.WriteLine("Picture has been saved...");
                Console.ReadKey(false);
                Run();

                break;
            case 1:
                Console.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens");
                short i = 1;

                foreach (FileInfo file in directoryInfo.GetFiles())
                    Console.WriteLine($"{i++}. {file.Name}");


                Console.Write("\nPress Any key for come back to menu ...");
                Console.ReadKey(false);
                Run();

                break;
            case 2:
                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter file name(Press 0 if you want come back to menu): ");
                    string path = Console.ReadLine();

                    if (path == "0") break;

                    try
                    {
                        if (string.IsNullOrEmpty(path))
                            throw new ArgumentNullException("File name");

                        if (File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens\{path}") is false)
                            throw new FileNotFoundException();

                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);

                        Thread.Sleep(1500);
                        continue;
                    }

                    OpenWithDefaultProgram($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens\{path}");

                }
                Run();
                break;
            case 3:
                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter file name(Press 0 if you want come back to menu): ");
                    string path = Console.ReadLine();

                    if (path == "0") break;

                    try
                    {
                        if (string.IsNullOrEmpty(path))
                            throw new ArgumentNullException("File name");

                        if (File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens\{path}") is false)
                            throw new FileNotFoundException();

                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);

                        Thread.Sleep(1500);
                        continue;
                    }

                    File.Delete($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens\{path}");
                    Console.WriteLine("Screenshot has been deleted...");
                    Console.ReadKey(false);
                }
                Run();
                break;
            case 4:
                Console.Clear();
                directoryInfo = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Screens");
                foreach (FileInfo file in directoryInfo.GetFiles())
                    File.Delete(file.FullName);

                Console.WriteLine("All Screenshots has been deleted...");
                Console.ReadKey(false);
                Run();
                break;
            case 5:
                Console.Clear();
                Console.WriteLine("============ You have exited ============");
                Console.ReadKey(false);
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }

}


class Program
{
    static void Main()
    {

        string[] Options = { "Take Screenshot", "See Screenshots", "Open With Default Program", "Delete with file name", "Delete all Screenshots", "Exit" };
        Menu menu = new Menu(Options);
        
        while(true)
        {
            menu.RunMenu(menu.Run());
        }
        

    }
}

