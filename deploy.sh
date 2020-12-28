#!/bin/sh



docker build -t alexgrebennikov/t-shirt_backend .

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push alexgrebennikov/t-shirt_backend:latest

ssh root@165.227.158.125

docker stop t-shirt_api_1
docker run -d --name t-shirt_api_1 -p 5000:80 --restart unless-stopped alexgrebennikov/t-shirt_backend
docker system prune -a -f