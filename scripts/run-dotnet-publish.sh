#!/bin/bash

# Move to the root directory
cd .. 
cd ..

export ASPNETCORE_ENVIRONMENT=local
PUBLISH=./scripts/dotnet-publish.sh
REPOSITORY=LNX-S3.ApiGateway

	 echo ========================================================
	 echo Publishing a project: $REPOSITORY
	 echo ========================================================

     cd $REPOSITORY
     $PUBLISH

# Beep to indicate successful compeletion
echo -ne '\007'