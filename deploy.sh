#!/bin/sh



docker build -t alexgrebennikov/t-shirt_backend .

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push alexgrebennikov/t-shirt_backend:latest




ssh -i ./deployf_rsa root@165.227.158.125 <<EOF

    docker stop api
    docker rm api 
    docker restart db
    docker run -d --name api -p 5000:80 --restart always alexgrebennikov/t-shirt_backend
    docker system prune -a -f
EOF

