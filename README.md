# WaveEffectGenerator

Generates frames for a retro wave effect as seen on SNES and other 16 bit consoles using horizontal displacement.

Build or download the [zipped executable](wavegen.exe).

# Usage

```
Usage: wavegen.exe [infile] [distance] [period] (optional: outfile)
  infile: file to process
  distance: number of pixels effect will extend to either side
  period: vertical size of effect before repeat (also number of frames)
  outfile: filename to use for frames, if you pass 'out.png', frames will be
           saved as 'out000.png', 'out001.png', etc. If not specified the frame
           numbers will be added between the source file name and its extension
```

# Sample

Source image (200% zoom)

![Source](sample.png)

Animated gif made from output using distance 1 and period 10 (200% zoom)

![Output](sample.gif)