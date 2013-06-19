namespace Blog.Core.Infrastructure
{
    public class EngineContext
    {
        private static IEngine _engine = null;
        
        public static IEngine Current
        {
            get
            {
                if (_engine == null)
                    Initialize(false);

                return _engine;
            }
        }

        public static IEngine Initialize(bool forceRecreate)
        {
            if (_engine == null || forceRecreate)
            {
                _engine = new BlogEngine();
                _engine.Initialize();
            }

            return _engine;
        }
    }
}