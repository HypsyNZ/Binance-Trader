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

namespace BTNET.BV.Abstract
{
    internal class SettingsObject
    {
        public SettingsObject(bool showBorrowInfoIsChecked, bool showSymbolInfoIsChecked, bool showBreakDownInfoIsChecked,
            bool showMarginInfoIsChecked, bool showIsolatedInfoIsChecked, bool transparentTitleIsChecked, bool checkForUpdates, bool sellLimitChecked, bool sellBorrowChecked,
            bool buyBorrowChecked, bool buyLimitChecked, bool showNotifications, bool disableOpacity,
            bool autoSave, bool keepFirstOrder, bool dangerousButtonsIsChecked, double watchListHeight, bool showScraperButtonIsChecked)
        {
            ShowBorrowInfoIsChecked = showBorrowInfoIsChecked;
            ShowSymbolInfoIsChecked = showSymbolInfoIsChecked;
            ShowBreakDownInfoIsChecked = showBreakDownInfoIsChecked;
            ShowMarginInfoIsChecked = showMarginInfoIsChecked;
            ShowIsolatedInfoIsChecked = showIsolatedInfoIsChecked;
            ShowScraperButtonIsChecked = showScraperButtonIsChecked;

            TransparentTitleIsChecked = transparentTitleIsChecked;
            DisableOpacity = disableOpacity;

            CheckForUpdates = checkForUpdates;

            SellLimitChecked = sellLimitChecked;
            BuyLimitChecked = buyLimitChecked;
            SellBorrowChecked = sellBorrowChecked;
            BuyBorrowChecked = buyBorrowChecked;

            ShowNotifications = showNotifications;
            AutoSaveSettings = autoSave;

            KeepFirstOrder = keepFirstOrder;
            DangerousButtonsIsChecked = dangerousButtonsIsChecked;
            WatchListHeight = watchListHeight;
        }

        public bool DisableOpacity { get; set; }
        public bool TransparentTitleIsChecked { get; set; }

        public bool ShowBorrowInfoIsChecked { get; set; } = true;
        public bool ShowSymbolInfoIsChecked { get; set; } = true;
        public bool ShowBreakDownInfoIsChecked { get; set; } = true;
        public bool ShowMarginInfoIsChecked { get; set; } = true;
        public bool ShowIsolatedInfoIsChecked { get; set; } = true;
        public bool ShowScraperButtonIsChecked { get; set; } = true;

        public bool DangerousButtonsIsChecked { get; set; } = false;

        public double WatchListHeight { get; set; } = 200;

        public bool CheckForUpdates { get; set; } = false;

        public bool SellLimitChecked { get; set; } = false;
        public bool SellBorrowChecked { get; set; } = false;

        public bool BuyLimitChecked { get; set; } = false;
        public bool BuyBorrowChecked { get; set; } = false;

        public bool ShowNotifications { get; set; } = true;
        public bool AutoSaveSettings { get; set; } = false;

        public bool KeepFirstOrder { get; set; } = false;
    }
}
