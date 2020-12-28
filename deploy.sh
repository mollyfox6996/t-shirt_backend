#!/bin/sh


IMAGE="alexgrebennikov/t-shirt_backend"
docker build -t alexgrebennikov/t-shirt_backend .

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push alexgrebennikov/t-shirt_backend:latest

docker stop sample_api_1
docker run --restart unless-stopped -d -n sample_api_1 -p 5000:80 alexgrebennikov/t-shirt_backend
docker system prune -a -f