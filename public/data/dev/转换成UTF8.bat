AnsiToUtf8.exe client *.csv
AnsiToUtf8.exe server *.csv

xcopy server\*.csv D:\Unity\Projects\AnyGame\program\platform\android\dev\AnyGame_vs\Server\TradeAge.Server.Game\bin\ConfigData /y /s /q

pause