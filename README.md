# kafka-streams

The KAFKA_ADVERTISED_LISTENERS variable is set to localhost:29092. This makes Kafka accessible from outside the container by advertising its location on the Docker host.

Also notice that KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR is set to 1. This is required when you are running with a single-node cluster. If you have three or more nodes, you can use the default.

# kafka-kraft


`docker build -t ermsystem/kafka-kraft -f Dockerfile .`
`docker run --name kafka-kraft -d -p 9092:9092 ermsystem/kafka-kraft`