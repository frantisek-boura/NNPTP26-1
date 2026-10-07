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

        private const int MinArguments = 6;
        private const string DefaultFilePath = "../../../out.png";

        public static Options Parse(params string[] args)
        {
            Options options = new Options();

            if (args.Length < MinArguments)
                throw new ArgumentException("Not enough arguments provided. Expected at least 7 arguments.");

            try
            {
                options.BitmapWidth = int.Parse(args[0]);
                options.BitmapHeight = int.Parse(args[1]);
                options.StepX = (options.MaxX - options.MinX) / options.BitmapWidth;
                options.StepY = (options.MaxY - options.MinY) / options.BitmapHeight;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid argument format. Please provide valid integers for bitmap dimensions.");
            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException("Bitmap width and height must be greater than zero.");
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
                options.BitmapFilePath = args[6];
            }
            catch (IndexOutOfRangeException)
            {
                options.BitmapFilePath = DefaultFilePath;
            }

            return options;
        }
    }
}
