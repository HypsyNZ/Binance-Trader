### Version 2.6.0.8
- [x] All Orders placed by the Scraper are now `Fill or Kill Limit Orders`
- [x] `Sell Percent` is now the `Minimum PnL` allowed
- [x] `Reverse Percent` is now the `Minimum PnL` allowed
- [x] Orders by the `Scraper` will fail if slippage occurs and it will return to the previous `Mode`
- [x] You can now `Switch` when the Buy/Sell orders are backwards

### Version 2.6.0.7
- [x] You can no longer manually close unprofitable trades with the `Scraper`
- [x] The `Scraper` will avoid slippage when you click `Close`
- [x] Make sure the user can't click `Close` faster than possible
- [x] Optimizations

### Version 2.6.0.6
- [x] Fix a UI bug

### Version 2.6.0.5
- [x] Fix a not bug that caused `Price Bias` to trigger

### Version 2.6.0.4
- [x] The `Scraper` can now be started with `Filled` limit orders
- [x] The `Scraper` will now place `FOK Limit Orders` when selling instead of `Market Orders`

### Version 2.6.0.3
- [x] Improvements to `Scraper Counter`
- [x] Rework the `Scraper UI`

### Version 2.6.0.2
- [x] Add `Dead Time` to `Scraper`
- [x] `Dead Time` is the amount of events that passed without the price changing in either direction
- [x] Save `Guesser` settings with the rest of the `Scraper` settings

### Version 2.6.0.1
- [x] You can now change the `Guesser` settings
- [x] New Module: `Trade Queue`
- [x] Manually placed orders are now slightly faster
- [x] Orders placed by the `Scraper` are now slightly faster
- [x] Fix a bug that freezed the `UI` sometimes when you Stop the Scraper
- [x] Fix a bug that freezed the `UI` sometimes when you click SellAll/Clear
- [x] Refactor some things to make them faster
- [x] Don't check `Ready` after `Ready` is triggered
- [x] Don't check `15MinuteReady` after `15MinuteReady` is triggered
- [x] Use less `CPU` for the `ScraperCounter`
- [x] Remove some excess Property Change updates
- [x] Fix a bug that caused `15Minute Ready` for `Insights` not to reset if you change symbols
- [x] Fix a bug that prevented `Sell All and Clear` from working if you have dust

### Version 2.5.1.4
- [x] Rework `Scraper`
- [x] Dispose `Semaphores` inbetween sessions
- [x] Rename `Diff2` to `Speed`
- [x] Remove `Diff1` from `Trade Information`
- [x] Split `PeriodTick` and `TradeTick`

### Version 2.5.1.1
- [x] Move `Insights` to its own `Panel`
- [x] Add `5 Second` time frame to `Trade Information Panel`
- [x] Add `1 Second` time frame to `Trade Information Panel`
- [x] Auto save `Insights Panel` location

### Version 2.5.1.0
- [x] Fix a bug that kept the `Guesser` counter running (this was just a UI bug)

### Version 2.5.0.9
- [x] Change some hard coded defaults in the `Scraper` (settings coming for these later)

### Version 2.5.0.8
- [x] Fixed a bug that can cause the `Scraper` to Stop (You can just restart it)
- [x] Increase `PnL` update speed

### Version 2.5.0.6
- [x] The Scraper won't stop on failure, It will try again or switch back to the previous mode.
- [x] Remove an unused event
- [x] Fixed a bug that can cause the `Scraper` to Stop (You can just restart it)
- [x] Prevent a potential `UI` bug from happening
- [x] Change how the `Up`/`Down` price is calculated
- [x] `Up`/`Down` now changes based on the current `Mode`
- [x] `Buy Price` is now zero in `Waiting Mode`
- [x] `Wait Price` is now zero in `Watching Mode`

### Version 2.5.0.5
- [x] Fix a bug that caused excess RAM usage

### Version 2.5.0.4
- [x] Fix a context issue

### Version 2.5.0.2
- [x] Significant Improvements to `Scraper`
- [x] `Scraper` has two new modes `Guessing Buy` and `Guessing Sell`
- [x] Fix a bug that caused the `Order Detail` to freeze the `UI`.
- [x] Fix a bug that prevented the settings from saving correctly sometimes.
- [x] Fix a bug that was wasting `CPU`
- [x] Changes to how orders are stored

