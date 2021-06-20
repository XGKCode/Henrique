#!/usr/bin/env python3

import scapy.all as scapy

import argparse


def get_arguments():
    parser = argparse.ArgumentParser()
    parser.add_argument("-a", "--ipaddress", dest="ipaddress",
                      help="The ip address of the network or host you want to ARP ping.", metavar="argument")
    options = parser.parse_args()
    return options


def scan(ip):
    arp_request = scapy.ARP(pdst=ip)  # creates an arp request with given ip
    broadcast = scapy.Ether(dst="ff:ff:ff:ff:ff:ff")  # makes a broadcast frame
    arp_request_broadcast = broadcast / arp_request  # combines arp request and broadcast frame to send on network
    (answered_list, unanswered_list) = scapy.srp(arp_request_broadcast, timeout=1)  # sends and receive our packets

    # show packets
    # arp_request.show()
    # broadcast.show()
    # arp_request_broadcast.show()
    # print(answered_list.summary())
    # print facts: print(arp_request_broadcast.summary())
    # information about available commands: scapy.ls(scapy.Ether())

    client_list = []
    for x in answered_list:  # print all answered results
        answered_dictionary = {"ip": x[1].psrc, "mac": x[1].hwsrc}
        client_list.append(answered_dictionary)
    return client_list


def show_scan(result_list):
    print("\nIP\t\t\t\tMAC Address\n--------------------------------------------------")
    for client in result_list:
        print(client["ip"] + "\t\t\t" + client["mac"])


options = get_arguments()

scan_result = scan(options.ipaddress)

show_scan(scan_result)

# scan("192.168.20.1/24")
