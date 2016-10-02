namespace MoveDirectory
{
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!Directory.Exists(args[0]))
                {
                    Console.Write($"Nothing to move. Source directory does not exist: '{args[0]}'");

                    Environment.Exit(0);
                }

                if (!ValidateArgs(args))
                {
                    Environment.Exit(1);
                }

                Console.Write($"Moving '{args[0]}' to '{args[1]}' ...");

                var destinationParent = Directory.GetParent(args[1]);

                if (!destinationParent.Exists)
                {
                    destinationParent.Create();
                }

                Directory.Move(args[0], args[1]);

                Console.WriteLine(" done!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error occurred: {e.Message}");
                Environment.Exit(1);
            }
        }

        private static bool ValidateArgs(string[] args)
        {
            var valid = true;

            if (!IsDirectory(args[0]))
            {
                valid = false;
                Console.WriteLine($"Source is not a directory: '{args[0]}'");
            }

            if (Directory.Exists(args[1]))
            {
                valid = false;
                Console.WriteLine($"Destination directory not empty: '{args[1]}'");
            }

            return valid;
        }

        private static bool IsDirectory(string path)
        {
            return File.GetAttributes(path)
                  .HasFlag(FileAttributes.Directory);
        }
    }
}
