FROM ubuntu:latest

RUN apt update && apt install -y \
    build-essential \
    python3 \
    python3-pip \
    python3-setuptools \
    python3-wheel

RUN pip3 install meson ninja

WORKDIR /app