### Version 2.4.8.6
- [x] Various Optimizations
- [x] Placing Orders is now slightly faster

### Version 2.4.8.5
- [x] Bug fixes and version rollup

### Version 2.4.8.4
- [x] Fix a bug in the `Scraper` that shows an incorrect `Notification`

### Version 2.4.8.3
- [x] Fix a bug in the logging queue
- [x] Fix a bug in the Websocket
- [x] Fix a bug that caused redraw of some images
- [x] Fix a bug in `Trade Information` that would miss some `Orders`
- [x] `Trade Information` uses less `CPU`
- [x] Add `Insights` to `Trade Information` Panel

### Version 2.4.8.2
- [x] Add Win/Loss count to `Scraper`
- [x] Fix font sizing issue in `Scraper`
- [x] Add Checkbox to clear stats from `Scraper`

### Version 2.4.8.1
- [x] Fix the `Direction Bias` tooltip

### Version 2.4.8.0
- [x] Save settings for the `Scraper`
- [x] Change default `Step` for the `Scraper`
- [x] Change the appearance of the `Real Time Ticker`
- [x] Fix a bug in the `Price Bias`
- [x] Fix the `Direction Bias` tooltip

### Version 2.4.7.9
- [x] Fix a UI bug

### Version 2.4.7.8
- [x] Change default `Opacity`
- [x] `Panel Opacity` is also disabled if you disable `Opacity`
- [x] Remove Opacity Slider

### Version 2.4.7.7
- [x] Change default `Opacity`
- [x] `Panel Opacity` is also disabled if you disable `Opacity`
- [x] Remove Opacity Slider

