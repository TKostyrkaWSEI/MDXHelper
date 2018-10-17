using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

using MDXHelperData.Data;

namespace MDXHelperApp
{
    public interface IProcessor
    {
        void SetConfig(ProcessorInput procInpt);
        void LoadCubeObjects();
        void SplitScript();
    }
}
