import bpy
import math


def update_arm():
    obj = bpy.data.objects.get('Cube')

    if obj:
        obj.rotation_euler = (math.radians(20), math.radians(30), math.radians(60))
        bpy.context.view_layer.update()
        print('Object updated')
    else:
        print('Object not found')


class SimpleOperator(bpy.types.Operator):
    bl_idname = "object.simple_operator"
    bl_label = "Simple Object Operator"

    def execute(self, context):
        update_arm()
        return {'FINISHED'}


def register():
    bpy.utils.register_class(SimpleOperator)


def unregister():
    bpy.utils.unregister_class(SimpleOperator)


if __name__ == "__main__":
    register()
    # Test call
    bpy.ops.object.simple_operator()