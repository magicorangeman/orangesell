using System;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('+', b => SafeIncByteMemory(b));
            vm.RegisterCommand('-', b => SafeDecByteMemory(b));
            vm.RegisterCommand('>', b => SafeShiftMemoryPointer(b, true));
            vm.RegisterCommand('<', b => SafeShiftMemoryPointer(b, false));
            vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = Convert.ToByte(read()));

            foreach (char c in "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890")
                vm.RegisterCommand(c, b => b.Memory[b.MemoryPointer] = (byte)c);
        }

        private static void SafeIncByteMemory(IVirtualMachine vm)
        {
            var a = (int)vm.Memory[vm.MemoryPointer];
            a = (a + 1) % 256;
            vm.Memory[vm.MemoryPointer] = Convert.ToByte(a);
        }

        private static void SafeDecByteMemory(IVirtualMachine vm)
        {
            var a = (int)vm.Memory[vm.MemoryPointer];
            a = (a + 255) % 256;
            vm.Memory[vm.MemoryPointer] = Convert.ToByte(a);
        }

        private static void SafeShiftMemoryPointer(IVirtualMachine vm, bool forward)
        {
            if (forward)
                vm.MemoryPointer = (vm.MemoryPointer + 1) % vm.Memory.Length;
            else
                vm.MemoryPointer = (vm.MemoryPointer + vm.Memory.Length - 1) % vm.Memory.Length;
        }
    }
}