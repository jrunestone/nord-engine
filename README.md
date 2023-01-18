# Configure
`sudo docker build -t dev/nord .`

`sudo docker run --name nord --rm -v ~/src/nord-engine:/app jr/nord /bin/bash -c "meson setup build"`

# Build
`sudo docker run --name nord --rm -v ~/src/nord-engine:/app jr/nord /bin/bash -c "meson setup build && meson compile -C build"`