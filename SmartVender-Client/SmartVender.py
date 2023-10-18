import logging
import re
import time
from signalrcore.hub_connection_builder import HubConnectionBuilder
import urllib3
from requests import Session

urllib3.disable_warnings()

# Variables that change on another system
systemUrl = "https://localhost:7127/hubs/VendingHub"  # URL of the Server
vendingMachineName = "Automat Werkstatt"  # Name of the Vending-Machine

# Slot Mapping for mapping Vending-machine slot to GPIO Pins
slots = [2, 3, 4, 17, 27, 22]  # Replace with actual GPIO pins for the slots
slotPinMap = [{"slot": 1, "pin": 16},
              {"slot": 2, "pin": 17},
              {"slot": 3, "pin": 18},
              {"slot": 4, "pin": 19},
              {"slot": 5, "pin": 20},
              {"slot": 6, "pin": 21}]


def on_order_received(message):
    pattern = re.compile(r"[0-9]+_[0-9]+", re.IGNORECASE)
    if pattern.match(message):
        # Extract slot and amount values from the message
        vending_slot = message.split('_')[0]  # Replace with actual extraction logic
        amount = message.split('_')[1]  # Replace with actual extraction logic

        for i in amount:
            # wait 1 second, Trigger GPIO output based on slot and wait 1 second
            slot = slotPinMap[vending_slot]
            print("Spit out Article from slot " + slot)
            time.sleep(4)
            # GPIO.output(slot, GPIO.HIGH)
            # time.sleep(4)
            # GPIO.output(slot, GPIO.LOW)


def on_message(message):
    print(message)


def on_error(error):
    print(error)


def on_close():
    print("### closed ###")


def on_open():
    time.sleep(2)
    print("### connected ###")
    connection.send('Connect', [vendingMachineName], lambda m: print(m))
    print("--- System Running and Connected to the Server ---")


def on_connect_result(result):
    print(result)


def on_test(message):
    print("###test test test###")
    print(message)


with Session() as session:
    session.verify = False

    connection = HubConnectionBuilder() \
        .with_url(systemUrl, {'verify_ssl': False}) \
        .with_automatic_reconnect({
            "type": "raw",
            "keep_alive_interval": 10,
            "reconnect_interval": 5,
            "max_attempts": 5
        }) \
        .build()
    connection.on("OrderReceived", on_order_received)
    connection.on("Test", on_test)
    connection.on("ConnectResult", on_connect_result)

    connection.on_open(on_open)
    connection.on_close(on_close)
    connection.start()

    while True:
        time.sleep(1)
