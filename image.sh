#!/bin/bash
# My script
#Windows 10 PCO Release Server

cd MC.Insurance

IMAGEID=$(docker images --format {{.ID}} insurance-back-mc);
CONTAINERS=$(docker ps -aq --filter="ancestor=$IMAGEID");
if [ ! -z "$CONTAINERS" ]; then docker stop $CONTAINERS; docker rm -f $CONTAINERS; fi;
if [ ! -z "$IMAGEID" ]; then docker rmi -f $IMAGEID; fi;
docker build -t insurance-back-mc .;

docker tag insurance-back-mc:latest 728401402215.dkr.ecr.us-east-1.amazonaws.com/insurance-back-mc:latest;

# docker run -e ASPNETCORE_ENVIRONMENT=Production --name insurance-back -p 5000:80 insurance-back-mc;
# docker run -e ASPNETCORE_ENVIRONMENT=Development --name insurance-back -p 5000:80 insurance-back-mc;

