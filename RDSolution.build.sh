#!/bin/bash
echo "----------Setting project variables"
BASE_PATH="C:\\Rodrigo\\Projetos\\.NET\\RDSolutions"
echo $BASE_PATH
BIN_PATH="C:\\RDSolutions\\Bin\\Debug"
echo $BIN_PATH
SOURCE="C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\"
echo $SOURCE
COMMOM="\\RDSolutions.Common\\RDSolutions.Common.csproj"
echo $COMMOM
REPOSITORY="\\RDSolutions.Repository\\RDSolutions.Repository.csproj"
echo $REPOSITORY
SERVICE="\\RDSolutions.Service\\RDSolutions.Service.csproj"
echo $SERVICE

COMMOM_PACK="RDSolutions.Common.1.0.0.nupkg"
REPOSITORY_PACK="RDSolutions.Repository.1.0.0.nupkg"
SERVICE_PACK="RDSolutions.Service.1.0.0.nupkg"

echo "----------Running build"
dotnet build "$BASE_PATH$COMMOM"
dotnet build "$BASE_PATH$REPOSITORY"
dotnet build "$BASE_PATH$SERVICE"

echo "----------Running pack"
dotnet pack "$BASE_PATH$COMMOM"
dotnet pack "$BASE_PATH$REPOSITORY"
dotnet pack "$BASE_PATH$SERVICE"

echo "----------Adding packages to local repository"
cd $BIN_PATH
echo "nuget add "$COMMOM_PACK" -Source "$SOURCE""
nuget add "$BIN_PATH$COMMOM_PACK" -Source $SOURCE
echo "nuget add "$REPOSITORY_PACK" -Source "$SOURCE""
nuget add "$BIN_PATH$REPOSITORY_PACK" -Source $SOURCE
echo "nuget add "$SERVICE_PACK" -Source "$SOURCE""
nuget add "$BIN_PATH$SERVICE_PACK" -Source $SOURCE

echo "----------Process finished"
read 