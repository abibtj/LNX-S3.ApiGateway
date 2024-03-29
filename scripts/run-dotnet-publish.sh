#!/bin/bash

# Move to the root directory
cd .. 
cd ..

export ASPNETCORE_ENVIRONMENT=local
PUBLISH=./scripts/dotnet-publish.sh
BUILD=./scripts/docker-build-local.sh
REPOSITORY=LNX-S3.ApiGateway

	cd $REPOSITORY

	 echo ========================================================
	 echo Publishing a project: $REPOSITORY
	 echo ========================================================

     	$PUBLISH

	 echo ""
	 echo ""

 	 echo ========================================================
	 echo Building a local Docker image: $REPOSITORY
	 echo ========================================================

	$BUILD

# Beep to indicate successful compeletion
echo -ne '\007'