import bpy
import serial
import threading

# set up streaming from arduino
arduino_port = '/dev/tty.usbmodem14201'
baud_rate = 115200
ser = serial.Serial(arduino_port, baud_rate)

# create queue for Arduino data
data_queue = []

def serial_thread():
    global data_queue
    while True:
        # check if there is incoming data to read
        if ser.in_waiting > 0:
            # read from serial
            data = ser.readline().decode('utf-8').strip().split(',')
            # if serial expected size, then add to queue
            if len(data) == 3:
                data_queue = list(map(float, data))

def update_blender():
    # extract data from queue
    if data_queue:
        x, y, z = data_queue
        # update object
        obj = bpy.data.objects.get('Cube')
        if obj:
            obj.rotation_euler = (x, y, z)
            bpy.context.view_layer.update()
            print(f"Updated Cube rotation to: {x}, {y}, {z}")
    # check for updates every 0.5s
    return 0.5

def register():
    # create thread that runs serial_thread function
    threading.Thread(target=serial_thread, daemon=True).start()
    # concurrently update object
    bpy.app.timers.register(update_blender)

def unregister():
    bpy.app.timers.unregister(update_blender)
    ser.close()

if __name__ == "__main__":
    register()
