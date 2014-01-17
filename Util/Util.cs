using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LWDock.Forms;

namespace LWDock
{
    static class Util
    {
        public static Icon GetSmallIcon(string fileName)
        {
            return WAPI.GetIcon(fileName, WAPI.SHGFI_SMALLICON);
        }

        public static Icon GetLargeIcon(string fileName)
        {
            return WAPI.GetIcon(fileName, WAPI.SHGFI_LARGEICON);
        }

        public static Bitmap getIcon(string filename)
        {
            if (ShellFile.IsPlatformSupported && ShellFolder.IsPlatformSupported && Settings.getInstance().iconQuality != (int)IconQuality.Minimum)
            {
                ShellObject shObj;
                Bitmap bm;

                if (Directory.Exists(filename))
                {
                    shObj = ShellFolder.FromParsingName(filename);
                    return GetLargeIcon(filename).ToBitmap();
                }
                else
                {
                    shObj = ShellFile.FromFilePath(filename);
                }

                ShellThumbnail thumbnail = shObj.Thumbnail;
                int quality = Settings.getInstance().iconQuality;
                try
                {
                    bm = quality == (int)IconQuality.Medium ? thumbnail.MediumBitmap : quality == (int)IconQuality.Small ? thumbnail.SmallBitmap : quality == (int)IconQuality.Big ? thumbnail.LargeBitmap : thumbnail.MediumBitmap;
                    if (bm != null)
                    {
                        shObj.Dispose();

                        bm.MakeTransparent(Color.Black);
                        return bm;
                    }
                }
                catch
                {
                    bm = null;
                }
                shObj.Dispose();
            }
            return GetLargeIcon(filename).ToBitmap();
        }

        public static Bitmap scale(Bitmap bitmap, float scale)
        {
            return new Bitmap(bitmap, new Size((int)(bitmap.Width * scale), (int)(bitmap.Height * scale)));
        }

        //TODO popup does not work!
        public static Size getSquarest(int amount, bool vertical, bool popup)
        {
            if (amount == 0) return new Size(0, 0);
            Size temp = new Size(vertical ? 0 : amount, vertical ? amount : 0);
            if (Settings.getInstance().tryOneLine && !popup) return new Size(amount, 1);
            int sqrt = (int)Math.Ceiling(Math.Sqrt(amount));

            while (amount % sqrt != 0)
            {
                if (sqrt == amount) break;
                sqrt++;
            }

            int big = sqrt;
            int small = amount / sqrt;
            return new Size(vertical ? small : big, vertical ? big : small);
        }

        public static bool fitsAmount(Size amounts)
        {
            return DockElement.getFinalSize(amounts).Width < Screen.PrimaryScreen.WorkingArea.Width;
        }

        public static Size improveSquares(Size squareAmount)
        {
            Size totalSize = DockElement.getFinalSize(squareAmount);
            int totalAmount = squareAmount.Width * squareAmount.Height;

            while (totalSize.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                squareAmount = new Size((int)Math.Ceiling((double)totalAmount / ((double)squareAmount.Height + 1)), squareAmount.Height + 1);
                totalSize = DockElement.getFinalSize(squareAmount);
            }
            return squareAmount;
        }

        public static int parseInt(string integer, int def)
        {
            try
            {
                return int.Parse(integer);
            }
            catch (System.FormatException)
            {
                return def;
            }
        }

        public static bool parseBool(string boolean, bool def)
        {
            try
            {
                return bool.Parse(boolean);
            }
            catch (System.FormatException)
            {
                return def;
            }
        }

        public static Image resize(Bitmap icon, int p1, int p2)
        {
            return new Bitmap(icon, p1, p2);
        }
    }
}
