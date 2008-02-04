using System;

using CPUx86 = Indy.IL2CPU.Assembler.X86;

namespace Indy.IL2CPU.IL.X86 {
	[OpCode(OpCodeEnum.Or)]
	public class Or: Op {
		public Or(ILReader aReader, MethodInformation aMethodInfo)
			: base(aReader, aMethodInfo) {
		}
		public override void DoAssemble() {
			new CPUx86.Pop(CPUx86.Registers.EAX);
			new CPUx86.Pop(CPUx86.Registers.EDX);
			new CPUx86.Or(CPUx86.Registers.EAX, CPUx86.Registers.EDX);
			new CPUx86.Pushd(CPUx86.Registers.EAX);
			Assembler.StackContents.Pop();
		}
	}
}