### Version 2.4.7.6
- [x] The [`Scraper`](https://i.imgur.com/MMAx0lh.png) is now a `Panel` on the `Main Overlay`
- [x] You can toggle the `Scraper` in the settings

### Version 2.4.7.5
- [x] Change how the client starts

### Version 2.4.7.4
- [x] Fix a bug that would stop the `Scraper`

### Version 2.4.7.3
- [x] Make it impossible for the bot and user to place an order at the same time

### Version 2.4.7.2
- [x] Tidy up some UI calls

### Version 2.4.7.1
- [x] Made it easier to visualize what will happen with the current settings in the `Scraper`
- [x] When the `Scraper` is in `Watching Mode` the `Up %` display price will be based on the pnl sell percent
- [x] When the `Scraper` is in `Watching Mode` the `Down %` display price will be based on the pnl sell percent
- [x] This means the Up/Down display price will change when you change the sell percent
- [x] `Up %` will change to the actual order when in waiting mode (nothing has changed)
- [x] `Down %` will change to the actual order when in waiting mode (nothing has changed)

### Version 2.4.7.0
- [x] Remove `Real Time Mode` setting, There is no tangible difference.
- [x] All threads are set to `High` priority now and all `CPU` cores are used.

### Version 2.4.6.9
- [x] Fix a bug that was causing some lag

### Version 2.4.6.8
- [x] Stop the bot buying/sell at the exact moment you click Add/Close

### Version 2.4.6.7
- [x] Add running total to the `Scraper`
- [x] You can't click `Close` and `Add` at the exact same moment now
- [x] Split some work off into an Event to make the Scraper faster

### Version 2.4.6.6
- [x] Fix a bug that caused `Exchange Information` to update more frequently than it should

### Version 2.4.6.5
- [x] Fix a bug in `Wait Time Count` in the `Scraper` that caused it to trigger 1 count early

### Version 2.4.6.4
- [x] Fix a bug in `Trade Information` that happened when you change symbols too fast.
- [x] Optimizations

### Version 2.4.6.3
- [x] Improvements to `Trade Information`

### Version 2.4.6.2
- [x] All `1 Hour` to `Trade Information`

### Version 2.4.6.1
- [x] Improvements to `Trade Information` panel
- [x] Change `500ms` to `1 Minute` in `Trade Information`

### Version 2.4.6.0
- [x] Improvements to `Trade Information` panel
- [x] Fix a bug that occurred in the `Trade Information` panel if you changed symbols too fast

### Version 2.4.5.9
- [x] Improvements to `Scraper` UI
- [x] Improvments to `Trade Information`
- [x] `500ms` trade information is now shown on the `Main Overlay`
- [x] `5 Minute` trade information is now shown on the `Main Overlay`
- [x] `15 Minute` trade information is now shown on the `Main Overlay`
- [x] This feature has a warmup time of `15 minutes`

### Version 2.4.5.6
- [x] `Settle Percent` in `Order Detail` is now shown as `Price` and `Percent`

### Version 2.4.5.5
- [x] Improvements to the `Scraper`

### Version 2.4.5.4
- [x] Improvements to the Scraper
- [x] `Sell Percent` is now shown as `Price` and `Percent`
- [x] `Up Reverse %` is now shown as `Price` and `Percent`
- [x] `Down Reverse %` is now shown as `Price` and `Percent`

### Version 2.4.5.3
- [x] Fix a bug in the [`Trade Information`](https://i.imgur.com/et9iup9.png) panel (WIP)
- [x] Improvements to the Scraper

### Version 2.4.5.2
- [x] Add `Close Current` button to the Scraper
- [x] `Close Current` will Sell the current Buy order in the scraper and go into `Waiting Mode`
- [x] Fix a bug in the [`Trade Information`](https://i.imgur.com/et9iup9.png) panel (WIP)

### Version 2.4.5.0
- [x] Fix an issue that caused Price/Quantity to be formatted incorrectly on some coins
- [x] Fix an issue that prevented Interest from displaying correctly for some coins
- [x] Add more tooltip to `Order Detail`

### Version 2.4.4.8
- [x] Calculate `Trade Ticks` every 500ms
- [x] Add detailed log to `Scraper`
- [x] New Information Panel: `Trade Information`
- [x] `Trade information` is now calculated based on `Real Time` trade data
- [x] `Trade information` can be moved around like other panels.

### Version 2.4.4.7
- [x] `All Windows` no longer appear in the `Taskbar`
- [x] Fix a bug that can occur when you open/close an `Order Detail` window
- [x] Remove the `Titlebar` from the `Main Overlay`

### Version 2.4.4.6
- [x] Fix a bug that could cause some windows to freeze when they are opened/closed

### Version 2.4.4.5
- [x] Fix a bug with `Alerts` that opened the `Danger` window by accident.

### Version 2.4.4.4
- [x] Improvements to the [`Scraper`](https://i.imgur.com/Xve6OMr.png)
- [x] `Reverse Up` and `Reverse Down` are now separate settings

### Version 2.4.4.3
- [x] Fix a potential divide by zero exception
- [x] Spend less time in some locks

### Version 2.4.4.2
- [x] Fix some undesirable behavior in the Scraper

### Version 2.4.4.1
- [x] Fix a bug that could freeze the UI when stopping the Scraper
- [x] Fix a bug that could freeze the UI when clicking sellall/clear button

### Version 2.4.4.0
- [x] Add some locks
- [x] Fix a bug in the way `Orders` are loaded

### Version 2.4.3.9
- [x] Fix a rare crash that occurred if all of your orders closed rapidly in a chain using `SwitchAuto`

### Version 2.4.3.8
- [x] Make sure you can't break the scraper with a Limit order
- [x] You can hide any limit orders you place with the delete key
- [x] If you hide a limit order with the delete key it won't be cancelled

### Version 2.4.3.7
- [x] You can now start the `Scraper` on a `Sell` order
- [x] `Scraper` will go into `Waiting Mode` if you start on a `Sell` order

### Version 2.4.3.6
- [x] Add new checkbox to Scraper: `Switch Auto`
- [x] If switch auto is checked the Scraper will switch to the next buy automatically
- [x] Add new checkbox to Scraper: `Reverse Both`
- [x] If reverse both is checked the Scraper will buy if the price goes either direction
- [x] You can only use one of these at a time
- [x] You can change `Switch Auto` and `Reverse Both` at any time

### Version 2.4.3.5
- [x] Fix a bug that caused `Orders` from the previous symbol to stay on the `Order List`

### Version 2.4.3.4
- [x] Add `Reverse Percentage` to the [`Scraper`](https://i.imgur.com/9RurFYT.png)
- [x] Add Buttons to change the `Wait Count` and `Reverse Percentage` to the `Scraper`
- [x] Increased the size of the status text on the `Scraper`
- [x] `Wait Time` is now in `Seconds`
- [x] The entire `Wait Time` must pass successfully to place based on time instead of 50%
- [x] Order will be placed regardless if the `Wait Count` is reached, same as before.

### Version 2.4.3.2
- [x] `Dangerous Buttons` now has its own window
- [x] The button to open the `Danger` window will only appear on the `Breakdown` when you have available base asset
- [x] Fix a rare crash that happened when using the `Scraper` and using the `Order Detail` for that order
- [x] Fix a bug that froze the UI when using the `Danger` buttons
- [x] Fix a bug that occurred if you use the `Danger` buttons while the `Scraper` was running
- [x] `Scraper` will stop automatically if you click `Danger` buttons

### Version 2.4.3.1
- [x] Add Button `Switch` to the `Scraper`
- [x] `Switch` will change to the next `Buy` on the `Order List` while in `Waiting Mode`

### Version 2.4.3.0
- [x] You can now `Add` while the `Scraper` is waiting to buy without stopping
- [x] The `Sell` will be automatically hidden

### Version 2.4.2.9
- [x] Scraper: An indication is now shown on the `Order List` for `Orders` that were purchased by the `Scraper`
- [x] This update isn't retroactive only new orders will show the status

### Version 2.4.2.8
- [x] Add some safety to the `Add` button in the `Scraper`

### Version 2.4.2.7
- [x] Changes to how orders are stored
- [x] Improvements to `Scraper`

### Version 2.4.2.6
- [x] Changes to `Real Time Ticker`

### Version 2.4.2.5
- [x] Refactor `Scraper` for future update

### Version 2.4.2.4
- [x] Refactor `Scraper` for future update
- [x] `Scraper` is now `Event Driven`
- [x] Add `EventHandlers` to `Scraper`

### Version 2.4.2.3
- [x] New Setting: `Show Scraper Button` on the Breakdown UI

### Version 2.4.2.2
- [x] Prevent the `Scraper` from being opened for symbols that don't currently support it.
- [x] `Scraper` can currently only be used on the `No Fees` pairs

### Version 2.4.2.1
- [x] Scraper: An indication is now shown on the `Order List` on the order that is being `Watched`
- [x] Scraper: An indication now appears on `Orders` that were purchased by the `Scraper`
- [x] Fix a bug that could occur when copying an `Order` to `Clipboard`
- [x] Fix a bug that could occur when deleting an `Order` from the `Order List`

### Version 2.4.2.0
- [x] Improvements to `Scraper`
- [x] Add `Status` text to the `Scraper`
- [x] Change default scrape percent
- [x] Change default wait time count

### Version 2.4.1.9
- [x] Improvements to `Scraper`
- [x] Add `Status` text to the `Scraper`

### Version 2.4.1.8
- [x] Improvements to `Scraper`
- [x] Fixes a random lag that occurred sometimes when you Start/Stop the `Scraper`
- [x] Fixes a bug cause by a lock that froze the UI.
- [x] Fix a bug in `Symbol Ticker` that happened when you change `Modes` and then change `Symbols`

### Version 2.4.1.7
- [x] Add `Precision Timer` which is a high resolution/precision `Multimedia Timer`
- [x] Update the `Scraper` so its just as fast but uses less CPU while in waiting mode

### Version 2.4.1.5
- [x] Fix a bug with `Wait Time` that caused it to take much longer
- [x] `Watchlist` items will now show "Waiting" until the ticker updates the first time
- [x] `UI Update` for the `Scraper`
- [x] The `Watchlist` will now format the price correctly based on the symbol
- [x] The `Watchlist` will now remember its size between sessions
- [x] This update fixes the price periodically flashing in the `Watchlist`
- [x] Decrease the chance that the `Scraper` will experience slippage in waiting mode
- [x] UI will load faster when you change symbols
- [x] New Setting:  `Enable Dangerous Buttons` will enable the `Sell All` (S) and `Sell All and Clear` (C) buttons

### Version 2.4.0.4
- [x]  Add Buttons to Increase/Decrease the wait time in the scraper without stopping it.
- [x]  Add `Tooltips` to explain some more features

### Version 2.4.0.3
- [x] New Setting: `Enable Dangerous Buttons`
- [x] `Enable Dangerous Buttons` is disabled by default
- [x] Ticking `Enable Dangerous Buttons` will enable the `Sell All` (S) and `Sell All and Clear` (C) buttons
- [x] Add `Tooltip` to the `Scraper` button

### Version 2.4.0.2
- [x] Update `Check for Updates` URL

### Version 2.4.0.1
- [x] Add extra tooltips to explain some features
