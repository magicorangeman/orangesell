using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        private Dictionary<char, Action<IVirtualMachine>> actionList;

        public VirtualMachine(string program, int memorySize = 30000)
        {
            Instructions = program;
            InstructionPointer = 0;
            Memory = new byte[memorySize];
            MemoryPointer = 0;
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            if (actionList == null) { actionList = new Dictionary<char, Action<IVirtualMachine>>(); }
            actionList.Add(symbol, execute);
        }

        public void Run()
        {
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
                if (actionList != null)
                    if (actionList.ContainsKey(Instructions[InstructionPointer]))
                        actionList[Instructions[InstructionPointer]](this);
            }
        }
    }
}