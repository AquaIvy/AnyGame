$waittime=300

#�ĵ�Ŀ¼
Start-Process ..\..\..\plan\W�ĵ� -WindowStyle Maximized
Start-Sleep -m $waittime

#�ĵ�����Ŀ¼
Start-Process ..\..\..\public\data\dev -WindowStyle Maximized
Start-Sleep -m $waittime

#Res_Android
Start-Process .\dev\Res_Android -WindowStyle Maximized
Start-Sleep -m $waittime

#������exe
Start-Process .\dev\AnyGame_vs\Server\AnyGame.Server.Game\bin\Debug -WindowStyle Maximized
Start-Sleep -m $waittime

#ԭʼͼƬ
Start-Process  .\dev\ImageSource -WindowStyle Maximized
Start-Sleep -m $waittime


