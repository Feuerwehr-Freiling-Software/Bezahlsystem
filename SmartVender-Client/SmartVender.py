import asyncio
import websockets
import RPi.GPIO as GPIO

# Set up the GPIO pin
GPIO.setmode(GPIO.BOARD)
GPIO.setup(15, GPIO.OUT)

# load all Pins from the server and add them

async def echo(websocket):
    async for message in websocket:
        # message = "SLOT_AMOUNT"
        slot = message.split('_')[0]
        amount = message.split('_')[1]
        for i in amount:
            GPIO.output(slot, GPIO.HIGH)
            await asyncio.sleep(1)
            GPIO.output(slot, GPIO.HIGH)

async def main():
    async with websockets.serve(echo, "localhost", 8765):
        await asyncio.Future()  # run forever

asyncio.run(main())
