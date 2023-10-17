import re
import time
from signalr import Connection

systemUrl = "http://localhost:7127/VendingHub"
vendingMachineName = "Automat Werkstatt"

slots = [2, 3, 4, 17, 27, 22]  # Replace with actual GPIO pins for the slots
slotPinMap = [{"slot": 1, "pin": 16},
              {"slot": 2, "pin": 17},
              {"slot": 3, "pin": 18},
              {"slot": 4, "pin": 19},
              {"slot": 5, "pin": 20},
              {"slot": 6, "pin": 21}]


def on_vending_received(message):
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
    print("### connected ###")
    connection.send(vendingMachineName)


if __name__ == "__main__":
    connection = Connection(systemUrl)
    hub = connection.register_hub('VendingHub')
    hub.client.on('vendingReceived', on_vending_received)
    connection.start()
    connection.wait()
