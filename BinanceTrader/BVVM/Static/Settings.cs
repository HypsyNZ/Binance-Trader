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

using BinanceAPI;
using BinanceAPI.ClientBase;
using BinanceAPI.ClientHosts;
using BinanceAPI.Objects;
using BTNET.BV.Abstract;
using BTNET.BV.Enum;
using BTNET.BVVM.Helpers;
using BTNET.BVVM.Identity;
using BTNET.BVVM.Identity.Structs;
using BTNET.BVVM.Log;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows;
using Json = TJson.NET.Json;

namespace BTNET.BVVM
{
    internal class Settings : Core
    {
        protected internal static volatile SecureString TEMP_CLIENT_PASSWORD = "1K-nG1d2rasCJ2SWfm6Aj+hF45A==".ToSecure(); // todo: add pop-up so password can be entered manually
        private static volatile SecureString IDENTITY_PASSWORD = "BinanceTrader.NET-H+nG1xrsaCm6APy2Udn$jhF4w5z==".ToSecure();

        internal const string KEY_SAVED = "Key Saved";
        internal const string KEY_MISSING = "API Key Missing";
        internal const string KEY_SAVED_TO_FILE = "Key Saved To File";
        internal const string KEY_LOADED_FROM_FILE = "Key Loaded From File";
        internal const string KEYS_ENTER = "Please enter your API Key and your API Secret";
        internal const string KEYS_ENCRYPTED = "Keys Encrypted";
        internal const string KEYS_ENCRYPTED_JSON = @"API Key and API Secret were saved to File [C:\BNET\Settings\Encrypted_Keys.json]";
        internal const string KEYS_NOT_VALID = "These are not valid keys";
        internal const string NOT_CONFIGURED = "API Keys Not Configured";

        private const string KEYS_ERROR_LOADING = "Error Loading Keys";
        private const string KEYS_NULL = "API Keys Were Null";
        private const string KEYS_FAILED_TO_DESERIALIZE = "Your API Keys didnt deserialize properly, You need to re-enter them.";
        private const string KEYS_INVALID = "Your API Keys are likely Invalid, You may need to enter them again or you can restart BinanceTrader and try again";

        private const string PUBLIC_KEY = "Public";
        private const string NOT_WORKING = "Not Working";
        private const string LOADING_SETTINGS = "Loading Settings";

        private const string IDENTITY_ERROR = "Identity isn't functioning properly, So Binance Trader can't start, If your hardware has changed please restart and re-enter your API Keys, Error: ";
        private const string INVALID_KEYS_UPDATE = "Your keys are Invalid, This is normal after some updates, Please reenter your keys";

        private const string HARDWARE_VALIDATED = "Hardware has been validated decrypting keys can continue";
        private const string HARDWARE_MISMATCH = "Your hardware appears to have changed and you need to re-enter your API Keys";
        private const string HARDWARE_RESET = "Hard was reset";

        private const string FEATURES_DISABLED = "Some features won't work until you enter your API Keys in the Settings";
        private const string SERVER_VALIDATED = "Server has confirmed API Keys are valid";
        private const string SERVER_CANT_VALIDATE = "The server had an error an can't validate your keys right now, Try again shortly";
        private const string EXCEPTION_CHECKING = "Problem while checking keys: ";
        private const string SOMETHING_WRONG = "There is something wrong with your keys: ";
        private const string ERROR_CODE = "| Error Code: ";
        private const string STATUS = "| Status: ";

        private const string SERVER_TIME_FAILED_TO_START_BOX = "Server Time Client failed to start in a resonable amount of time, So Binance Trader can't start";
        private const string SERVER_TIME_FAILED_TO_START = "Failed to Start Server Time Client: ";
        private const int SERVER_TIME_READY_EXPIRE_MS = 10000;
        private const int SERVER_TIME_READY_CHECK_DELAY_MS = 2;

        private const string TEST_SYMBOL = "BTCUSDT";
        private const int TEST_ORDER_QUANTITY = 1;

        private const double DEFAULT_WATCHLIST_HEIGHT = 200;
        private const double DEFAULT_ORDER_OPACITY = 0.7;
        private const int RECV_WINDOW_SECONDS = 1;

