using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Zx.Core
{
// Take a set of actions and create an InputDevice for it that has a control
// for each of the actions. Also binds the actions to that those controls.
    public static class MockDevice
    {
        public static void Keyboard(Key key, bool value)
        {
            var mockInput = UnityEngine.InputSystem.Keyboard.current[key];
            using (StateEvent.From(mockInput.device, out var eventPtr))
            {
                eventPtr.time = InputState.currentTime;
                mockInput.WriteValueIntoEvent(value ? 1f : 0f, eventPtr);

                InputSystem.QueueEvent(eventPtr);
            }
        }
    }
}