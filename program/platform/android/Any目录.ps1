$waittime=300


#文档目录
Start-Process ..\..\..\plan\W文档 -WindowStyle Maximized
Start-Sleep -m $waittime

#文档导出目录
Start-Process ..\..\..\public\data\dev -WindowStyle Maximized
Start-Sleep -m $waittime

#服务器exe
Start-Process .\dev\AnyGame_vs\Server\TradeAge.Server.Game\bin\Debug -WindowStyle Maximized
Start-Sleep -m $waittime

#原始图片
Start-Process  .\dev\ImageSource -WindowStyle Maximized
Start-Sleep -m $waittime