        private const int BAD_KEY = -2008;
        private const int BAD_API_KEY = 2015;
        private const int BAD_API_KEY_FORMAT = 2014;
        private const int BAD_REQUEST = 400;
        private const int NO_ERROR_ERROR = 555555;
        private const int ILLEGAL_CHARS = -1100;
        private const int INVALID_PARAM = -1130;
        private const int SYSTEM_BUSY = -3044;
        private const int UNKNOWN = -1000;
        private const int DISCONNECTED = -1001;
        private const int UNAUTHORIZED = -1002;
        private const int TOO_MANY_REQUESTS = -1003;
        private const int SERVER_BUSY = -1004;
        private const int UNEXPECTED_RESPONSE = -1006;
        private const int TIMEOUT = -1007;
        private const int SPOT_SERVER_BUSY = -1008;
        private const int UNKNOWN_COMPOSITION = -1014;
        private const int SERVICE_SHUTTING_DOWN = -1016;
        private const int UNSUPPORTED_OPERATION = -1020;
        private const int INVALID_TIMESTAMP = -1021;
        private const int INVALID_SIGNATURE = -1022;
        private const int NOT_FOUND_NOT_AUTHORIZED = -1099;

        public static bool KeysLoaded { get; set; }
        internal static bool PromptDefaultUser { get; set; }
        internal static string PromptDefaultMessage { get; set; } = string.Empty;

        public static bool LoadSettings()
        {
            Directory.CreateDirectory(App.SettingsPath);
            WriteLog.Info(LOADING_SETTINGS);

            Status hardwareStatus = UniqueIdentity.Initialize(IDENTITY_PASSWORD);
            IDENTITY_PASSWORD.Dispose();

            bool r = true;
            if (!hardwareStatus.Success)
            {
                DeleteKeys();
                Prompt.ShowBox(IDENTITY_ERROR + hardwareStatus.Message, NOT_WORKING, waitForReply: true, exit: true, shutdownOrExit: false);
            }
            else
            {
                SetupClients();
                if (TestHardware(hardwareStatus))
                {
                    if (File.Exists(App.KeyFile)) // Check if Keys exist
                    {
                        if (LoadKeys()) // Decrypt Keys
                        {
                            r = TestKeys();
                        }
                    }
                    else
                    {
                        ConfigureDefaultUser(FEATURES_DISABLED); // No Keys
                        r = false;
                    }
                }
                else
                {
                    ConfigureDefaultUser(HARDWARE_MISMATCH); // Hardware has Changed
                    r = false;
                }

                LoadUserSettings();
                SettingsVM.IsUpToDate = General.CheckUpTodateAsync().Result;
            }

            return r;
        }

        /// <summary>
        /// Setup the Default User
        /// </summary>
        /// <param name="message"></param>
        protected private static void ConfigureDefaultUser(string message)
        {
            SettingsVM.ApiKey = "";
            SettingsVM.ApiSecret = "";
            KeysLoaded = false;
            BaseClient.SetAuthentication(PUBLIC_KEY, PUBLIC_KEY);
            SettingsVM.Enable();
            PromptDefaultUser = true;
            PromptDefaultMessage = message;
        }

        protected private static void SetupClients()
        {
            var options = new BinanceClientHostOptions()
            {
                ReceiveWindow = TimeSpan.FromSeconds(RECV_WINDOW_SECONDS),
                LogPath = App.LogClient,
#if RELEASE
                LogLevel = LogLevel.Error,
#else
                LogLevel = LogLevel.Debug,
#endif
            };

            BinanceClientHost.SetDefaultOptions(options);
            BaseClient.SetAuthentication(PUBLIC_KEY, PUBLIC_KEY);
            SocketClientHost.SetDefaultOptions(new SocketClientHostOptions()
            {
                LogPath = App.LogSocket,
#if RELEASE
                LogLevel = LogLevel.Error,
#else
                LogLevel = LogLevel.Debug,
#endif
            });

            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            try
            {
                Client = new BTClient(cts.Token);
                ServerTimeClient.Start(options, cts.Token).ConfigureAwait(true);

                Stopwatch sw = Stopwatch.StartNew();
                while (!ServerTimeClient.IsReady())
                {
                    if (sw.ElapsedMilliseconds > SERVER_TIME_READY_EXPIRE_MS)
                    {
                        Prompt.ShowBox(SERVER_TIME_FAILED_TO_START, NOT_WORKING, waitForReply: true, exit: true);
                    }
                }
            }
            catch (Exception cr)
            {
                WriteLog.Error(SERVER_TIME_FAILED_TO_START + cr);
                Prompt.ShowBox(SERVER_TIME_FAILED_TO_START_BOX, NOT_WORKING, waitForReply: true, exit: true);
            }
        }

