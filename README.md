**<h2>Stock Market Candlestick Visualizer</h2>**
A Windows Forms application for visualizing stock market data using candlestick charts with pattern recognition and technical analysis features.
<h3>Overview</h3>
This C# .NET Framework application provides an intuitive interface for loading and analyzing stock market data from CSV files. It displays interactive candlestick charts with volume indicators, identifies common candlestick patterns, and marks significant peaks and valleys in price movements.

<h3>Core Functionality</h3>

Multi-Stock Support: Load and display multiple stock charts simultaneously in cascading windows
Interactive Candlestick Charts: Professional-grade candlestick visualization with OHLC (Open, High, Low, Close) data
Volume Analysis: Separate volume chart synchronized with price movements
Date Range Filtering: Select specific date ranges for analysis
Dynamic Date Updates: Modify date ranges on-the-fly without reloading files

<h3>Technical Analysis</h3>

Pattern Recognition: Automatically identifies candlestick patterns including:

Bullish/Bearish candles
Doji (neutral)
Hammer
Marubozu
Dragonfly Doji
Gravestone Doji


Peak & Valley Detection: Automatically marks local highs and lows with colored horizontal lines
Interactive Tooltips: Hover over any candlestick to see:

Date
Pattern type
OHLC values
Price ranges

<h3>Visual Features</h3>

Color-Coded Candles:

Smart Y-Axis Scaling: Automatically adjusts to show 98-102% of price range
Grid Lines: Major grid for price axis, clean chart area
Professional Layout: Split view with 80% for price chart, 20% for volume

<h3>Installation</h3>

Clone or download the repository
Open Project1_Stocks.csproj in Visual Studio
Restore NuGet packages if needed
Build the solution (F6)
Run the application (F5)

<h3>Usage</h3>
Loading Stock Data

Launch the application to open the input form
Select your desired date range using the date pickers
Click "Load Stock" button
Select one or more CSV files containing stock data
Charts will open in cascading windows for each selected file

CSV File Format
The application expects CSV files with the following columns:

Date
Open
High
Low
Close
Volume

File naming convention: [TICKER]-[TIMEFRAME].csv (e.g., AAPL-day.csv, MSFT-week.csv)
