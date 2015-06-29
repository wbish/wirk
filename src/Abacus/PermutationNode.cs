using System.Collections.Generic;
using WiRK.Terminator;

namespace WiRK.Abacus
{
	public class PermutationNode
	{
		public ProgramCardType? Card { get; set; }

		public IEnumerable<PermutationNode> Children { get; set; }
	}
}