        protected private static void LoadUserSettings()
        {
            if (File.Exists(App.Settings))
            {
                SettingsObject? results = Json.Load<SettingsObject>(App.Settings);

                if (results != null)
                {
                    SettingsVM.ShowBorrowInfoIsChecked = results.ShowBorrowInfoIsChecked;
                    SettingsVM.ShowSymbolInfoIsChecked = results.ShowSymbolInfoIsChecked;
                    SettingsVM.ShowMarginInfoIsChecked = results.ShowMarginInfoIsChecked;
                    SettingsVM.ShowIsolatedInfoIsChecked = results.ShowIsolatedInfoIsChecked;
                    SettingsVM.ShowBreakDownInfoIsChecked = results.ShowBreakDownInfoIsChecked;
                    SettingsVM.TransparentTitleBarIsChecked = results.TransparentTitleIsChecked;
                    SettingsVM.DangerousButtonsIsChecked = results.DangerousButtonsIsChecked;
                    SettingsVM.CheckForUpdatesIsChecked = results.CheckForUpdates;
                    SettingsVM.AutoSaveIsChecked = results.AutoSaveSettings;
                    SettingsVM.KeepFirstOrderIsChecked = results.KeepFirstOrder;
                    SettingsVM.ShowScraperButtonIsChecked = results.ShowScraperButtonIsChecked;

                    VisibilityVM.WatchListHeight = results.WatchListHeight;

                    TradeVM.UseLimitBuyBool = results.BuyLimitChecked;
                    TradeVM.UseLimitSellBool = results.SellLimitChecked;

                    BorrowVM.BorrowBuy = results.BuyBorrowChecked;
                    BorrowVM.BorrowSell = results.SellBorrowChecked;

                    SettingsVM.DisableOpacityIsChecked = results.DisableOpacity;

                    SettingsVM.ConfigureTitleBar();
                }
            }

            if (File.Exists(App.SettingsPanels))
            {
                SettingsObjectPanels? results = Json.Load<SettingsObjectPanels>(App.SettingsPanels);
                if (results != null)
                {
                    VisibilityVM.PanelBorrowBoxLeft = results.PanelBorrowBoxLeft;
                    VisibilityVM.PanelBorrowBoxTop = results.PanelBorrowBoxTop;

                    VisibilityVM.PanelBreakdownLeft = results.PanelBreakdownLeft;
                    VisibilityVM.PanelBreakdownTop = results.PanelBreakdownTop;

                    VisibilityVM.PanelRealTimeLeft = results.PanelRealTimeLeft;
                    VisibilityVM.PanelRealTimeTop = results.PanelRealTimeTop;

                    VisibilityVM.PanelMarginInfoLeft = results.PanelMarginInfoLeft;
                    VisibilityVM.PanelMarginInfoTop = results.PanelMarginInfoTop;

                    VisibilityVM.PanelInfoBoxLeft = results.PanelInfoBoxLeft;
                    VisibilityVM.PanelInfoBoxTop = results.PanelInfoBoxTop;

                    VisibilityVM.OrderListHeight = results.OrderListHeight;

                    VisibilityVM.TradeInfoLeft = results.PanelTradeInfoleft;
                    VisibilityVM.TradeInfoTop = results.PanelTradeInfoTop;

                    VisibilityVM.ScraperLeft = results.PanelScraperLeft;
                    VisibilityVM.ScraperTop = results.PanelScraperTop;

                    VisibilityVM.InsightsPanelLeft = results.PanelInsightsLeft;
                    VisibilityVM.InsightsPanelTop = results.PanelInsightsTop;
                }
            }

            if (File.Exists(App.SettingsScraper))
            {
                SettingsObjectScraper? results = Json.Load<SettingsObjectScraper>(App.SettingsScraper);
                if (results != null)
                {
                    ScraperVM.PriceBias = results.PriceBias;
                    ScraperVM.SellPercent = results.SellPercent;
                    ScraperVM.ReverseDownPercent = results.DownPercent;
                    ScraperVM.WaitTime = results.WaitTime;
                    ScraperVM.WaitTimeCount = results.WaitCount;

                    ScraperVM.ScraperCounter.GuesserResetShortCountUp = results.ResetUpS;
                    ScraperVM.ScraperCounter.GuesserResetShortCountDown = results.ResetDownS;
                    ScraperVM.ScraperCounter.GuesserDivPercent = results.DivPercent;
                    ScraperVM.ScraperCounter.GuesserResetLongCountUp = results.ResetUpL;
                    ScraperVM.ScraperCounter.GuesserResetLongCountDown = results.ResetDownL;
                    ScraperVM.ScraperCounter.GuesserResetTime = results.ResetTime;
                    ScraperVM.ScraperCounter.GuesserResetTimeBias = results.TimeBias;
                    ScraperVM.ScraperCounter.GuesserUpBias = results.UpBias;
                    ScraperVM.ScraperCounter.GuesserDownBias = results.DownBias;
                    ScraperVM.ScraperCounter.GuesserDeadTime = results.DeadTime;

                    ScraperVM.UseLimitAdd = results.UseLimitAdd;
                    ScraperVM.UseLimitClose = results.UseLimitClose;
                    ScraperVM.UseLimitBuy = results.UseLimitBuy;
                    ScraperVM.UseLimitSell = results.UseLimitSell;
                }
            }
        }

