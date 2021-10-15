# kafka-streams

The KAFKA_ADVERTISED_LISTENERS variable is set to localhost:29092. This makes Kafka accessible from outside the container by advertising its location on the Docker host.

Also notice that KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR is set to 1. This is required when you are running with a single-node cluster. If you have three or more nodes, you can use the default.

# kafka-kraft


`docker build -t ermsystem/kafka-kraft -f Dockerfile .`
`docker run --name kafka-kraft -d -p 9092:9092 ermsystem/kafka-kraft`

# server.properties


https://adityasridhar.com/posts/how-to-easily-install-kafka-without-zookeeper
for server 1
```
node.id=1

process.roles=broker,controller

inter.broker.listener.name=PLAINTEXT

controller.listener.names=CONTROLLER

listeners=PLAINTEXT://:9092,CONTROLLER://:19092

log.dirs=/tmp/server1/kraft-combined-logs

listener.security.protocol.map=CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT,SSL:SSL,SASL_PLAINTEXT:SASL_PLAINTEXT,SASL_SSL:SASL_SSL

controller.quorum.voters=1@localhost:19092,2@localhost:19093,3@localhost:19094
```