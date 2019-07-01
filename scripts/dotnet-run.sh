#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/S3.ApiGateway
dotnet run --no-restore