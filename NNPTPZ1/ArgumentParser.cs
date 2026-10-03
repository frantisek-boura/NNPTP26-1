using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal class ArgumentParser
    {

        private const string DEFAULT_FILE_PATH = "../../../out.png";

        public int BitmapWidth { get; private set; }
        public int BitmapHeight { get; private set; }
        public string BitmapFilePath { get; private set; }
        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        public double StepX { get; private set; }
        public double StepY { get; private set; }

        public ArgumentParser(params string[] args)
        {
            if (args.Length < 7)
                throw new ArgumentException("Not enough arguments provided. Expected at least 7 arguments.");

            try
            {
                BitmapWidth = int.Parse(args[0]);
                BitmapHeight = int.Parse(args[1]);
                BitmapFilePath = args[6];
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid argument format. Please provide valid integers for bitmap dimensions.");
            }

            try
            {
                MinX = double.Parse(args[2]);
                MaxX = double.Parse(args[3]);
                MinY = double.Parse(args[4]);
                MaxY = double.Parse(args[5]);

            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid argument format. Please provide valid doubles for coordinate bounds.");
            }

            if (MinX >= MaxX || MinY >= MaxY)
                throw new ArgumentException("Invalid coordinate bounds. Ensure that MinX < MaxX and MinY < MaxY.");

            try
            {
                StepX = (MaxX - MinX) / BitmapWidth;
                StepY = (MaxY - MinY) / BitmapHeight;

            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException("Bitmap width and height must be greater than zero.");
            }

            try
            {
                BitmapFilePath = args[6];
            }
            catch (IndexOutOfRangeException)
            {
                BitmapFilePath = DEFAULT_FILE_PATH;
            }
        }
    }
}
