using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveEffectGenerator {
    class Program {
        static void Main() {
            var args = CommandLineArguments.Parse();

            if (args.Count < 3) {
                Console.WriteLine("Usage: wavegen.exe [infile] [distance] [period] (optional: outfile)");
                Console.WriteLine("  infile: file to process");
                Console.WriteLine("  distance: number of pixels effect will extend to either side");
                Console.WriteLine("  period: vertical size of effect before repeat (also number of frames)");
                Console.WriteLine("  outfile: filename to use for frames, if you pass 'out.png', frames will be");
                Console.WriteLine("           saved as 'out000.png', 'out001.png', etc. If not specified the frame");
                Console.WriteLine("           numbers will be added between the source file name and its extension");
                return;
            }

            var inFile = args[0];
            var distance = Convert.ToInt32(args[1]);
            var period = Convert.ToInt32(args[2]);

            string outFile;
            if (args.Count > 3) {
                outFile = Path.GetFileNameWithoutExtension(args[3]) + "?" + Path.GetExtension(args[3]);
            }
            else {
                outFile = Path.GetFileNameWithoutExtension(inFile) + "?" + Path.GetExtension(inFile);
            }

            Console.WriteLine("Loading source image");
            var original = Bitmap.FromFile(inFile);
            var source = new Bitmap(original.Width + distance * 2, original.Height);

            Console.WriteLine("Resizing image canvas");
            using (var gfx = Graphics.FromImage(source)) {
                gfx.DrawImage(original, new Rectangle(distance, 0, original.Width, original.Height), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }
            original.Dispose();

            for (int i = 0; i < period; i++) {
                Console.WriteLine("Processing frame " + i);
                var frame = new Bitmap(source.Width, source.Height);
                using (var gfx = Graphics.FromImage(frame)) {
                    for (int y = 0; y < source.Height; y++) {
                        var offset = (int)Math.Round(Math.Sin((y + i) * Math.PI * 2 / period) * distance);

                        var location = new Point(distance, y);
                        var offsetLocation = new Point(location.X + offset, location.Y);
                        var size = new Size(source.Width, 1);

                        gfx.DrawImage(source, new Rectangle(offsetLocation, size), new Rectangle(location, size), GraphicsUnit.Pixel);
                    }
                }
                frame.Save(outFile.Replace("?", i.ToString("D3")), ImageFormat.Png);
            }

            Console.WriteLine("Done!");
        }
    }
}
