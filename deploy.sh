#!/bin/sh



docker build -t alexgrebennikov/t-shirt_backend .

echo "${DOCKER_PASSWORD}" | docker login -u "${DOCKER_USERNAME}" --password-stdin
docker push alexgrebennikov/t-shirt_backend:latest

ssh -i ./deployf_rsa root@165.227.158.125 <<EOF
    cd /var/www/t-shirt
    docker stop api
    docker system prune -a -f
    docker-compose stop
    docker-compose up --build -d
    
EOF

