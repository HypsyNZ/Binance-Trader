/*
*MIT License
*
*Copyright (c) 2022 S Christison
*
*Permission is hereby granted, free of charge, to any person obtaining a copy
*of this software and associated documentation files (the "Software"), to deal
*in the Software without restriction, including without limitation the rights
*to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
*copies of the Software, and to permit persons to whom the Software is
*furnished to do so, subject to the following conditions:
*
*The above copyright notice and this permission notice shall be included in all
*copies or substantial portions of the Software.
*
*THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
*IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
*AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
*OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
*SOFTWARE.
*/

using System;
using System.Runtime.InteropServices;

namespace PrecisionTiming
{
    internal static class MMTimerExports
    {
        [DllImport(Constants.windowsMultimediaAPIString)]
        internal static extern int timeBeginPeriod(int period);

        [DllImport(Constants.windowsMultimediaAPIString)]
        internal static extern int timeEndPeriod(int period);

        [DllImport(Constants.windowsMultimediaAPIString)]
        internal static extern int timeGetDevCaps(ref TimerCapabilities caps, int sizeOfTimerCaps);

        [DllImport(Constants.windowsMultimediaAPIString)]
        internal static extern int timeKillEvent(int id);

        [DllImport(Constants.windowsMultimediaAPIString)]
        internal static extern int timeSetEvent(int delay, int resolution, TimerProc proc, IntPtr user, TimerMode mode);

        internal delegate void TimerProc(int hwnd, int uMsg, IntPtr idEvent, int dwTime, int WTFref);

        public static TimerCapabilities Capabilities;
    }
}
