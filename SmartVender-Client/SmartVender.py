import logging
import numbers
import re
import time
from signalrcore.hub_connection_builder import HubConnectionBuilder
import urllib3
import asyncio
from requests import Session
try:
    import RPi.GPIO as GPIO
except RuntimeError:
    print("Error importing RPi.GPIO!  This is probably because you need superuser privileges.  You can achieve this by using 'sudo' to run your script")

# RFID
import spidev
from mfrc522 import MFRC522

reader = MFRC522(spi_id=0, sck=2, miso=4, mosi=3, cs=1, rst=0)

username = ""

urllib3.disable_warnings()

# Variables that change on another system
systemUrl = "https://localhost:7127/hubs/VendingHub"  # URL of the Server
vendingMachineName = "Automat Werkstatt"  # Name of the Vending-Machine


# Slot Mapping for mapping Vending-machine slot to GPIO Pins
slots = [2, 3, 4, 17, 27, 22]  # Replace with actual GPIO pins for the slots
inputSlotPinMap = [{"slot": 1, "pin": 16},
                   {"slot": 2, "pin": 17},
                   {"slot": 3, "pin": 18},
                   {"slot": 4, "pin": 19},
                   {"slot": 5, "pin": 20},
                   {"slot": 6, "pin": 21}]

# Slot Mapping for mapping incoming Signals from the Vending-Machine to Slots in Db
outputSlotPinMap = [{"slot": 1, "pin": 22},
                    {"slot": 2, "pin": 23},
                    {"slot": 3, "pin": 24},
                    {"slot": 4, "pin": 25},
                    {"slot": 5, "pin": 26},
                    {"slot": 6, "pin": 27}]

# RPI
GPIO.setmode(GPIO.BOARD)

for slot, pin in inputSlotPinMap:
    GPIO.setup(pin, GPIO.IN)

for slot, pin in outputSlotPinMap:
    GPIO.setup(pin, GPIO.OUT)


async def read_rfid():
    while True:
        reader.init()
        (stat, tag_type) = reader.request(reader.REQIDL)

        if stat == reader.OK:
            (stat, uid) = reader.SelectTagSN()

            if stat == reader.OK:
                global username
                username = str(int.from_bytes(bytes(uid), "little"))
                print("CARD NAME: " + username)
            else:
                global username
                username = ""
        else:
            global username
            username = ""
        await asyncio.sleep(0.25)  # pause execution for 250ms


def on_order_received(message):
    pattern = re.compile(r"[0-9]+_[0-9]+", re.IGNORECASE)
    if pattern.match(message):
        # Extract slot and amount values from the message
        vending_slot = message.split('_')[0]  # Replace with actual extraction logic
        amount = message.split('_')[1]  # Replace with actual extraction logic

        for i in amount:
            # wait 1 second, Trigger GPIO output based on slot and wait 1 second
            slot = inputSlotPinMap[vending_slot]
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
    connection.send('Connect', [vendingMachineName], lambda m: print(m))


def on_connect_result(result):
    if result:
        print("### Connected Successfully ###")
        print("--- System Running and Connected to the Server ---")
    else:
        print("### Error when connecting to Server ###")


def on_test(message):
    print("###test test test###")
    print(message)


# INPUTS
def input_callback(pin: numbers.Number):
    slot = inputSlotPinMap[pin]
    username = ""
    # TODO: Get username from RFID
    connection.send("NewArticleOrdered", [slot, username])

    outputPin = outputSlotPinMap[slot]
    GPIO.output(outputPin, GPIO.HIGH)
    # TODO: Maybe change interval
    time.sleep(3)
    GPIO.output(outputPin, GPIO.LOW)


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

    asyncio.run(read_rfid())
    while True:
        time.sleep(1)
