cd .\WebServer
dotnet publish -r win-x64 -c Release --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimMode=link -p:EnableCompressionInSingleFile=true -p:PublishReadyToRun=true
dotnet publish -r linux-x64 -c Release --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimMode=link -p:EnableCompressionInSingleFile=true -p:PublishReadyToRun=true
dotnet publish -r osx-x64 -c Release --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimMode=link -p:EnableCompressionInSingleFile=true -p:PublishReadyToRun=true
cd ..
xcopy "$PSScriptRoot\WebServer\bin\Release\net9.0\win-x64\publish" "$PSScriptRoot\Output\Windows\" /Y
xcopy "$PSScriptRoot\Game" "$PSScriptRoot\Output\Windows\Game\" /Y /S
Compress-Archive "$PSScriptRoot\Output\Windows\" "$PSScriptRoot\Output\Windows.zip" -Force
xcopy "$PSScriptRoot\WebServer\bin\Release\net9.0\linux-x64\publish" "$PSScriptRoot\Output\Linux\" /Y
xcopy "$PSScriptRoot\Game" "$PSScriptRoot\Output\Linux\Game\" /Y /S
Compress-Archive "$PSScriptRoot\Output\Linux\" "$PSScriptRoot\Output\Linux.zip" -Force
xcopy "$PSScriptRoot\WebServer\bin\Release\net9.0\osx-x64\publish" "$PSScriptRoot\Output\MacOS\" /Y
xcopy "$PSScriptRoot\Game" "$PSScriptRoot\Output\MacOS\Game\" /Y /S
Compress-Archive "$PSScriptRoot\Output\MacOS\" "$PSScriptRoot\Output\MacOS.zip" -Force