#!/bin/sh
docker ps -a | awk '{ print $1,$2 }' | grep dockertest | awk '{print $1 }' | xargs -I {} docker rm --force {}