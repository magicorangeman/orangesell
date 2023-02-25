using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        private static Dictionary<int, int> bordersCycle;
        private static Stack<int> stack;
        private static void InitializeAndFill(IVirtualMachine vm)
        {
            bordersCycle = new Dictionary<int, int>();
            stack = new Stack<int>();
            stack.Push(vm.InstructionPointer);
            var i = vm.InstructionPointer + 1;
            while (stack.Count != 0)
            {
                if (vm.Instructions[i] == '[') stack.Push(i);
                else if (vm.Instructions[i] == ']')
                    bordersCycle.Add(stack.Pop(), i);
                i++;
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
        {
            vm.RegisterCommand('[', b =>
            {
                if (bordersCycle == null || bordersCycle.Count == 0)
                {
                    InitializeAndFill(b);
                }
                if (b.Memory[b.MemoryPointer] == 0)
                {
                    b.InstructionPointer = bordersCycle[b.InstructionPointer];
                    if (stack.Count == 0) bordersCycle.Clear();
                }
                else stack.Push(b.InstructionPointer);
            });
            vm.RegisterCommand(']', b => b.InstructionPointer = stack.Pop() - 1);
        }
    }
}