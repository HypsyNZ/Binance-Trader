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
using BTNET.BVVM.BT;
using BTNET.BVVM.Helpers;
using BTNET.BVVM.Log;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BTNET.VM.ViewModels
{
    public class TradeViewModel : Core
    {
        private const string UNEXPECTED_EXCEPTION = "Selected Tab Error: Unexpected Exception";
        private const string PROP_CHANGED_USE_LIMIT_BUY = "UseLimitBuy";
        private const string PROP_CHANGED_USE_LIMIT_SELL = "UseLimitSell";

        private decimal symbolPriceBuy;
        private decimal symbolPriceSell;
        private decimal orderQuantity;
        private bool useLimitBuy = false;
        private bool useLimitSell = false;
        private bool useLimitToggleCheckbox;

        public ICommand? UseLimitToggleCommand { get; set; }

        public ICommand? BuyCommand { get; set; }
        public ICommand? SellCommand { get; set; }
        public ICommand? SellBaseFreeCommand { get; set; }

        public ICommand? SellBaseFreeAndClearCommand { get; set; }

        public TradeRunner TradeRunner = new TradeRunner();

        public void InitializeCommands()
        {
            UseLimitToggleCommand = new DelegateCommand(UseLimitToggleBasedOnTab);

            BuyCommand = new DelegateCommand(Buy);
            SellCommand = new DelegateCommand(Sell);
            SellBaseFreeCommand = new DelegateCommand(SellBaseFree);
            SellBaseFreeAndClearCommand = new DelegateCommand(SellBaseFreeAndClear);
            TradeRunner.Start();
        }

        public bool UseLimitCheckboxToggle
        {
            get => useLimitToggleCheckbox;
            set
            {
                useLimitToggleCheckbox = value;
                PropChanged();
            }
        }

        public decimal SymbolPriceBuy
        {
            get => this.symbolPriceBuy;
            set
            {
                this.symbolPriceBuy = value;
                PropChanged();
            }
        }

        public decimal SymbolPriceSell
        {
            get => this.symbolPriceSell;
            set
            {
                this.symbolPriceSell = value;
                PropChanged();
            }
        }

        public decimal OrderQuantity
        {
            get => this.orderQuantity;
            set
            {
                this.orderQuantity = value;
                PropChanged();
            }
        }

        public bool UseLimitBuyBool
        {
            get => this.useLimitBuy;
            set
            {
                this.useLimitBuy = value;
                PropChanged();
            }
        }

        public bool UseLimitSellBool
        {
            get => this.useLimitSell;
            set
            {
                this.useLimitSell = value;
                PropChanged();
            }
        }

        public void Buy(object o)
        {
            Trade.Buy();
        }

        public void Sell(object o)
        {
            Trade.Sell();
        }

        public void SellBaseFree(object o)
        {
            _ = Task.Run(() =>
            {
                Trade.SellAllFreeBase();
            }).ConfigureAwait(false);
        }

        public void SellBaseFreeAndClear(object o)
        {
            _ = Task.Run(() =>
            {
                Trade.SellAllFreeBaseAndClear();
            }).ConfigureAwait(false);
        }

        public void TradeVMOnTabChanged(object sender, EventArgs args)
        {
            switch (Static.CurrentlySelectedSymbolTab)
            {
                case SelectedTab.Settle:
                    return;

                case SelectedTab.Buy:
                    UseLimitCheckboxToggle = UseLimitBuyBool;
                    break;

                case SelectedTab.Sell:
                    UseLimitCheckboxToggle = UseLimitSellBool;
                    break;

                default:
                    WriteLog.Error(UNEXPECTED_EXCEPTION);
                    break;
            }
        }

        public void UseLimitToggleBasedOnTab(object o)
        {
            switch (Static.CurrentlySelectedSymbolTab)
            {
                case SelectedTab.Settle:
                    return;

                case SelectedTab.Buy:
                    UseLimitBuyBool = !UseLimitBuyBool;
                    break;

                case SelectedTab.Sell:
                    UseLimitSellBool = !UseLimitSellBool;
                    break;
            }
        }
    }
}
