#!/bin/bash

echo "----------Setting project variables"
BASE_PATH="C:\\Rodrigo\\Projetos\\.NET\\RDSolutions"
echo $BASE_PATH
BIN_PATH="C:\\RDSolutions\\Bin\\Debug\\"
echo $BIN_PATH
SOURCE="C:\\NugetPackages\\"
echo $SOURCE
COMMOM="\\RDSolutions.Common\\RDSolutions.Common.csproj"
echo $COMMOM
REPOSITORY="\\RDSolutions.Repository\\RDSolutions.Repository.csproj"
echo $REPOSITORY
SERVICE="\\RDSolutions.Service\\RDSolutions.Service.csproj"
echo $SERVICE
COMMOM_PACK_NAME="RDSolutions.Common.1.4.0.nupkg"
COMMOM_PACK="$BIN_PATH$COMMOM_PACK_NAME"
echo $COMMOM_PACK
REPOSITORY_PACK_NAME="RDSolutions.Repository.1.4.0.nupkg"
REPOSITORY_PACK="$BIN_PATH$REPOSITORY_PACK_NAME"
echo $REPOSITORY_PACK
SERVICE_PACK_NAME="RDSolutions.Service.1.4.0.nupkg"
SERVICE_PACK="$BIN_PATH$SERVICE_PACK_NAME"
echo $SERVICE_PACK

echo "----------Running build"
dotnet build "$BASE_PATH$COMMOM"
dotnet build "$BASE_PATH$REPOSITORY"
dotnet build "$BASE_PATH$SERVICE"

echo "----------Running pack"
dotnet pack "$BASE_PATH$COMMOM"
dotnet pack "$BASE_PATH$REPOSITORY"
dotnet pack "$BASE_PATH$SERVICE"

echo "----------Adding packages to local repository"
echo "nuget add "$COMMOM_PACK" -Source "$SOURCE""
nuget add "$COMMOM_PACK" -Source "$SOURCE"
echo "nuget add "$REPOSITORY_PACK" -Source "$SOURCE""
nuget add "$REPOSITORY_PACK" -Source "$SOURCE"
echo "nuget add "$SERVICE_PACK" -Source "$SOURCE""
nuget add "$SERVICE_PACK" -Source "$SOURCE"

echo "----------Process finished"
read 