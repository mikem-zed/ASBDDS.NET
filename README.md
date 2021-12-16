# ASBDDS.NET
This system is DEMO ONLY(proof of concept).

ASBDDS.NET is a simple ARM Single Board Devices Deployment System API and WEB Interface writed in C# net5.0.
This set of software and devices is used to automate the deployment of systems on arm single board devices.

#### The system includes:
* HTTP server
* TFTP server
* DHCP server

#### The system needs:
* PostgreSQL database
* Suported POE switch

#### Suported arm single board devices:
* Raspberry pi 4 (with POE power HAT)

#### Supported POE Swithes:
* Ubiquiti US-24-250W

# We use:

## PostgreSQL database:
We use PostgreSQL as main database.

## U-boot (as Bootloader and Storage cleaner)
We use 3 different build of u-boot for each device. Each build has a different boot command.
* [Storage cleaner](https://github.com/Insei/asbdds-u-boot/blob/asbds/configs/asbds_sdcard_erase_config#L5)
* [Default](https://github.com/Insei/asbdds-u-boot/blob/asbds/configs/asbds_config#L5)
* [IPXE booting](https://github.com/Insei/asbdds-u-boot/blob/asbds/configs/asbds_provisioning_config#L5)

## HTTP
* We use HTTP API for contol this system. 
* We have a WEB interface that uses the HTTP API.

## IPXE
We use ipxe in two variants:
* Default (For OS Installation)
* For API callbacks. (As example: u-boot after erasing storage boot via ipxe and get ipxe.cfg from the API at this moment we check the device state and if state is erasing - we switch device power to off on ethernet port switch and mark device rent as closed)

## TFTP
We use TFTP server for provision bootloaders and ipxe to devices.
For each device in use we create folter in tftp root with device serial number and in this folder will be placed firmware, ipxe and bootloader files.
We switch bootloaders build's if this needed via api.

## DHCP
From DHCP devices get their IP address and get TFTP server address.

## POE Switches
We use switches as power DC for single board arm devices. We manage POE power on ports via cli.