        protected private static bool LoadKeys()
        {
            string keysRaw = File.ReadAllText(App.KeyFile);
            if (!string.IsNullOrEmpty(keysRaw))
            {
                var keysDeserialized = JsonConvert.DeserializeObject<ApiKeys>(keysRaw);
                if (keysDeserialized != null)
                {
                    // Decrypt keys and store them as a Secure String
                    BaseClient.SetAuthentication(UniqueIdentity.Decrypt(keysDeserialized.ApiKey.ToSecure(), TEMP_CLIENT_PASSWORD), UniqueIdentity.Decrypt(keysDeserialized.ApiSecret.ToSecure(), TEMP_CLIENT_PASSWORD));

                    // Ensure the keys aren't in the UI.
                    keysRaw = string.Empty;
                    keysDeserialized.ApiKey = string.Empty;
                    keysDeserialized.ApiSecret = string.Empty;
                    keysDeserialized = new();
                    SettingsVM.ApiKey = KEY_LOADED_FROM_FILE;
                    SettingsVM.ApiSecret = KEY_LOADED_FROM_FILE;
                    SettingsVM.Disable();
                    KeysLoaded = true;
                    return true;
                }
                else
                {
                    KeysLoaded = false;
                    SettingsVM.Enable();
                    WriteLog.Error(KEYS_NULL);
                    ConfigureDefaultUser(KEYS_FAILED_TO_DESERIALIZE);
                    return false;
                }
            }

            return false;
        }

        protected private static bool TestKeys()
        {
            try
            {
                var test = Client.Local.Spot.Order.PlaceTestOrderAsync(TEST_SYMBOL, BinanceAPI.Enums.OrderSide.Sell, BinanceAPI.Enums.OrderType.Market, TEST_ORDER_QUANTITY).Result;
                if (!test.Success)
                {
                    switch (test.Error.Code)
                    {
                        case NOT_FOUND_NOT_AUTHORIZED:
                        case INVALID_SIGNATURE:
                        case NO_ERROR_ERROR:
                        case BAD_REQUEST:
                        case -BAD_REQUEST:
                        case BAD_API_KEY:
                        case -BAD_API_KEY:
                        case BAD_API_KEY_FORMAT:
                        case -BAD_API_KEY_FORMAT:
                        case INVALID_PARAM:
                        case ILLEGAL_CHARS:
                        case BAD_KEY:
                        case -BAD_KEY:
                            DeleteKeys();
                            ConfigureDefaultUser(INVALID_KEYS_UPDATE);
                            return false;

                        case SYSTEM_BUSY:
                        case INVALID_TIMESTAMP:
                        case UNSUPPORTED_OPERATION:
                        case SERVICE_SHUTTING_DOWN:
                        case UNKNOWN_COMPOSITION:
                        case SPOT_SERVER_BUSY:
                        case TIMEOUT:
                        case UNEXPECTED_RESPONSE:
                        case SERVER_BUSY:
                        case TOO_MANY_REQUESTS:
                        case UNAUTHORIZED:
                        case DISCONNECTED:
                        case UNKNOWN:
                            Prompt.ShowBox(SERVER_CANT_VALIDATE, NOT_WORKING, waitForReply: true, exit: true);
                            break;

                        default:
                            ConfigureDefaultUser(SOMETHING_WRONG + test.Error.Message + ERROR_CODE + test.Error.Code + STATUS + test.ResponseStatusCode);
                            return false;
                    }
                }
                else
                {
                    WriteLog.Info(SERVER_VALIDATED); // OK
                }

                return true;
            }
            catch (Exception cr) // Hardware has Changed / Invalid Keys
            {
                WriteLog.Error(EXCEPTION_CHECKING + cr);
                ConfigureDefaultUser(KEYS_INVALID);
                return false;
            }
        }

