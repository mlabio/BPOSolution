# Sample contents of Dockerfile
# Stage 1
 FROM microsoft/aspnetcore-build AS builder
 WORKDIR /source

 # caches restore result by copying csproj file separately
 #COPY ./Portfolio/BPOSolution.csproj .
 

 # copies the rest of your code
 COPY ./Portfolio/ .
 RUN dotnet restore
 RUN dotnet publish --output /app/ --configuration Release

 # Stage 2
 #FROM microsoft/aspnetcore
 WORKDIR /app
 #COPY --from=builder /app .
 ENTRYPOINT ["dotnet", "BPOSolution.dll"]