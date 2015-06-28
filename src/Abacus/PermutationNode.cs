using System.Collections.Generic;
using WiRC.Terminator;

namespace WiRC.Abacus
{
	public class PermutationNode
	{
		public ProgramCardType? Card { get; set; }

		public IEnumerable<PermutationNode> Children { get; set; }
	}
}
