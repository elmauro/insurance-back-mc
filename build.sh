dotnet build MC.Insurance/MC.Insurance.Back.sln
dotnet publish MC.Insurance/MC.Insurance.Back.sln
cp -r MC.Insurance/bin/Debug/netcoreapp3.1/publish/* /c/inetpub/wwwroot/publish/insurance-back/