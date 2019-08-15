#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/S3.Services.Record
dotnet run --no-restore