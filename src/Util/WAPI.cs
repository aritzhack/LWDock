using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LWDock
{
    static class WAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public struct POINTAPI
        {
            public int x;
            public int y;
        }

        public struct RECTAPI
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref POINTAPI lpPoint);
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        public const int NORMAL_BORDER_WIDTH = 45; // SM_CXEDGE
        public const int NORMAL_BORDER_HEIGHT = 46; // SM_CYEDGE

        public const int BORDER_3D_WIDTH = 5; // SM_CXBORDER
        public const int BORDER_3D_HEIGHT = 6; // SM_CYBORDER

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_MOVING = 0x216;
        public const int HT_CAPTION = 0x2;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon


        /// <summary>
        /// Window with this style will not show up in the Alt-Tab window list
        /// </summary>
        public const int WS_EX_TOOLWINDOW = 0x80;

        public static void dragWindow(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public static Icon GetIcon(string fileName, uint flags)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgSmall = SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | flags);

            Icon icon = (Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon).Clone();
            DestroyIcon(shinfo.hIcon);
            return icon;
        }

        public static Point getCursorPos()
        {
            POINTAPI p = new POINTAPI();
            GetCursorPos(ref p);
            return new Point(p.x, p.y);
        }
    }
}
