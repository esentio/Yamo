@echo off

set /p key="Enter API key: "

dotnet nuget push "../../Setup/Yamo.0.7.0.nupkg" -k %key% -s https://api.nuget.org/v3/index.json
dotnet nuget push "../../Setup/Yamo.SQLite.0.7.0.nupkg" -k %key% -s https://api.nuget.org/v3/index.json
dotnet nuget push "../../Setup/Yamo.SqlServer.0.7.0.nupkg" -k %key% -s https://api.nuget.org/v3/index.json
