#!/bin/bash
echo=========================================================
echo Release,build and push
dotnet publish -c Release --output ./publish
docker build -t juancoredocker/img-account .
