FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app

COPY ./publish/ /app
ENV ASPNETCORE_URLS=http://+:5002
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5002

ENTRYPOINT ["dotnet", "/app/WebApplication1.dll"]


