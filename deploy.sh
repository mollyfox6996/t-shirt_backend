#!/bin/sh



docker build -t alexgrebennikov/t-shirt_backend .

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push alexgrebennikov/t-shirt_backend:latest

