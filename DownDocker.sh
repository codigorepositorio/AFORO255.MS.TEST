#!/bin/bash
echo=========================================================
echo Remove files and container,images
echo=========================================================
rm -rf publish/
docker rmi -t juancoredocker/img-account .
