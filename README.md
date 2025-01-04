# GluetunExtendarr

This project aims to provide some additional features to [gluetun](https://github.com/qdm12/gluetun) by [qdm12](qmc12), which are (by design) not included in the original project.

Currently, the following features are available:

- Automatically resolve the IP address of the VPN server to connect to and update the configuration file accordingly (this will obviously leak the IP address of this container to the DNS server)
- Shuffle 'openvpn' files, so that the VPN server to connect to is chosen randomly on each startup

## Usage

Look into the [Docker compose file](docker-compose.yml) to see how to use this project. It is recommended to make gluetun depend on this project (with healthcheck) to ensure that the IP address is resolved before gluetun starts.

Note how the config file is mounted into the GluetunExtendarr container, copied to a temporary file inside a volume, and then mounted into the gluetun container. Gluetun should not have access to the config file directly. Instead of a volume, you may as well use a bind mount to store the config file on the host.

The following environment variables are available:

| Variable Name                    | Description                                                                                 | Default Value | Valid Values                                                                                 |
| -------------------------------- | ------------------------------------------------------------------------------------------- | ------------- | -------------------------------------------------------------------------------------------- |
| `INPUT_DIR`                      | The directory where the configuration file is stored                                        | `/input`      | Any path                                                                                     |
| `OUTPUT_DIR`                     | The directory where the updated configuration file is stored                                | `/output`     | Any path                                                                                     |
| `CONFIG_NAME`                    | The name of the configuration file. Make sure to use the same name in the Gluetun container | `custom.conf` | Any string                                                                                   |
| `SERILOG__MINIMUMLEVEL__Default` | The minimum level of messages to log                                                        | `Information` | [Valid values](https://github.com/serilog/serilog/wiki/configuration-basics#Minimum%20level) |

## Contributing

TODO
