FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY ./bin/Debug/netcoreapp3.1/publish /root/
ENV ASPNETCORE_ENVIRONMENT=
EXPOSE 80/tcp
ENTRYPOINT dotnet /root/insurance-back-mc.dll
