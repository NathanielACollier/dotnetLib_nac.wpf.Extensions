﻿namespace nac.wpf.Extensions;

public static class Win32Extensions
{
    public static System.Windows.Forms.IWin32Window GetIWin32Window(System.Windows.Media.Visual visual)
    {
        var source = System.Windows.PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
        System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
        return win;
    }

    private class OldWindow : System.Windows.Forms.IWin32Window
    {
        private readonly System.IntPtr _handle;
        public OldWindow(System.IntPtr handle)
        {
            _handle = handle;
        }

        #region IWin32Window Members
        System.IntPtr System.Windows.Forms.IWin32Window.Handle
        {
            get { return _handle; }
        }
        #endregion
    }

}