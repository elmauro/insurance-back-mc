dotnet sonarscanner begin /k:account /name:account-core /version:1.0
dotnet build PCO.AccountCore/PCO.AccountCore.sln
dotnet sonarscanner end