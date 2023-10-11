import asyncio
import websockets
import RPi.GPIO as GPIO

# Set up the GPIO pin
GPIO.setmode(GPIO.BOARD)
GPIO.setup(15, GPIO.OUT)

async def echo(websocket):
    async for message in websocket:
        # When a message is received, emit a signal to PIN 15
        GPIO.output(15, GPIO.HIGH)
        await asyncio.sleep(1)
        GPIO.output(15, GPIO.LOW)

async def main():
    async with websockets.serve(echo, "localhost", 8765):
        await asyncio.Future()  # run forever

asyncio.run(main())
