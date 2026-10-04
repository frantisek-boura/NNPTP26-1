using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal static class UserInputParser
    {

        private const int MIN_ARGUMENTS = 6;
        private const string DEFAULT_FILE_PATH = "../../../out.png";

        public static Options Parse(params string[] args)
        {
            Options options = new Options();

            if (args.Length < MIN_ARGUMENTS)
                throw new ArgumentException("Not enough arguments provided. Expected at least 7 arguments.");

            try
            {
                options.BitmapWidth = int.Parse(args[0]);
                options.BitmapHeight = int.Parse(args[1]);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid argument format. Please provide valid integers for bitmap dimensions.");
            }

            try
            {
                options.MinX = double.Parse(args[2]);
                options.MaxX = double.Parse(args[3]);
                options.MinY = double.Parse(args[4]);
                options.MaxY = double.Parse(args[5]);

            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid argument format. Please provide valid doubles for coordinate bounds.");
            }

            if (options.MinX >= options.MaxX || options.MinY >= options.MaxY)
                throw new ArgumentException("Invalid coordinate bounds. Ensure that MinX < MaxX and MinY < MaxY.");

            try
            {
                options.StepX = (options.MaxX - options.MinX) / options.BitmapWidth;
                options.StepY = (options.MaxY - options.MinY) / options.BitmapHeight;

            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException("Bitmap width and height must be greater than zero.");
            }

            try
            {
                options.BitmapFilePath = args[6];
            }
            catch (IndexOutOfRangeException)
            {
                options.BitmapFilePath = DEFAULT_FILE_PATH;
            }

            return options;
        }
    }
}
