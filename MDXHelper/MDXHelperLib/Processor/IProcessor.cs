namespace MDXHelperApp
{
    public interface IProcessor
    {
        void SetConfig(ProcessorInput procInpt);
        void LoadCubeObjects();
        void SplitScript();
    }
}
