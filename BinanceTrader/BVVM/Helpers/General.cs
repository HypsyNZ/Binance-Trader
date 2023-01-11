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

using BTNET.BV.Abstract;
using BTNET.BVVM.Log;
using BTNET.VM.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BTNET.BVVM.Helpers
{
    internal class General : Core
    {
        private const int EXIT_REASON = 7;
        private const int AFFINITY_MASK = 24;
        private const string DEFAULT_FORMAT = "#,0.############";

        static IntPtr DefaultAffinityMask = IntPtr.Zero;

        public static decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }

        public static string StringFormat(decimal scale)
        {
            switch (scale)
            {
                case 0:
                    return DEFAULT_FORMAT;
                case 1:
                    return "#,0.0";
                case 2:
                    return "#,0.00";
                case 3:
                    return "#,0.000";
                case 4:
                    return "#,0.0000";
                case 5:
                    return "#,0.00000";
                case 6:
                    return "#,0.000000";
                case 7:
                    return "#,0.0000000";
                case 8:
                    return "#,0.00000000";
                case >= 9:
                    return "#,0.000000000";
                default: return DEFAULT_FORMAT;
            }
        }

        public static string StringFormatQuote(decimal scale)
        {
            switch (scale)
            {
                case <= 4:
                    return "#,0.0000";
                case 5:
                    return "#,0.00000";
                case 6:
                    return "#,0.000000";
                case 7:
                    return "#,0.0000000";
                case 8:
                    return "#,0.00000000";
                case >= 9:
                    return "#,0.000000000";
                default: return DEFAULT_FORMAT;
            }
        }

        /// <summary>
        /// Use the Github API to Check for Updates
        /// </summary>
        /// <returns></returns>
        public static Task<string> CheckUpTodateAsync()
        {
            try
            {
                if (SettingsVM.CheckForUpdatesIsChecked)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/HypsyNZ/Binance-Trader/releases");
                    request.UserAgent = new Random(new Random().Next()).ToString();

                    var response = request.GetResponse();
                    if (response != null)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        string readerOutput = reader.ReadToEnd();

                        IEnumerable<Tag>? deserializedString = JsonConvert.DeserializeObject<List<Tag>>(readerOutput);
                        if (deserializedString != null && deserializedString.Any())
                        {
                            Tag tagOnFirstRelease = deserializedString.FirstOrDefault();
                            if (tagOnFirstRelease != null)
                            {
                                if ("v" + MainViewModel.Version == tagOnFirstRelease.TagName)
                                {
                                    return Task.FromResult("You are using the most recent version");
                                }
                                else
                                {
                                    return Task.FromResult("Update Available: " + tagOnFirstRelease.TagName);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Task.FromResult("Checking for Updates is Disabled");
                }
            }
            catch (Exception ex)
            {
                WriteLog.Error("Error Checking Version: ", ex);
                return Task.FromResult("Error While Checking Version");
            }

            return Task.FromResult("Couldn't check for Updates, Network Error");
        }

        /// <summary>
        /// Sets Process Priority to Real Time
        /// </summary>
        /// <returns></returns>
        public static Task ProcessPriorityAsync()
        {
            try
            {
                using Process p = Process.GetCurrentProcess();
                p.PriorityClass = ProcessPriorityClass.High;
            }
            catch (Exception ex)
            {
                WriteLog.Error("Something went wrong setting Process Priority", ex);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Determines if the current user has the Administrator Role
        /// </summary>
        /// <returns>True if the user has the Administrator Role</returns>
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Exits if the specified number of instances of the program are already running
        /// </summary>
        /// <param name="processName"></param>
        public static void LimitInstances(string processName, int maxNumber)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > maxNumber)
            {
                Prompt.ShowBox("Binance Trader is Already Running [" + maxNumber + "] Instance, which is the Maximum", "Already Running", waitForReply: true, exit: true);
            }
        }
    }
}
