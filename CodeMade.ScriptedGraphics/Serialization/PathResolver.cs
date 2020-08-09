namespace CodeMade.ScriptedGraphics
{
    public class PathResolver : IPathResolver
    {
        private readonly string _absoluteDir;

        public PathResolver(string absoluteDir)
        {
            _absoluteDir = absoluteDir;
        }

        public string Resolve(string relative)
        {
            return System.IO.Path.Combine(_absoluteDir, relative);
        }
    }

}