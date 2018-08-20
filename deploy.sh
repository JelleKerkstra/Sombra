#!/bin/bash
images=$(docker image ls --format "{{.Repository}}" | grep -P -i "^($DC_REPOSITORY/sombra\.).*(?<!unittests)$")
for image in $images; do
	docker push $image
done
exit 0