'''
NOTE - Python 3.10, Blender v3.6
'''

import bpy
import serial

arduino_port = '/dev/tty.usbmodem14201'
baud_rate = 115200

ser = serial.Serial(arduino_port, baud_rate)


def update_arm():
    while True:
        data = ser.readline().decode('utf-8').strip().split(',')
        x, y, z = map(float, data)

        # Update arm rotation based on accelerometer data
        bpy.data.objects['Arm'].rotation_euler = (x, y, z)


def main():
    update_arm()


if __name__ == '__main__':
    main()