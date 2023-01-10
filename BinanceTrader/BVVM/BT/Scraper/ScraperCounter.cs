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

using BinanceAPI.Enums;
using BTNET.BVVM.Helpers;
using PrecisionTiming;
using System.Diagnostics;

namespace BTNET.BVVM.BT
{
    public class ScraperCounter : Core
    {
        private const string STOPWATCH_FORMAT = "mm\\:ss\\.ffff";
        private const string PROP_CHANGED_STOPWATCH = "GuesserElapsedString";

        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int THREE = 3;
        private const int FOUR = 4;
        private const int FIVE = 5;
        private const int TEN = 10;
        private const int ELEVEN = 11;
        private const int FIFTY = 50;
        private const int ONE_HUNDRED = 100;
        private const int TWO_HUNDRED_FIFTY = 250;

        private decimal guesserDiv = 0;
        private decimal guesserUpCount = 10;
        private decimal guesserDownCount = 11;

        private decimal guesserResetShortCountUp = 1250;
        private decimal guesserResetShortCountDown = 1250;
        private decimal guesserResetLongCountUp = 2500;
        private decimal guesserResetLongCountDown = 2500;
        private decimal guesserResetTime = 15;
        private decimal guesserResetTimeBias = 5;
        private decimal divPercent = 30;
        private decimal guesserUpBias = 1.7m;
        private decimal guesserDownBias = 1.7m;

        protected private PrecisionTimer CounterTimer = new PrecisionTimer();

        public Stopwatch GuesserStopwatch = new Stopwatch();
        private decimal guesserDeadCount = 0;
        private decimal guesserDeadTime = 100;

        public OrderSide CounterDirection { get; set; }

        public long LastGueserBiasUpdate { get; set; }

        public decimal GuesserLastPriceAvg { get; set; }
        public int GuessNewHighCount { get; set; }
        public int GuessNewHightCountTwo { get; set; }
        public int GuessNewLowCount { get; set; }
        public int GuessNewLowCountTwo { get; set; }
        public decimal GuesserThresholdValue { get; set; }
        public int GuesserBias { get; set; } = 1;

        public string GuesserElapsedString
        {
            get => GuesserStopwatch.Elapsed.ToString(STOPWATCH_FORMAT);
        }

        public decimal GuesserUpCount
        {
            get => guesserUpCount;
            set
            {
                guesserUpCount = value;
                PropChanged();
            }
        }

        public decimal GuesserDownCount
        {
            get => guesserDownCount;
            set
            {
                guesserDownCount = value;
                PropChanged();
            }
        }

        public decimal GuesserDiv
        {
            get => guesserDiv;
            set
            {
                guesserDiv = value;
                PropChanged();
            }
        }

        public decimal GuesserResetShortCountUp
        {
            get => guesserResetShortCountUp;
            set
            {
                guesserResetShortCountUp = value;
                PropChanged();
            }
        }

        public decimal GuesserResetShortCountDown
        {
            get => guesserResetShortCountDown;
            set
            {
                guesserResetShortCountDown = value;
                PropChanged();
            }
        }

        public decimal GuesserResetLongCountUp
        {
            get => guesserResetLongCountUp;
            set
            {
                guesserResetLongCountUp = value;
                PropChanged();
            }
        }

        public decimal GuesserResetLongCountDown
        {
            get => guesserResetLongCountDown;
            set
            {
                guesserResetLongCountDown = value;
                PropChanged();
            }
        }

        public decimal GuesserResetTime
        {
            get => guesserResetTime;
            set
            {
                guesserResetTime = value;
                PropChanged();
            }
        }

        public decimal GuesserResetTimeBias
        {
            get => guesserResetTimeBias;
            set
            {
                guesserResetTimeBias = value;
                PropChanged();
            }
        }

        public decimal GuesserDivPercent
        {
            get => divPercent;
            set
            {
                divPercent = value;
                PropChanged();
            }
        }

        public decimal GuesserUpBias
        {
            get => guesserUpBias;
            set
            {
                guesserUpBias = value;
                PropChanged();
            }
        }

        public decimal GuesserDownBias
        {
            get => guesserDownBias;
            set
            {
                guesserDownBias = value;
                PropChanged();
            }
        }

        public void ResetGuesserStopwatch()
        {
            ResetCounter();
            GuesserBias = 1;
            GuesserStopwatch.Reset();
        }

        public void RestartGuesserStopwatch()
        {
            ResetCounter();
            GuesserStopwatch.Restart();
        }

        public void ResetCounter()
        {
            GuessNewHighCount = ZERO;
            GuessNewHightCountTwo = ZERO;
            GuessNewLowCount = ZERO;
            GuessNewLowCountTwo = ZERO;
            GuesserThresholdValue = ZERO;

            InvokeUI.CheckAccess(() =>
            {
                GuesserDeadCount = ZERO;
                GuesserUpCount = GuesserBias * TEN;
                GuesserDownCount = GuesserBias * ELEVEN;
                GuesserDiv = ZERO;
            });
        }

        public bool IsRunning => CounterTimer.IsRunning();

        public void Stop()
        {
            if (CounterTimer.Stop())
            {
                CounterTimer.SetAction(null!);
            }
        }

        public void CheckStart(OrderSide direction)
        {
            if (!IsRunning)
            {
                Start(direction);
            }
            else
            {
                ChangeSide(direction);
            }
        }

        public void ChangeSide(OrderSide direction)
        {
            CounterDirection = direction;
        }

