rmdir Output /Q /S nonemptydir
del project.lock.json
dotnet restore --no-cache
dotnet pack --output Output --configuration Release