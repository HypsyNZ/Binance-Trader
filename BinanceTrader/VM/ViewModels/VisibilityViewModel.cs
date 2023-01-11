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

using BTNET.BV.Enum;
using BTNET.BVVM;
using BTNET.BVVM.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTNET.VM.ViewModels
{
    public class VisibilityViewModel : Core
    {
        private double panelRealTimeLeft = 1055;
        private double panelRealTimeTop = -400;
        private double panelBorrowBoxLeft = 10;
        private double panelBorrowBoxTop = -170;
        private double panelMarginInfoLeft = 113;
        private double panelMarginInfoTop = -170;
        private double panelBreakdownLeft = 10;
        private double panelBreakdownTop = -279;
        private double panelInfoBoxLeft = 10;
        private double panelInfoBoxTop = -385;
        private double panelTradeInfoLeft = 10;
        private double panelTradeInfoTop = -578;
        private double panelScraperLeft = 10;
        private double panelScraperTop = -805;
        private double insightsPanelLeft = 566;
        private double insightsPanelTop = -577;

        private const int INTEREST_WIDTH_DEFAULT = 105;
        private const int PADDING_DEFAULT = 100;
        private const int ORDER_SETTINGS_WIDTH_DEFAULT = 67;
        private const int ORDER_LIST_DEFAULT_HEIGHT = 80;

        private double interestWidth = INTEREST_WIDTH_DEFAULT;
        private bool hideSettleTab;

        private double orderSettingsWidthFrom = 0;
        private double orderSettingsWidthTo = ORDER_SETTINGS_WIDTH_DEFAULT;
        private bool orderSettingsVisibility = true;
        private double orderListWidthOffset;
        private double orderListHeight = ORDER_LIST_DEFAULT_HEIGHT;
        private double watchListHeight = 200;

        public ICommand? OrderSettingsCommand { get; set; }

        public void InitializeCommands()
        {
            OrderSettingsCommand = new DelegateCommand(OrderSettingsToggle);
        }

        public double WatchListHeight
        {
            get => watchListHeight;
            set
            {
                watchListHeight = value;
                PropChanged();
            }
        }

        public double OrderListHeight
        {
            get => orderListHeight;
            set
            {
                orderListHeight = value;
                PropChanged();
            }
        }

        public double OrderListWidthOffset
        {
            get => orderListWidthOffset;
            set
            {
                orderListWidthOffset = value;
                PropChanged();
            }
        }

        public bool HideSettleTab
        {
            get => hideSettleTab;
            set
            {
                hideSettleTab = value;
                PropChanged();
            }
        }

        public double InterestWidth
        {
            get => interestWidth;
            set
            {
                interestWidth = value;
                PropChanged();
            }
        }

        public double OrderSettingsWidthFrom
        {
            get => orderSettingsWidthFrom;
            set
            {
                orderSettingsWidthFrom = value;
                PropChanged();
            }
        }

        public double OrderSettingsWidthTo
        {
            get => orderSettingsWidthTo;
            set
            {
                orderSettingsWidthTo = value;
                PropChanged();
            }
        }

        public bool OrderSettingsVisibility
        {
            get => orderSettingsVisibility;
            set
            {
                orderSettingsVisibility = value;
                PropChanged();
            }
        }

        public double InsightsPanelLeft
        {
            get => insightsPanelLeft; // 646
            set
            {
                insightsPanelLeft = value;
                PropChanged();
            }
        }

        public double InsightsPanelTop
        {
            get => insightsPanelTop; // -577
            set
            {
                insightsPanelTop = value;
                PropChanged();
            }
        }

        public double ScraperLeft
        {
            get => panelScraperLeft;
            set
            {
                panelScraperLeft = value;
                PropChanged();
            }
        }

        public double ScraperTop
        {
            get => panelScraperTop; // -805
            set
            {
                panelScraperTop = value;
                PropChanged();
            }
        }

        public double TradeInfoLeft
        {
            get => panelTradeInfoLeft;
            set
            {
                panelTradeInfoLeft = value;
                PropChanged();
            }
        }

        public double TradeInfoTop
        {
            get => panelTradeInfoTop;
            set
            {
                panelTradeInfoTop = value;
                PropChanged();
            }
        }

        public double PanelBreakdownLeft
        {
            get => panelBreakdownLeft;
            set
            {
                panelBreakdownLeft = value;
                PropChanged();
            }
        }

        public double PanelBreakdownTop
        {
            get => panelBreakdownTop;
            set
            {
                panelBreakdownTop = value;
                PropChanged();
            }
        }

        public double PanelInfoBoxLeft
        {
            get => panelInfoBoxLeft;
            set
            {
                panelInfoBoxLeft = value;
                PropChanged();
            }
        }

        public double PanelInfoBoxTop
        {
            get => panelInfoBoxTop;
            set
            {
                panelInfoBoxTop = value;
                PropChanged();
            }
        }

        public double PanelRealTimeLeft
        {
            get => panelRealTimeLeft;
            set
            {
                panelRealTimeLeft = value;
                PropChanged();
            }
        }

        public double PanelRealTimeTop
        {
            get => panelRealTimeTop;
            set
            {
                panelRealTimeTop = value;
                PropChanged();
            }
        }

        public double PanelBorrowBoxLeft
        {
            get => panelBorrowBoxLeft;
            set
            {
                panelBorrowBoxLeft = value;
                PropChanged();
            }
        }

        public double PanelBorrowBoxTop
        {
            get => panelBorrowBoxTop;
            set
            {
                panelBorrowBoxTop = value;
                PropChanged();
            }
        }

        public double PanelMarginInfoLeft
        {
            get => panelMarginInfoLeft;
            set
            {
                panelMarginInfoLeft = value;
                PropChanged();
            }
        }

        public double PanelMarginInfoTop
        {
            get => panelMarginInfoTop;
            set
            {
                panelMarginInfoTop = value;
                PropChanged();
            }
        }

        public void OrderSettingsOnTabChanged(object sender, EventArgs args)
        {
            if (Static.CurrentlySelectedSymbolTab == SelectedTab.Settle)
            {
                OrderSettingsVisibility = false;
                return;
            }

            OrderSettingsVisibility = true;
        }

        public void OrderSettingsToggle(object o)
        {
            OrderSettingsWidthFrom = (OrderSettingsWidthFrom == ORDER_SETTINGS_WIDTH_DEFAULT ? 0 : ORDER_SETTINGS_WIDTH_DEFAULT);
            OrderSettingsWidthTo = (OrderSettingsWidthTo == ORDER_SETTINGS_WIDTH_DEFAULT ? 0 : ORDER_SETTINGS_WIDTH_DEFAULT);
        }

        public Task AdjustWidthAsync(TradingMode currentMode)
        {
            if (currentMode == TradingMode.Spot)
            {
                InterestWidth = 0;
                return Task.CompletedTask;
            }

            InterestWidth = INTEREST_WIDTH_DEFAULT;

            return Task.CompletedTask;
        }
    }
}
