ARG REPO=mcr.microsoft.com/dotnet/core
#Build the base image and set up the working dir
FROM $REPO/sdk:2.2 AS base
WORKDIR /source

#Copy over the project files
COPY BooksApi/*.csproj BooksApi/
COPY Models/*.csproj Models/

RUN dotnet restore BooksApi/BooksApi.csproj

#Coppy and build the project
COPY BooksApi/ BooksApi/
COPY Models/ Models/
WORKDIR /source/BooksApi
RUN dotnet build -c release --no-restore

FROM base AS publish
RUN dotnet publish -c release --no-build -o /app

# final stage/image
FROM ${REPO}/runtime:2.2
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BooksApi.dll"]