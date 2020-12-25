#!/bin/sh
set -e

IMAGE="alexgrebennikov/t-shirt_backend"
docker build -t "${IMAGE}":latest

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push "${IMAGE}":latest