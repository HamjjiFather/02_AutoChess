namespace KKSFramework.DataBind
{
    /// <summary>
    /// on getter delegate.
    /// </summary>
    public delegate TV GetDelegate<out TV> ();

    /// <summary>
    /// on setter delegate.
    /// </summary>
    public delegate void SetDelegate<in TV> (TV value);

    /// <summary>
    /// property bind.
    /// </summary>
    public class Property<TV>
    {
        /// <summary>
        /// on getter delegate.
        /// </summary>
        private readonly GetDelegate<TV> _get;

        /// <summary>
        /// on setter delegate.
        /// </summary>
        private readonly SetDelegate<TV> _set;

        /// <summary>
        /// constructor.
        /// </summary>
        public Property (GetDelegate<TV> get, SetDelegate<TV> set)
        {
            _get = get;
            _set = set;
        }

        public TV Value
        {
            get => _get ();
            set => _set (value);
        }
    }
}