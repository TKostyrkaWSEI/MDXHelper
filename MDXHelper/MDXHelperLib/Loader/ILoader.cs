using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MDXHelperApp
{
    public interface ILoader
    {
        LoaderOutput GetLoaderOutput(BaseConfig config);
    }
}
