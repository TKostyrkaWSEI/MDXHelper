using System.Collections.Generic;

namespace MDXHelperApp
{
    public interface IDivider
    {
        List<string> Divide(    string str,
                                List<BreakSignPair> breakPairs,
                                char divideSign
                                );

        List<string> Divide(    string str,
                                List<BreakSignPair> breakPairs,
                                char divideSign,
                                List<BreakSignPair> bracketPairs
                                );
    }
}
