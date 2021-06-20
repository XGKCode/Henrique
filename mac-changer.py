#!/usr/bin/env python3

import subprocess

from optparse import OptionParser

import re


def get_arguments():
    parser = OptionParser()
    parser.add_option("-i", "--interface", dest="interface",
                      help="The name of the interface or network adapter you are looking to manipulate.\n"
                           "Default mac will be set to 00:11:22:33:44:55 (if no mac-address is specified)."
                      , metavar="argument")
    parser.add_option("-m", "--mac", dest="new_mac",
                      help="Specify 'new' mac-address you would like to set, You MUST specify an interface beforehand."
                      , metavar="argument")

    (options, arguments) = parser.parse_args()

    if not options.interface:
        # no error
        parser.error("[-] Please specify an interface, use --help for more info.")
    elif not options.new_mac:
        # no error
        parser.error("[-] Please specify a mac address, use --help for more info.")
    return options


def change_mac(interface, new_mac):
    if new_mac:
        print("\n[+] Changing MAC for interface: " + interface + " to " + new_mac + "\n")
        subprocess.run(["sudo", "ifconfig", interface, "down"])
        subprocess.run(["sudo", "ifconfig", interface, "hw", "ether", new_mac])
        subprocess.run(["sudo", "ifconfig", interface, "up"])
        # subprocess.run(["sudo", "ifconfig", interface])


def get_mac(interface):
    ifconfig_result = subprocess.check_output(["ifconfig", interface])
    ifconfig_search = str(ifconfig_result)
    search_output = re.search("\w\w:\w\w:\w\w:\w\w:\w\w:\w\w", ifconfig_search)

    if search_output:
        return search_output.group(0)
    else:
        print("[-] Could not read MAC Address.")

# Getting user inputs


options = get_arguments()
current_mac = get_mac(options.interface)
print("\n[*] Current MAC is: " + str(current_mac))

# Changing the mac address
change_mac(options.interface, options.new_mac)

# Displaying output for user
current_mac = get_mac(options.interface)
if current_mac == options.new_mac:
    print("[+] MAC address has been changed to :" + current_mac)
else:
    print("[-] MAC address did not change.")

