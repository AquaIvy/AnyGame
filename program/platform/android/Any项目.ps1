$unity="D:\Unity\001_AnyGame\program\platform\android\dev\AnyGame\Assets\Loader.unity"
$vs="D:\Unity\001_AnyGame\program\platform\android\dev\AnyGame_vs\AnyGame_vs.sln"

if(Test-Path $unity)
{
    Start-Process $unity
    Start-Sleep -m 500
}

if(Test-Path $vs)
{
    Start-Process $vs
    Start-Sleep -m 500
}