        public decimal Count(decimal price, decimal last)
        {
            if (price > last)
            {
                last = price;
                GuesserUpCount++;
            }
            else if (price < last)
            {
                last = price;
                GuesserDownCount++;
            }
            else
            {
                GuesserDeadCount++;
            }

            return last;
        }

        public decimal CountAvg(decimal price, decimal last)
        {
            if (price > last)
            {
                last = price;
                GuesserUpCount++;
            }
            else if (price < last)
            {
                last = price;
                GuesserDownCount++;
            }

            return last;
        }

        public decimal GuesserDeadTime
        {
            get => guesserDeadTime;
            set
            {
                guesserDeadTime = value;
                PropChanged();
            }
        }

        public decimal GuesserDeadCount
        {
            get => guesserDeadCount;
            set
            {
                guesserDeadCount = value;
                PropChanged();
            }
        }

        public void Start(OrderSide direction)
        {
            CounterDirection = direction;
            ResetCounter();
            GuesserLastPriceAvg = MarketVM.AverageOneSecond; // Seed Value
            ScraperVM.GuesserLastPriceTicker = MarketVM.AverageOneSecond; // Seed Value
            CounterTimer.SetInterval(() =>
            {
                bool direction = CounterDirection == OrderSide.Sell ? false : true;
                var ready = MarketVM.Insights.Ready;
                var ready15 = MarketVM.Insights.Ready15Minutes;

                GuesserLastPriceAvg = CountAvg(MarketVM.AverageOneSecond, GuesserLastPriceAvg);

                if (direction)
                {
                    //Count(RealTimeVM.AskPrice);
                    var zero = GuesserDownCount - GuesserUpCount;
                    if (zero != ZERO)
                    {
                        var div = decimal.Round((zero / GuesserDownCount) * ONE_HUNDRED, FOUR);
                        InvokeUI.CheckAccess(() =>
                        {
                            GuesserDiv = div;
                        });
                    }

                    if (MarketVM.Insights.NewHighOneSecond)
                    {
                        GuesserUpCount++;
                        GuessNewHighCount++;
                        GuessNewHightCountTwo++;
                    }

                    if (MarketVM.Insights.NewLowOneSecond)
                    {
                        GuesserDownCount = GuesserDownCount + GuesserDownBias;
                    }

                    if (ready15)
                    {
                        if (MarketVM.Insights.NewHighFive && MarketVM.Insights.NewHighFifteen)
                        {
                            GuesserUpCount++;
                            GuessNewHighCount++;
                        }
                    }

                    if (ready)
                    {
                        if (MarketVM.Insights.NewHighHour || MarketVM.Insights.NewHigh)
                        {
                            GuesserUpCount++;
                            GuessNewHightCountTwo++;
                        }
                    }
                }
                else
                {
                    //Count(RealTimeVM.BidPrice);

                    var zero = GuesserUpCount - GuesserDownCount;
                    if (zero != ZERO)
                    {
                        var div = decimal.Round(zero / GuesserUpCount * ONE_HUNDRED, FOUR);
                        InvokeUI.CheckAccess(() =>
                        {
                            GuesserDiv = div;
                        });
                    }

                    if (MarketVM.Insights.NewLowOneSecond)
                    {
                        GuesserDownCount++;
                        GuessNewLowCount++;
                        GuessNewLowCountTwo++;
                    }

                    if (MarketVM.Insights.NewHighOneSecond)
                    {
                        GuesserUpCount = GuesserUpCount + GuesserUpBias;
                    }

                    if (ready15)
                    {
                        if (MarketVM.Insights.NewLowFive && MarketVM.Insights.NewLowFifteen)
                        {
                            GuesserDownCount++;
                            GuessNewLowCount++;
                        }
                    }

                    if (ready)
                    {
                        if (MarketVM.Insights.NewLowHour || MarketVM.Insights.NewLow)
                        {
                            GuesserDownCount++;
                            GuessNewLowCountTwo++;
                        }
                    }
                }

                var elapsed = GuesserStopwatch.Elapsed;
                if (GuesserDiv > GuesserDivPercent && (GuesserDownCount > GuesserResetShortCountDown || GuesserUpCount > GuesserResetShortCountUp))
                {
                    RestartGuesserStopwatch();
                }
                else if (GuesserDiv > -GuesserResetTimeBias && GuesserDiv < GuesserResetTimeBias && elapsed.TotalSeconds >= (double)GuesserResetTime)
                {
                    GuesserBias = FIVE;
                    RestartGuesserStopwatch();
                }
                else if (GuesserUpCount > GuesserResetLongCountUp || GuesserDownCount > GuesserResetLongCountDown)
                {
                    RestartGuesserStopwatch();
                }

                if ((elapsed.TotalSeconds >= (double)GuesserResetTime) && GuesserDiv > GuesserResetTimeBias)
                {
                    if (GuesserDiv > GuesserThresholdValue)
                    {
                        GuesserThresholdValue = GuesserDiv;
                    }
                    else
                    {
                        if (GuesserDiv < (GuesserThresholdValue - FIVE))
                        {
                            RestartGuesserStopwatch();
                        }
                    }
                }

                if (GuesserDeadCount >= GuesserDeadTime)
                {
                    GuesserBias = TWO;
                    RestartGuesserStopwatch();
                }

                if (elapsed.Ticks >= LastGueserBiasUpdate + TWO_HUNDRED_FIFTY)
                {
                    LastGueserBiasUpdate = elapsed.Ticks;

                    GuesserBias++;

                    if (GuesserBias > FIFTY)
                    {
                        GuesserBias = THREE;
                    }
                }

                PropChanged(PROP_CHANGED_STOPWATCH);
            }, FOUR, resolution: ONE);
        }
    }
}