        protected private static void DeleteKeys()
        {
            SettingsVM.ApiKey = KEYS_ERROR_LOADING;
            SettingsVM.ApiSecret = KEYS_ERROR_LOADING;

            try
            {
                if (File.Exists(App.KeyFile))
                {
                    File.Delete(App.KeyFile);
                }
            }
            catch
            {
                // Computer says no
            }

            try
            {
                if (File.Exists(App.ValidationPath))
                {
                    File.Delete(App.ValidationPath);
                }
            }
            catch
            {
                // Computer says no
            }
        }

        protected private static bool TestHardware(Status s)
        {
            if (s.ValidationString != null)
            {
                if (File.Exists(App.ValidationPath))
                {
                    ValidationString? v = Json.Load<ValidationString>(App.ValidationPath);

                    if (v != null && v.Hash != null)
                    {
                        if (v.Hash.Equals(s.ValidationString.GetSecure()))
                        {
                            WriteLog.Info(HARDWARE_VALIDATED);
                            return true;
                        }
                        else
                        {
                            if (File.Exists(App.ValidationPath))
                            {
                                File.Delete(App.ValidationPath);
                            }

                            WriteLog.Info(HARDWARE_RESET);
                            return false;
                        }
                    }
                }
                else
                {
                    Json.Save(new ValidationString(s.ValidationString.GetString()), App.ValidationPath);
                    return true;
                }
            }

            return false;
        }

        public static void OnClosing()
        {
            try
            {
                WriteLog.Info("Shutting Down..");

                foreach (Window w in Application.Current.Windows)
                {
                    w.Hide();
                    Core.MainVM.IsSymbolSelected = false;
                }

                try
                {
                    NotepadVM?.SaveNotes();
                }
                catch
                {
                    // Shutting Down
                }

                try
                {
                    WriteLog.Info("Unsubscribing Sockets..");
                    Client?.DisposeClients();
                }
                catch
                {
                    // Shutting Down
                }

                SaveSettings(false);
                Thread.Sleep(500);
                Goodbye("Exiting, Goodbye..");
            }
            catch (Exception ex)
            {
                WriteLog.Error("OnClosingError: ", ex);
            }
            finally
            {
                Thread.Sleep(500);
                Goodbye("Exiting with Exceptions, Goodbye..");
            }
        }

        public static void PromptExit()
        {
            if (Prompt.ShowBox("Are you Sure you want to Exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning, waitForReply: true) == MessageBoxResult.Yes)
            {
                NotifyIcon.TrayNotifyIcon.Dispose();
                Settings.OnClosing();
            }
        }

        public static void SaveSettings(bool quiet)
        {
            if (Static.IsStarted)
            {
                WriteLog.Info("Saving Settings..");

                Directory.CreateDirectory(App.SettingsPath);
                Directory.CreateDirectory(App.OrderPath);
                SettingsVM.SaveOnClose();

                if (!quiet)
                {
                    WriteLog.Info("Saving Alerts..");
                }

                Json.Save(AlertVM.Alerts.ToList(), App.StoredAlerts);

                if (!quiet)
                {
                    WriteLog.Info("Storing Orders..");
                }

                Static.ManageStoredOrders.SaveAll();

                if (!quiet)
                {
                    WriteLog.Info("Storing Quotes..");
                }

                try
                {
                    Quotes.AddStoredQuote();

                    Json.Save(Stored.Quotes, App.StoredQuotes);
                }
                catch (Exception ex)
                {
                    WriteLog.Error(ex);
                }

                if (!Core.MainVM.IsWatchlistStillLoading)
                {
                    if (!quiet)
                    {
                        WriteLog.Info("Saving Watchlist..");
                    }

                    WatchListVM.StoreWatchListItemsSymbols();
                }
            }
        }

        private static void Goodbye(string m)
        {
            WriteLog.Info(m);
            Environment.Exit(0);
        }
    }
}
